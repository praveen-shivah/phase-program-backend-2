namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;
    using AutomaticTaskBrowserCommandProcessingLibrary.Service.BasePageObjects;
    using OpenQA.Selenium;
    using PlayersRepositoryTypes;
    using SeleniumExtras.PageObjects;
    public class FirestormPlayersPage : BasePlayersPage
    {
        private readonly string pageLoadedText = "Customer Info";
        private By nxtBtnElementLocator = By.XPath("//*[@id=\"CustomerInfo_next\"]");
        private By pageBtnElementLocator = By.XPath("//*[@id=\"CustomerInfo_paginate\"]/span/a");
        private List<ResellerPlayersDetail> resellerPlayersDetails;

        private readonly IPlayersRepository playersRepository;

        private readonly string pageUrl = "https://pos.firestormhub.com/CustomerAccount/CustomerInfo";
        public FirestormPlayersPage(IWebDriver driver, IPlayersRepository playersRepository) : base(driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver.Url = this.pageUrl;
            this.resellerPlayersDetails = new List<ResellerPlayersDetail>();
            this.playersRepository = playersRepository;
        }

        protected override bool isPageUrlSet()
        {
            var result = this.wait.Until(d => d.Url.Contains(this.pageUrl));
            return result;
        }
        private void parseTable(ResellerPlayersRetrieveRequest request)
        {
            var btn_export = getElementByLocator(By.XPath("//*[@id=\"btn_export\"]"));
            while (btn_export.Text != "Excel")
            {
                btn_export = getElementByLocator(By.XPath("//*[@id=\"btn_export\"]"));
            }
            IWebElement nxtbtnElement = driver.FindElement(this.nxtBtnElementLocator);
            string strBtnClass = nxtbtnElement.GetAttribute("class");
            IList<IWebElement> pageBtns = driver.FindElements(this.pageBtnElementLocator);
            int cnt = pageBtns.Count;
            while (!strBtnClass.Contains("disabled") || cnt == 1)
            {
                cnt = 2;
                IWebElement tableElement = driver.FindElement(By.XPath("//*[@id='CustomerInfo']"));
                IList<IWebElement> trCollection = tableElement.FindElements(By.TagName("tr"));
                IList<IWebElement> tdCollection;
                foreach (IWebElement element in trCollection)
                {
                    try
                    {
                        tdCollection = element.FindElements(By.TagName("td"));
                        if (tdCollection.Count > 0)
                        {
                            resellerPlayersDetails.Add(new ResellerPlayersDetail()
                            {
                                CustomerId = tdCollection[0].Text,
                                MobileId = tdCollection[1].Text,
                                Name = tdCollection[2].Text,
                                Gender = tdCollection[3].Text,
                                Phone = tdCollection[4].Text,
                                Mail = tdCollection[5].Text,
                                OrganizationId = request.OrganizationId,
                                ResellerId = request.ResellerId,
                                VendorId = request.VendorId,
                                LoginUsername = request.LoginPageInformation.SiteUserId,
                                LoginPassword = request.LoginPageInformation.SitePassword
                            });
                        }
                    }
                    catch { }
                }
                try
                {
                    nxtbtnElement = driver.FindElement(this.nxtBtnElementLocator);
                    strBtnClass = nxtbtnElement.GetAttribute("class");
                    nxtbtnElement.Click();
                    Thread.Sleep(10 * 1000);
                }
                catch { break; }
            }
        }

        protected override ResellerPlayersDetail[] savePlayersDetails(ResellerPlayersRetrieveRequest request)
        {
            parseTable(request);
            foreach (var resellerPlayerDetail in resellerPlayersDetails)
            {
                PlayerDto playerInformation = new PlayerDto
                {
                    PlayerId = resellerPlayerDetail.CustomerId,
                    MobileId = resellerPlayerDetail.MobileId,
                    Name = resellerPlayerDetail.Name,
                    Gender = resellerPlayerDetail.Gender,
                    Phone = resellerPlayerDetail.Phone,
                    Mail = resellerPlayerDetail.Mail,
                    OrganizationId = request.OrganizationId,
                    ResellerId = request.ResellerId,
                    VendorId = request.VendorId,
                    LoginUsername = request.LoginPageInformation.SiteUserId,
                    LoginPassword = request.LoginPageInformation.SitePassword
                };
                playersRepository.AddPlayerRequestAsync(playerInformation).GetAwaiter().GetResult();
            }
            return resellerPlayersDetails.ToArray();
        }

        protected override bool verifyPageLoaded()
        {
            var result = this.wait.Until(d => d.PageSource.Contains(this.pageLoadedText));
            return result;
        }
    }
}
