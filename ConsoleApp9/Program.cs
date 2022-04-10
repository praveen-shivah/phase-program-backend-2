// See https://aka.ms/new-console-template for more information

using PuppeteerSharp;

Console.WriteLine("PuppeteerSharp");
// await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

// Create an instance of the browser and configure launch options
Console.WriteLine("PuppeteerSharp - launching browser");
Browser browser = await Puppeteer.LaunchAsync(
                      new LaunchOptions
                          {
                              ExecutablePath = "/opt/google/chrome/chrome",
                              Headless = true,
                              Args = new[] { "--disable-gpu", "--disable-dev-shm-usage", "--disable-setuid-sandbox", "--no-sandbox" }
                          });

// Create a new page and go to Bing Maps
Console.WriteLine("PuppeteerSharp - launching maps");
Page page = await browser.NewPageAsync();
await page.GoToAsync("https://www.bing.com/maps");

// Search for a local tourist attraction on Bing Maps
Console.WriteLine("PuppeteerSharp - awaiting input");
await page.WaitForSelectorAsync(".searchbox input");
await page.FocusAsync(".searchbox input");
await page.Keyboard.TypeAsync("CN Tower, Toronto, Ontario, Canada");
await page.ClickAsync(".searchIcon");
await page.WaitForNavigationAsync(new NavigationOptions() { WaitUntil = new WaitUntilNavigation[] { WaitUntilNavigation.DOMContentLoaded, WaitUntilNavigation.Networkidle0, WaitUntilNavigation.Networkidle2 } });

Console.WriteLine("PuppeteerSharp - saving screenshot");
await page.ScreenshotAsync("screenshot.png");

await browser.CloseAsync();

