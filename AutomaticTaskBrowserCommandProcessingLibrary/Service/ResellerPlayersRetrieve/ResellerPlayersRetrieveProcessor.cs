namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskSharedLibrary;
    using OpenQA.Selenium.Chrome;
    using PlayersRepositoryTypes;

    public class ResellerPlayersRetrieveProcessor: IResellerPlayersRetrieveProcessor
    {
        private readonly IResellerPlayersRetrieveAdapter resellerPlayersRetrieveAdapter;
        public ResellerPlayersRetrieveProcessor(IResellerPlayersRetrieveAdapter resellerPlayersRetrieveAdapter)
        {
            this.resellerPlayersRetrieveAdapter = resellerPlayersRetrieveAdapter;
        }

        Task<ResellerPlayersRetrieveResponseDto> IResellerPlayersRetrieveProcessor.Execute(ResellerPlayersRetrieveRequestDto resellerPlayersRetrieveRequestDto)
        {
            try
            {
                return Task<ResellerPlayersRetrieveResponseDto>.Factory.StartNew(
                () =>
                {
                    var resellerId = resellerPlayersRetrieveRequestDto.ResellerId;
                    var softwareType = resellerPlayersRetrieveRequestDto.SoftwareType;
                    var userId = resellerPlayersRetrieveRequestDto.UserId;
                    var password = resellerPlayersRetrieveRequestDto.Password;
                    var organizationId = resellerPlayersRetrieveRequestDto.OrganizationId;
                    var apiKey = resellerPlayersRetrieveRequestDto.ApiKey;
                    var vendorId = resellerPlayersRetrieveRequestDto.VendorId;
                    var drawer = resellerPlayersRetrieveRequestDto.Drawer;

                    var driver = new ChromeDriver(@"C:\Program Files (x86)");
                    try
                    {
                        var request = new ResellerPlayersRetrieveRequest(softwareType, organizationId, apiKey, resellerId, userId, password,vendorId,drawer);
                        var response = this.resellerPlayersRetrieveAdapter.Execute(driver, request);
                        driver.Quit();

                        var result = new ResellerPlayersRetrieveResponseDto()
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

                    return new ResellerPlayersRetrieveResponseDto()
                    {
                        IsSuccessful = false
                    };
                });
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
