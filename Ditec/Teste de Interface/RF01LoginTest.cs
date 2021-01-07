using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ditec.Teste_de_Interface
{
	[TestFixture]
	public class LoginTest
	{
		private IWebDriver driver;
		public IDictionary<string, object> vars { get; private set; }
		private IJavaScriptExecutor js;
		WebDriverWait timespan;
		string email = "raphael.mota14@hotmail.com";

		[SetUp]
		public void SetUp()
		{
			driver = new ChromeDriver();
			js = (IJavaScriptExecutor)driver;
			vars = new Dictionary<string, object>();
			timespan = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
		}

		[TearDown]
		protected void TearDown()
		{
			driver.Quit();
		}

		[Test]
		public void CTI01UsuarioESenhaValidos()
		{
			driver.Navigate().GoToUrl("https://admin.ditecdistribuidora.com.br/");
			driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
			driver.FindElement(By.Id("Usuario")).Click();
			driver.FindElement(By.Id("Usuario")).SendKeys("suporte@forsh.com.br");
			driver.FindElement(By.Id("Senha")).SendKeys("1234");

			driver.FindElement(By.CssSelector(".btn")).Click();

			try
			{
				var dashboardBttn = timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Dashboard Admin")));
				Assert.True(true); 
			}
			catch (NoSuchElementException)
			{
				Assert.True(false);  
			}
		}

		[Test]
		public void CTI02SenhaInvalida()
		{
			driver.Navigate().GoToUrl("https://admin.ditecdistribuidora.com.br/");
			driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
			driver.FindElement(By.Id("Usuario")).Click();
			driver.FindElement(By.Id("Usuario")).SendKeys("suporte@forsh.com.br");
			driver.FindElement(By.Id("Senha")).SendKeys("1235");

			driver.FindElement(By.CssSelector(".btn")).Click();

			try
			{
				var dashboardBttn = timespan.Until(ExpectedConditions.ElementExists(By.XPath(string.Format("//span[contains(text(), '{0}')]//parent::div", "Usuário ou Senha invalidos"))));
				Assert.True(true);
			}
			catch (NoSuchElementException)
			{
				Assert.True(false);
			}
		}

		[Test]
		public void CTI03UsuarioInvalido()
		{
			driver.Navigate().GoToUrl("https://admin.ditecdistribuidora.com.br/");
			driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
			driver.FindElement(By.Id("Usuario")).Click();
			driver.FindElement(By.Id("Usuario")).SendKeys("suporte@fors");
			driver.FindElement(By.Id("Senha")).SendKeys("1234");

			driver.FindElement(By.CssSelector(".btn")).Click();

			try
			{
				var dashboardBttn = timespan.Until(ExpectedConditions.ElementExists(By.XPath(string.Format("//span[contains(text(), '{0}')]//parent::div", "Usuário ou Senha invalidos"))));
				Assert.True(true);
			}
			catch (NoSuchElementException)
			{
				Assert.True(false);
			}
		}

		[Test]
		public void CTI04LoginComNomeDeUsuario()
		{
			driver.Navigate().GoToUrl("https://admin.ditecdistribuidora.com.br/");
			driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
			driver.FindElement(By.Id("Usuario")).Click();
			driver.FindElement(By.Id("Usuario")).SendKeys("Admin");
			driver.FindElement(By.Id("Senha")).SendKeys("1234");

			driver.FindElement(By.CssSelector(".btn")).Click();

			try
			{
				var dashboardBttn = timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Dashboard Admin")));
				Assert.True(true);
			}
			catch (NoSuchElementException)
			{
				Assert.True(false);
			}
		}

	}
}
