using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ditec
{
	public static class TestTools
	{
		public static IWebDriver driver = new ChromeDriver();
		public static IDictionary<string, object> vars { get; private set; }
		public static IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
		public static WebDriverWait timespan = new WebDriverWait(driver, TimeSpan.FromSeconds(4));


		//ACESSA O ADMIN E O DASHBOARD DE FUNCIONARIOS
		public static void LoginAndAccess()
		{
			//ACESSA O MENU DE FUNCIONÁRIOS
			driver.Navigate().GoToUrl("https://admin.ditecdistribuidora.com.br/");
			driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
			driver.FindElement(By.Id("Usuario")).Click();
			driver.FindElement(By.Id("Usuario")).SendKeys("suporte@forsh.com.br");
			driver.FindElement(By.Id("Senha")).SendKeys("1234");
			driver.FindElement(By.CssSelector(".btn")).Click();
		}

		public static IWebElement FindElement(By by)
		{
			return timespan.Until(driver => driver.FindElement(by));
		}
	}
}
