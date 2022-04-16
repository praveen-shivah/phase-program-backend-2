﻿namespace MobileRequestApi.Middleware
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.IO;

    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }

    public class RequestResponseLoggingMiddleware
    {
        private readonly ILogger _logger;

        private readonly RequestDelegate _next;

        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this._next = next;
            this._logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
            this._recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            await this.LogRequest(context);
            await this.LogResponse(context);
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using (var textWriter = new StringWriter())
            {
                using (var reader = new StreamReader(stream))
                {
                    var readChunk = new char[readChunkBufferLength];
                    int readChunkLength;

                    do
                    {
                        readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                        textWriter.Write(readChunk, 0, readChunkLength);
                    }
                    while (readChunkLength > 0);

                    return textWriter.ToString();
                }
            }
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = this._recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Http Request Information:");
            sb.AppendLine($"Schema:{context.Request.Scheme}");
            sb.AppendLine($"Host: {context.Request.Host}");
            sb.AppendLine($"Path: {context.Request.Path}");
            sb.AppendLine($"QueryString: {context.Request.QueryString}");
            sb.AppendLine($"Content-Type: {context.Request.ContentType}");
            sb.AppendLine();
            sb.AppendLine($"Request Body: {ReadStreamInChunks(requestStream)}");
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            this._logger.LogInformation(sb.ToString());
            context.Request.Body.Position = 0;
        }

        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            await using var responseBody = this._recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            await this._next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            this._logger.LogInformation(
                $"Http Response Information:{Environment.NewLine}" + $"Schema:{context.Request.Scheme} " + $"Host: {context.Request.Host} " + $"Path: {context.Request.Path} "
                + $"QueryString: {context.Request.QueryString} " + $"Response Body: {text}");

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}