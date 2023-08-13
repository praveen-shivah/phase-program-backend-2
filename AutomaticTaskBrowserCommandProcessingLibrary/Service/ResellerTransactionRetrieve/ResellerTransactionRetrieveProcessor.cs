using AutomaticTaskSharedLibrary;
using InvoiceRepositoryTypes;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public class ResellerTransactionRetrieveProcessor : IResellerTransactionRetrieveProcessor
    {
        private readonly IResellerTransactionRetrieveAdapter resellerTransactionRetrieveAdapter;

        public ResellerTransactionRetrieveProcessor(IResellerTransactionRetrieveAdapter resellerTransactionRetrieveAdapter)
        {
            this.resellerTransactionRetrieveAdapter = resellerTransactionRetrieveAdapter;
        }

        Task<ResellerTransactionRetrieveResponseDto> IResellerTransactionRetrieveProcessor.Execute(ResellerTransactionRetrieveRequestDto resellerTransactionRetrieveRequestDto)
        {
            return Task<ResellerTransactionRetrieveResponseDto>.Factory.StartNew(
                () =>
                {
                    var resellerId = resellerTransactionRetrieveRequestDto.ResellerId;
                    var softwareType = resellerTransactionRetrieveRequestDto.SoftwareType;
                    var userId = resellerTransactionRetrieveRequestDto.UserId;
                    var password = resellerTransactionRetrieveRequestDto.Password;
                    var organizationId = resellerTransactionRetrieveRequestDto.OrganizationId;
                    var apiKey = resellerTransactionRetrieveRequestDto.ApiKey;

                    var driver = new ChromeDriver(@"C:\Program Files (x86)");
                    try
                    {
                        var request = new ResellerTransactionRetrieveRequest(softwareType, organizationId, apiKey, resellerId, userId, password);
                        var response = this.resellerTransactionRetrieveAdapter.Execute(driver, request);
                        driver.Quit();

                        var result = new ResellerTransactionRetrieveResponseDto()
                        {
                            IsSuccessful = response.IsSuccessful,
                            Details = response.Details
                        };
                        return result;
                    }
                    catch
                    {
                    }

                    driver.Quit();

                    return new ResellerTransactionRetrieveResponseDto()
                    {
                        IsSuccessful = false
                    };
                });
        }
    }
}
