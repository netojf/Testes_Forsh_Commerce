using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Tools
{
	public static class TestTools
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
            throw new Exception("Elemento com o Localizador: '" + elementLocator + "' Não foi encontrado na página.");
         }
      }

      public static IWebElement WaitUntilElementInteractable(By elementLocator, IWebDriver Driver, int timeout = 10)
		{
			try
			{
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator));
			}
			catch (NoSuchElementException)
			{

            throw new Exception("Elemento com o Localizador: '" + elementLocator + "' Não foi encontrado na página.");
         }
		}
   }
}
