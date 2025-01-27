﻿using Microsoft.AspNetCore.Mvc;

namespace ApiHost
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ApiDTO;

    using ApiHost.Middleware;
    using APISupport;

    using AuthenticationRepositoryTypes;

    using LoggingLibrary;

    using VendorRepositoryTypes;

    [AuthorizePolicy]
    [ApiController]
    [Route("api/vendor")]
    public class VendorController : Controller
    {
        private readonly ILogger logger;

        private readonly IVendorRepository vendorRepository;

        public VendorController(ILogger logger, IVendorRepository vendorRepository)
        {
            this.logger = logger;
            this.vendorRepository = vendorRepository;
        }

        [HttpGet("get-vendors")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ALL)]
        public async Task<ActionResult<List<VendorDto>>> GetVendors()
        {
            this.logger.Debug(LogClass.General, "GetVendors received");

            var result = await this.vendorRepository.GetVendors();
            return this.Ok(result);
        }

        [HttpPost("update-vendor")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ALL)]
        public async Task<IActionResult> UpdateVendor(VendorDto vendorDto)
        {
            this.logger.Debug(LogClass.General, "UpdateVendor received");

            var result = await this.vendorRepository.UpdateVendorRequestAsync(vendorDto);
            return this.Ok(result);
        }
    }
}
