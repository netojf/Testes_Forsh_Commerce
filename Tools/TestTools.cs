using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Tools
{
	public class TestTools
	{
      public static IWebElement WaitUntilElementExists(By elementLocator, IWebDriver Driver, int timeout = 10)
      {
         try
         {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.ElementExists(elementLocator));
         }
         catch (NoSuchElementException)
         {
            Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
            throw;
         }
      }
   }
}
