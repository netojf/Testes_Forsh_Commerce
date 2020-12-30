using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Ditec.Teste_de_Interface
{
	[TestFixture]
	public class FuncionariosTest
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

		//ACESSA O ADMIN E O DASHBOARD DE FUNCIONARIOS
		private void LoginAndAccess()
		{
			//ACESSA O MENU DE FUNCIONÁRIOS
			driver.Navigate().GoToUrl("https://admin.ditecdistribuidora.com.br/");
			driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
			driver.FindElement(By.Id("Usuario")).Click();
			driver.FindElement(By.Id("Usuario")).SendKeys("suporte@forsh.com.br");
			driver.FindElement(By.Id("Senha")).SendKeys("1234");
			driver.FindElement(By.CssSelector(".btn")).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Dashboard Admin"))).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Distribuidoras"))).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Funcionários"))).Click();
		}

		[Test]
		public void CT01CadastroDeFuncionarioCamposVazios()
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


		[Test]
		public void CT02CadastroDeFuncionariosCamposPreenchidos()
		{
			driver.Navigate().GoToUrl("https://admin.ditecdistribuidora.com.br/");
			driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
			driver.FindElement(By.Id("Usuario")).SendKeys("suporte@forsh.com.br");
			driver.FindElement(By.Id("Senha")).SendKeys("1234");
			driver.FindElement(By.CssSelector(".btn")).Click();
			driver.FindElement(By.LinkText("Dashboard Admin")).Click();

			timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Distribuidoras"))).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Funcionários"))).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("button[title='Cadastrar funcionário']"))).Click();

			driver.FindElement(By.Id("Nome")).Click();
			driver.FindElement(By.Id("Nome")).SendKeys("Raphael");
			driver.FindElement(By.Id("PerfilFuncionario")).Click();
			{
				var dropdown = driver.FindElement(By.Id("PerfilFuncionario"));
				dropdown.FindElement(By.XPath("//option[. = 'Administrador']")).Click();
			}
			driver.FindElement(By.Id("PerfilFuncionario")).Click();
			driver.FindElement(By.Id("NomeUsuario")).Click();
			driver.FindElement(By.Id("NomeUsuario")).SendKeys("raphael");
			driver.FindElement(By.Id("Email")).Click();
			driver.FindElement(By.Id("Email")).SendKeys("raphael.mota14@hotmail.com");
			driver.FindElement(By.Id("Senha")).SendKeys("1234");
			driver.FindElement(By.Id("Ativo")).Click();
			driver.FindElement(By.CssSelector(".btn-success")).Click();
			string message = driver.FindElement(By.CssSelector(".alert")).Text;
			Assert.That(message == "×\r\nRegistro salvo com sucesso!");
		}


		[Test]
		public void CT03CadastroDeFuncionarioEmailExistente()
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
			driver.FindElement(By.Id("Nome")).Click();
			driver.FindElement(By.Id("Nome")).SendKeys("Raphael");
			driver.FindElement(By.Id("PerfilFuncionario")).Click();
			{
				var dropdown = driver.FindElement(By.Id("PerfilFuncionario"));
				dropdown.FindElement(By.XPath("//option[. = 'Administrador']")).Click();
			}
			driver.FindElement(By.Id("PerfilFuncionario")).Click();
			driver.FindElement(By.Id("NomeUsuario")).Click();
			driver.FindElement(By.Id("NomeUsuario")).SendKeys("raphael");
			driver.FindElement(By.Id("Email")).SendKeys("raphael.mota14@hotmail.com");
			driver.FindElement(By.Id("Senha")).SendKeys("1234");
			driver.FindElement(By.Id("Ativo")).Click();
			driver.FindElement(By.CssSelector(".btn-success")).Click();

			Assert.That(driver.PageSource.Contains("O nome de usuário informado já está sendo utilizado!"));

			Assert.That(driver.PageSource.Contains("O email informado já está sendo utilizado!"));
		}


		[Test]
		public void CT04AlteraçãoDeCadastroComEmailInvalidoClick()
		{

			//ACESSA O MENU DE FUNCIONÁRIOS
			driver.Navigate().GoToUrl("https://admin.ditecdistribuidora.com.br/");
			driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
			driver.FindElement(By.Id("Usuario")).Click();
			driver.FindElement(By.Id("Usuario")).SendKeys("suporte@forsh.com.br");
			driver.FindElement(By.Id("Senha")).SendKeys("1234");
			driver.FindElement(By.CssSelector(".btn")).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Dashboard Admin"))).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Distribuidoras"))).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Funcionários"))).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("td")));

			//TESTA SE HÁ O CADASTRO DE TESTE (DEFINIDO PELA VARIÁVEL email) E ACESSA A EDIÇÃO DO CADASTRO
			try
			{
				var row = timespan.Until(ExpectedConditions.ElementExists(By.XPath(string.Format("//tr/td[contains(text(), '{0}')]//parent::tr", email))));
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			catch (InvalidSelectorException)
			{

				throw new Exception("Botão de Edição ou cadastro não encontrado na página");
			}

			IWebElement emailInput = driver.FindElement(By.XPath("//input[@id='Email']"));
			emailInput.Clear();
			emailInput.SendKeys(email.Replace("@", ""));
			timespan.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"))).Click();
			string tooltip = driver.FindElement(By.XPath("//input[@id='Email']")).GetAttribute("validationMessage");
			//get the parent of the email input and search for a class named field-validation-valid
			Assert.That(tooltip, Is.EqualTo("Inclua um \"@\" no endereço de e-mail. \"raphael.mota14hotmail.com\" está com um \"@\" faltando."));
		}


		[Test]
		public void CT05AlteraçãoDeCadastroComEmailInvalidoSubmit()
		{

			//ACESSA O MENU DE FUNCIONÁRIOS
			driver.Navigate().GoToUrl("https://admin.ditecdistribuidora.com.br/");
			driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
			driver.FindElement(By.Id("Usuario")).Click();
			driver.FindElement(By.Id("Usuario")).SendKeys("suporte@forsh.com.br");
			driver.FindElement(By.Id("Senha")).SendKeys("1234");
			driver.FindElement(By.CssSelector(".btn")).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Dashboard Admin"))).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Distribuidoras"))).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Funcionários"))).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("td")));

			//TESTA SE HÁ O CADASTRO DE TESTE (DEFINIDO PELA VARIÁVEL email) E ACESSA A EDIÇÃO DO CADASTRO
			try
			{
				var row = timespan.Until(ExpectedConditions.ElementExists(By.XPath(string.Format("//tr/td[contains(text(), '{0}')]//parent::tr", email))));
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			catch (InvalidSelectorException)
			{

				throw new Exception("Botão de Edição ou cadastro não encontrado na página");
			}

			IWebElement emailInput = driver.FindElement(By.XPath("//input[@id='Email']"));
			emailInput.Clear();
			emailInput.SendKeys(email.Replace("@", ""));
			timespan.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"))).Submit();
			string errorMessage = emailInput.GetAttribute("validationMessage");
			//*[@id="page-wrapper"]/div[3]/div
			Assert.That(errorMessage, Is.EqualTo("Inclua um \"@\" no endereço de e-mail. \"raphael.mota14hotmail.com\" está com um \"@\" faltando."));
		}


		[Test]
		public void CT06AlteraçãoDeCadastroComSenhaInvalida()
		{
			LoginAndAccess();
			timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("td")));

			try
			{
				var row = timespan.Until(ExpectedConditions.ElementExists(By.XPath(string.Format("//tr/td[contains(text(), '{0}')]//parent::tr", email))));
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			catch (InvalidSelectorException)
			{

				throw new Exception("Botão de Edição ou cadastro não encontrado na página");
			}
			driver.FindElement(By.Id("Ativo")).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='alterarSenha']"))).Click();
			timespan.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='Ativo']"))).Click();
			
			IWebElement passwordInput = driver.FindElement(By.XPath("//*[@id='Senha']"));
			passwordInput.SendKeys("1");
			Console.WriteLine();
			timespan.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"))).Click();
			string message = timespan.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='page-wrapper']/div[3]/div"))).Text;
			Assert.That(message, Is.EqualTo 
				("×\\r\\nRegistro salvo com sucesso!"));
		}

		[Test]
		public void CT10ExclusaoDeCadastroExistente()
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
			IWebElement search = timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("#iptSearch")));
			search.Click();

			string email = "raphael.mota14@hotmail.com";

			for (int i = 0; i < email.Length; i++)
			{
				search.SendKeys(email[i].ToString());
				Thread.Sleep(10);
			}

			var button1 = driver.FindElement(By.CssSelector("button#btnSearch > i[class='fa fa-search']"));
			button1.Click();

			timespan.Until(ExpectedConditions.ElementExists((By.CssSelector(".btn.btn-sm.btn-danger")))).Click();
			IWebElement confirmButton = timespan.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Sim')]")));
			confirmButton.Click();
			search.Clear();
			driver.Navigate().Refresh();
			bool assertion = (driver.PageSource.Contains(email));
			Assert.That(!assertion);

			//driver.FindElement(By.CssSelector(".sweet-confirm")).Click();
			//Assert.That(driver.FindElement(By.CssSelector(".toast-message")).Text, Is.EqualTo("Registro excluido com sucesso!"));
		}



	}
}
