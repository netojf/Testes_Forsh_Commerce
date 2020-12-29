// Generated by Selenium IDE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;

namespace Ditec.Teste_de_Interface.ST01Funcionarios
{
	[TestFixture]
	public class CT01CadastroDeFuncionarioCamposVaziosTest
	{
		private IWebDriver driver;
		public IDictionary<string, object> vars { get; private set; }
		private IJavaScriptExecutor js;
		WebDriverWait timespan;

		[SetUp]
		public void SetUp()
		{
			driver = new ChromeDriver();
			js = (IJavaScriptExecutor)driver;
			vars = new Dictionary<string, object>();
			timespan = new WebDriverWait(driver, TimeSpan.FromSeconds(2)); 
		}

		//[TearDown]
		//protected void TearDown()
		//{
		//	driver.Quit();
		//}

		[Test]
		public void cT01CadastroDeFuncionarioCamposVazios()
		{
			driver.Navigate().GoToUrl("https://admin.ditecdistribuidora.com.br/");
			driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
			driver.FindElement(By.Id("Usuario")).Click();
			driver.FindElement(By.Id("Usuario")).SendKeys("suporte@forsh.com.br");
			driver.FindElement(By.Id("Senha")).SendKeys("1234");
			
			driver.FindElement(By.CssSelector(".btn")).Click();


			timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Dashboard Admin"))).Click();

			timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Distribuidoras"))).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Funcionários"))).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("button[title='Cadastrar funcionário']"))).Click();

			driver.FindElement(By.CssSelector(".btn-success")).Click();
			Assert.That(driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("×\\\\nVerifique os dados informados."));
			Assert.That(driver.FindElement(By.CssSelector(".form-group:nth-child(2) .field-validation-error")).Text, Is.EqualTo("Preencha o campo Nome"));
			Assert.That(driver.FindElement(By.CssSelector(".form-group:nth-child(3) .field-validation-error")).Text, Is.EqualTo("The value \\\'Selecione\\\' is not valid for Perfil do Usuário."));
			Assert.That(driver.FindElement(By.CssSelector(".form-group:nth-child(4) .field-validation-error")).Text, Is.EqualTo("Preencha o campo Nome de Usuário"));
			Assert.That(driver.FindElement(By.CssSelector(".form-group:nth-child(5) .field-validation-error")).Text, Is.EqualTo("Preencha o campo E-mail"));
			Assert.That(driver.FindElement(By.CssSelector("#divSenha .field-validation-error")).Text, Is.EqualTo("Preencha o campo Senha"));
		}
	}

}
