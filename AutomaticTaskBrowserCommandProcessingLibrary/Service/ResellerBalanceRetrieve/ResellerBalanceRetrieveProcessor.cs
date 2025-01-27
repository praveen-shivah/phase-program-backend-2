﻿namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskSharedLibrary;

    using InvoiceRepositoryTypes;

    using OpenQA.Selenium.Chrome;

    public class ResellerBalanceRetrieveProcessor : IResellerBalanceRetrieveProcessor
    {
        private readonly IResellerBalanceRetrieveAdapter resellerBalanceRetrieveAdapter;

        public ResellerBalanceRetrieveProcessor(IResellerBalanceRetrieveAdapter resellerBalanceRetrieveAdapter)
        {
            this.resellerBalanceRetrieveAdapter = resellerBalanceRetrieveAdapter;
        }

        Task<ResellerBalanceRetrieveResponseDto> IResellerBalanceRetrieveProcessor.Execute(ResellerBalanceRetrieveRequestDto resellerBalanceRetrieveRequestDto)
        {
            return Task<ResellerBalanceRetrieveResponseDto>.Factory.StartNew(
                () =>
                    {
                        var resellerId = resellerBalanceRetrieveRequestDto.ResellerId;
                        var softwareType = resellerBalanceRetrieveRequestDto.SoftwareType;
                        var userId = resellerBalanceRetrieveRequestDto.UserId;
                        var password = resellerBalanceRetrieveRequestDto.Password;
                        var organizationId = resellerBalanceRetrieveRequestDto.OrganizationId;
                        var apiKey = resellerBalanceRetrieveRequestDto.ApiKey;

                        var driver = new ChromeDriver(@"C:\Program Files (x86)");
                        try
                        {
                            var request = new ResellerBalanceRetrieveRequest(softwareType, organizationId, apiKey, resellerId, userId, password);
                            var response = this.resellerBalanceRetrieveAdapter.Execute(driver, request);
                            driver.Quit();

                            var result = new ResellerBalanceRetrieveResponseDto()
                            {
                                IsSuccessful = response.IsSuccessful,
                                BalanceAsPoints = response.BalanceAsPoints
                            };
                            return result;
                        }
                        catch
                        {
                        }

                        driver.Quit();

                        return new ResellerBalanceRetrieveResponseDto()
                        {
                            IsSuccessful = false,
                            BalanceAsPoints = 0
                        };
                    });
        }
    }
}