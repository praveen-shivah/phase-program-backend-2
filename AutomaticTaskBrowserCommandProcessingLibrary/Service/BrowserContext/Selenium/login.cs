using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using SeleniumExtras.PageObjects;

public class Login
{
    private readonly string pageLoadedText = "";

    private readonly string pageUrl = "/";

    [FindsBy(How = How.CssSelector, Using = "#vuePopup section.login-screen div:nth-of-type(1) div:nth-of-type(2) button:nth-of-type(11)")]
    [CacheLookup]
    private IWebElement _0;
    [FindsBy(How = How.CssSelector, Using = "#vuePopup section.login-screen div:nth-of-type(1) div:nth-of-type(2) button:nth-of-type(1)")]
    [CacheLookup]
    private IWebElement _1;
    [FindsBy(How = How.CssSelector, Using = "#vuePopup section.login-screen div:nth-of-type(1) div:nth-of-type(2) button:nth-of-type(2)")]
    [CacheLookup]
    private IWebElement _2;
    [FindsBy(How = How.CssSelector, Using = "#vuePopup section.login-screen div:nth-of-type(1) div:nth-of-type(2) button:nth-of-type(3)")]
    [CacheLookup]
    private IWebElement _3;
    [FindsBy(How = How.CssSelector, Using = "#vuePopup section.login-screen div:nth-of-type(1) div:nth-of-type(2) button:nth-of-type(4)")]
    [CacheLookup]
    private IWebElement _4;
    [FindsBy(How = How.CssSelector, Using = "#vuePopup section.login-screen div:nth-of-type(1) div:nth-of-type(2) button:nth-of-type(5)")]
    [CacheLookup]
    private IWebElement _5;
    [FindsBy(How = How.CssSelector, Using = "#vuePopup section.login-screen div:nth-of-type(1) div:nth-of-type(2) button:nth-of-type(6)")]
    [CacheLookup]
    private IWebElement _6;
    [FindsBy(How = How.CssSelector, Using = "#vuePopup section.login-screen div:nth-of-type(1) div:nth-of-type(2) button:nth-of-type(7)")]
    [CacheLookup]
    private IWebElement _7;
    [FindsBy(How = How.CssSelector, Using = "#vuePopup section.login-screen div:nth-of-type(1) div:nth-of-type(2) button:nth-of-type(8)")]
    [CacheLookup]
    private IWebElement _8;
    [FindsBy(How = How.CssSelector, Using = "#vuePopup section.login-screen div:nth-of-type(1) div:nth-of-type(2) button:nth-of-type(9)")]
    [CacheLookup]
    private IWebElement _9;
    [FindsBy(How = How.CssSelector, Using = "button.pin-keyboard__btn.app-btn.app-btn_uppercase.app-btn_type_default.app-btn_size_sm.app-btn_color_third")]
    [CacheLookup]
    private IWebElement c;

    private readonly Dictionary<string, string> data;

    private readonly IWebDriver driver;

    [FindsBy(How = How.CssSelector, Using = "button.pin-keyboard__btn.app-btn.app-btn_uppercase.app-btn_type_default.app-btn_size_sm.app-btn_color_default")]
    [CacheLookup]
    private IWebElement ok;
    [FindsBy(How = How.CssSelector, Using = "input.app-input__field")]
    [CacheLookup]
    private IWebElement rememberMyPin1;
    [FindsBy(How = How.CssSelector, Using = "#vuePopup section.login-screen div:nth-of-type(1) div:nth-of-type(3) label.checkbox-item input[type='checkbox']")]
    [CacheLookup]
    private IWebElement rememberMyPin2;

    private readonly int timeout = 15;

    public Login()
        : this(default(IWebDriver), new Dictionary<string, string>(), 15)
    {
    }

    public Login(IWebDriver driver)
        : this(driver, new Dictionary<string, string>(), 15)
    {
    }

    public Login(IWebDriver driver, Dictionary<string, string> data)
        : this(driver, data, 15)
    {
    }

    public Login(
        IWebDriver driver,
        Dictionary<string, string> data,
        int timeout)
    {
        this.driver = driver;
        this.data = data;
        this.timeout = timeout;
    }

    /// <summary>
    ///     Click on 0 Button.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login ClickButton0()
    {
        this._0.Click();
        return this;
    }

    /// <summary>
    ///     Click on 1 Button.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login ClickButton1()
    {
        this._1.Click();
        return this;
    }

    /// <summary>
    ///     Click on 2 Button.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login ClickButton2()
    {
        this._2.Click();
        return this;
    }

    /// <summary>
    ///     Click on 3 Button.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login ClickButton3()
    {
        this._3.Click();
        return this;
    }

    /// <summary>
    ///     Click on 4 Button.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login ClickButton4()
    {
        this._4.Click();
        return this;
    }

    /// <summary>
    ///     Click on 5 Button.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login ClickButton5()
    {
        this._5.Click();
        return this;
    }

    /// <summary>
    ///     Click on 6 Button.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login ClickButton6()
    {
        this._6.Click();
        return this;
    }

    /// <summary>
    ///     Click on 7 Button.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login ClickButton7()
    {
        this._7.Click();
        return this;
    }

    /// <summary>
    ///     Click on 8 Button.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login ClickButton8()
    {
        this._8.Click();
        return this;
    }

    /// <summary>
    ///     Click on 9 Button.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login ClickButton9()
    {
        this._9.Click();
        return this;
    }

    /// <summary>
    ///     Click on C Button.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login ClickCButton()
    {
        this.c.Click();
        return this;
    }

    /// <summary>
    ///     Click on Ok Button.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login ClickOkButton()
    {
        this.ok.Click();
        return this;
    }

    /// <summary>
    ///     Fill every fields in the page.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login Fill()
    {
        this.SetRememberMyPin1CheckboxField();
        this.SetRememberMyPin2CheckboxField();
        return this;
    }

    /// <summary>
    ///     Set default value to Remember My Pin Text field.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login SetRememberMyPin1CheckboxField()
    {
        return this.SetRememberMyPin1CheckboxField(this.data["REMEMBER_MY_PIN"]);
    }

    /// <summary>
    ///     Set Remember My Pin Checkbox field.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login SetRememberMyPin1CheckboxField(string rememberMyPinValue)
    {
        this.rememberMyPin1.SendKeys(rememberMyPinValue);
        return this;
    }

    /// <summary>
    ///     Set Remember My Pin Checkbox field.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login SetRememberMyPin2CheckboxField()
    {
        if (!this.rememberMyPin2.Selected)
        {
            this.rememberMyPin2.Click();
        }

        return this;
    }

    /// <summary>
    ///     Unset Remember My Pin Checkbox field.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login UnsetRememberMyPin2CheckboxField()
    {
        if (this.rememberMyPin2.Selected)
        {
            this.rememberMyPin2.Click();
        }

        return this;
    }

    /// <summary>
    ///     Verify that the page loaded completely.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login VerifyPageLoaded()
    {
        new WebDriverWait(this.driver, TimeSpan.FromSeconds(this.timeout)).Until(d => { return d.PageSource.Contains(this.pageLoadedText); });
        return this;
    }

    /// <summary>
    ///     Verify that current page URL matches the expected URL.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public Login VerifyPageUrl()
    {
        new WebDriverWait(this.driver, TimeSpan.FromSeconds(this.timeout)).Until(d => { return d.Url.Contains(this.pageUrl); });
        return this;
    }
}