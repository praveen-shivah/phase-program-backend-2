﻿namespace DatabaseClassLibrary
{
    using System.Data.Common;

    using Microsoft.Extensions.Configuration;

    using Npgsql;

    using SharedUtilities;
    using SharedUtilities.Utilities;

    public class ConnectionFactoryNormal : IConnectionFactory
    {
        private readonly IConfiguration configuration;

        public ConnectionFactoryNormal(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        DbConnection IConnectionFactory.Create()
        {
            var connectionString = this.configuration.GetConnectionString("MobileOMatic")
                                   ?? "host=localhost;database=postgres2;user id=postgres;password=~!AmyLee~!0;Include Error Detail=true";
            var sqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString)
            {
                MaxPoolSize = 10000,
                MinPoolSize = 200
            };
            var connection = new NpgsqlConnection(sqlConnectionStringBuilder.ConnectionString);
            return connection;
        }

        DbConnection IConnectionFactory.Create(string dbName)
        {
            var connectionString = this.configuration.GetConnectionString("MobileOMatic")
                                   ?? "host=localhost;database=postgres2;user id=postgres;password=~!AmyLee~!0;Include Error Detail=true";
            connectionString = connectionString.ReplaceBetween("Catalog=", ";", $"{dbName}");
            var sqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString)
            {
                MaxPoolSize = 10000,
                MinPoolSize = 200
            };
            var connection = new NpgsqlConnection(sqlConnectionStringBuilder.ConnectionString);
            return connection;
        }

        DbConnection IConnectionFactory.CreateMaster()
        {
            var connectionString = this.configuration.GetConnectionString("MobileOMatic")
                                   ?? "host=localhost;database=postgres2;user id=postgres;password=~!AmyLee~!0;Include Error Detail=true";
            connectionString = connectionString.ReplaceBetween("Catalog=", ";", "Master");
            var sqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString)
            {
                MaxPoolSize = 10000,
                MinPoolSize = 200
            };
            var connection = new NpgsqlConnection(sqlConnectionStringBuilder.ConnectionString);
            return connection;
        }
    }
}