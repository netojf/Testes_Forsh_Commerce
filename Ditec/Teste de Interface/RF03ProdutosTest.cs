using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Tools;

namespace Ditec.Teste_de_Interface
{
	[TestFixture]
	public class RF03ProdutosTest
	{

		List<string> data;
		string email = "teste@teste.com";
		public static IWebDriver driver;
		public static IJavaScriptExecutor js;

		[SetUp]
		public void SetUp()
		{
			data = new List<string>();
			driver = new ChromeDriver();
			js = (IJavaScriptExecutor)driver;

			Acessar();
		}

		[TearDown]
		protected void TearDown()
		{
			foreach (var search in data)
			{
				if (search != "")
				{
					Excluir(search);
				}
			}

			driver.Quit();
		}

		[Test]
		public void CTI01CadastroDeProdutoCamposVazios()
		{
			
			driver.FindElement(By.CssSelector(".btn-success")).Click();

			//todo: captura dos avisos quando consertar o erro 404
			Assert.That(driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("×\\\\nVerifique os dados informados."));
			Assert.That(driver.FindElement(By.CssSelector(".form-group:nth-child(2) .field-validation-error")).Text, Is.EqualTo("Preencha o campo Nome"));
			Assert.That(driver.FindElement(By.CssSelector(".form-group:nth-child(3) .field-validation-error")).Text, Is.EqualTo("The value \\\'Selecione\\\' is not valid for Perfil do Usuário."));
			Assert.That(driver.FindElement(By.CssSelector(".form-group:nth-child(4) .field-validation-error")).Text, Is.EqualTo("Preencha o campo Nome de Usuário"));
			Assert.That(driver.FindElement(By.CssSelector(".form-group:nth-child(5) .field-validation-error")).Text, Is.EqualTo("Preencha o campo E-mail"));
			Assert.That(driver.FindElement(By.CssSelector("#divSenha .field-validation-error")).Text, Is.EqualTo("Preencha o campo Senha"));
		}




		void Acessar()
		{
			//Acesso ao RF
			driver.Navigate().GoToUrl("https://admin.ditecdistribuidora.com.br/");
			driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
			driver.FindElement(By.Id("Usuario")).Click();
			driver.FindElement(By.Id("Usuario")).SendKeys("suporte@forsh.com.br");
			driver.FindElement(By.Id("Senha")).SendKeys("1234");
			driver.FindElement(By.CssSelector(".btn")).Click();

			TestTools.WaitUntilElementExists(By.XPath("//*[. = 'Dashboard Admin']"), driver).Click();
			var productTab  =  TestTools.WaitUntilElementExists(By.XPath("//*[. = 'Produtos']"), driver);
			productTab.Click();
			Thread.Sleep(1000); 
			productTab.FindElement(By.XPath("//a[. = 'Produtos']")).Click();
			TestTools.WaitUntilElementExists(By.XPath("//h2[. = 'Produtos']"), driver);

		}
		void Cadastrar()
		{
			TestTools.WaitUntilElementExists(By.CssSelector("button[title='Cadastrar Produto']"), driver).Click();
		}
		void Excluir(string search) { }
		void Buscar() { }
	}
}
