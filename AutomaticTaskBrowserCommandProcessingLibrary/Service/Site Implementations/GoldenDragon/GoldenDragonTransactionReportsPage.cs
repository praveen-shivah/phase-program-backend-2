using AutomaticTaskSharedLibrary;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public class GoldenDragonTransactionReportsPage : BaseTransactionReportsPage
    {
        private readonly string pageLoadedText = "Transaction Search Rule";
        private By lastWeekElementLocator = By.XPath("//*[@id='_SearchForm_']/ul/li[5]/button[4]");
        private By thisWeekElementLocator = By.XPath("//*[@id='_SearchForm_']/ul/li[5]/button[3]");
        private By reportsBtnElementLocator = By.XPath("//*[@id='ReportsBtn']");
        private By detailReportsElementLocator = By.XPath("//*[@id='DetailReports']/tbody");
        private By nxtBtnElementLocator = By.XPath("//*[@id='DetailReports_next']");
        private By pageBtnElementLocator = By.XPath("//*[@id='DetailReports_paginate']/span/a");
        private List<ResellerTransactionDetail> resellerTransactionDetails = null;


        private readonly string pageUrl = "https://pos.goldendragoncity.com/Reports/TransactionReports";
        public GoldenDragonTransactionReportsPage(IWebDriver driver)
            : base(driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver.Url = this.pageUrl;
            this.resellerTransactionDetails = new List<ResellerTransactionDetail>();
        }

        protected override bool isPageUrlSet()
        {
            var result = this.wait.Until(d => d.Url.Contains(this.pageUrl));
            return result;
        }

        protected override bool verifyPageLoaded()
        {
            var result = this.wait.Until(d => d.PageSource.Contains(this.pageLoadedText));
            return result;
        }

        private void parseTable()
        {
            IWebElement nxtbtnElement = driver.FindElement(this.nxtBtnElementLocator);
            string strBtnClass = nxtbtnElement.GetAttribute("class");
            IList<IWebElement> pageBtns = driver.FindElements(this.pageBtnElementLocator);
            int cnt = pageBtns.Count;
            while (!strBtnClass.Contains("disabled") || cnt == 1)
            {
                cnt = 2;
                IWebElement tableElement = driver.FindElement(By.XPath("//*[@id='DetailReports']"));
                IList<IWebElement> trCollection = tableElement.FindElements(By.TagName("tr"));
                IList<IWebElement> tdCollection;
                foreach (IWebElement element in trCollection)
                {
                    try
                    {
                        tdCollection = element.FindElements(By.TagName("td"));
                        if (tdCollection.Count > 0)
                        {
                            resellerTransactionDetails.Add(new ResellerTransactionDetail()
                            {
                                Time = tdCollection[0].Text,
                                Station = tdCollection[1].Text,
                                CustomerID = tdCollection[2].Text,
                                Type = tdCollection[3].Text,
                                Amount = tdCollection[4].Text,
                                Comps = tdCollection[5].Text,
                                Free = tdCollection[6].Text
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

        protected override ResellerTransactionDetail[] fetchReport()
        {

            var lastWeekElement = getElementByLocator(this.lastWeekElementLocator);
            var reportsBtnElement = getElementByLocator(this.reportsBtnElementLocator);
            lastWeekElement.Click();
            reportsBtnElement.Click();
            this.wait.Until(d => d.PageSource.Contains("Transaction Detail Reports"));
            Thread.Sleep(10 * 1000);
            parseTable();

            var thisWeekElement = getElementByLocator(this.thisWeekElementLocator);
            thisWeekElement.Click();
            reportsBtnElement = getElementByLocator(this.reportsBtnElementLocator);
            reportsBtnElement.Click();
            this.wait.Until(d => d.PageSource.Contains("Transaction Detail Reports"));
            Thread.Sleep(10 * 1000);
            parseTable();
            return resellerTransactionDetails.ToArray();
        }
    }
}
