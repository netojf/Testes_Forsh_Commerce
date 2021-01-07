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
		//private IWebDriver TestTools.driver;
		//public IDictionary<string, object> vars { get; private set; }
		//private IJavaScriptExecutor js;
		//WebDriverWait TestTools.timespan;
		string email = "teste@teste.com";

		//[SetUp]
		//public void SetUp()
		//{
		//	TestTools.driver = new ChromeDriver();
		//	js = (IJavaScriptExecutor)TestTools.driver;
		//	vars = new Dictionary<string, object>();
		//	TestTools.timespan = new WebDriverWait(TestTools.driver, TimeSpan.FromSeconds(4));
		//}

		[TearDown]
		protected void TearDown()
		{
			ExcluirElemento("teste1@teste.com"); 
			TestTools.driver.Quit();
		}


		[Test]
		public void CTI01CadastroDeFuncionarioCamposVazios()
		{

			TestTools.LoginAndAccess();

			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Dashboard Admin"))).Click();

			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Distribuidoras"))).Click();
			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Funcionários"))).Click();
			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("button[title='Cadastrar funcionário']"))).Click();

			TestTools.driver.FindElement(By.CssSelector(".btn-success")).Click();
			Assert.That(TestTools.driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("×\\\\nVerifique os dados informados."));
			Assert.That(TestTools.driver.FindElement(By.CssSelector(".form-group:nth-child(2) .field-validation-error")).Text, Is.EqualTo("Preencha o campo Nome"));
			Assert.That(TestTools.driver.FindElement(By.CssSelector(".form-group:nth-child(3) .field-validation-error")).Text, Is.EqualTo("The value \\\'Selecione\\\' is not valid for Perfil do Usuário."));
			Assert.That(TestTools.driver.FindElement(By.CssSelector(".form-group:nth-child(4) .field-validation-error")).Text, Is.EqualTo("Preencha o campo Nome de Usuário"));
			Assert.That(TestTools.driver.FindElement(By.CssSelector(".form-group:nth-child(5) .field-validation-error")).Text, Is.EqualTo("Preencha o campo E-mail"));
			Assert.That(TestTools.driver.FindElement(By.CssSelector("#divSenha .field-validation-error")).Text, Is.EqualTo("Preencha o campo Senha"));
		}


		[Test]
		public void CTI02CadastroDeFuncionariosCamposValidos()
		{
			TestTools.LoginAndAccess();

			AcessoDashboardFuncionarios(); 

			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("button[title='Cadastrar funcionário']"))).Click();

			PreenchimentoDeFormulario("teste", 1, "Administrador Teste", email, "teste123", true); 

			string message = TestTools.driver.FindElement(By.CssSelector(".alert")).Text;
			Assert.That(message == "×\r\nRegistro salvo com sucesso!");
		}


		[Test]
		public void CTI03CadastroDeFuncionarioEmailExistente()
		{
			TestTools.LoginAndAccess();

			AcessoDashboardFuncionarios();

			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("button[title='Cadastrar funcionário']"))).Click();

			PreenchimentoDeFormulario("teste1", 1, "Administrador Teste1", email, "teste1234", true);

			var emailElement = TestTools.FindElement(By.XPath("//input[@id='Email']"));
			string tooltip = emailElement.GetAttribute("ValidationMessage");
			Assert.IsNotEmpty(tooltip);

			var alertTxt = TestTools.FindElement(By.CssSelector(".alert")).Text; 
			Assert.That(TestTools.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("×\r\nVerifique os dados informados."));


		}

		[Test]
		public void CTI04CadastroDeFuncionarioNomeExistente()
		{
			TestTools.LoginAndAccess();
			AcessoDashboardFuncionarios();

			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("button[title='Cadastrar funcionário']"))).Click();

			PreenchimentoDeFormulario("teste", 1, "Administrador Teste1", "teste1@teste.com", "teste1234", true);

			string message = TestTools.driver.FindElement(By.CssSelector(".alert")).Text;
			Assert.That(message == "×\r\nRegistro salvo com sucesso!");

		}

		[Test]
		public void CTI05CadastroDeFuncionarioNomeDeUsuarioExistente()
		{

			TestTools.LoginAndAccess();
			AcessoDashboardFuncionarios();

			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("button[title='Cadastrar funcionário']"))).Click();

			PreenchimentoDeFormulario("teste1", 1, "Administrador Teste", "teste1@teste.com", "teste1234", true);

			var emailElement = TestTools.FindElement(By.XPath("//*[@id='NomeUsuario']"));
			string tooltip = emailElement.GetAttribute("ValidationMessage");
			Assert.IsNotEmpty(tooltip);

			
			Assert.That(TestTools.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("×\r\nVerifique os dados informados."));

		}

		[Test]
		public void CTI06CadastroDeFuncionarioSenhaExistente()
		{

			TestTools.LoginAndAccess();
			AcessoDashboardFuncionarios();

			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("button[title='Cadastrar funcionário']"))).Click();

			PreenchimentoDeFormulario("teste1", 1, "Administrador Teste1", "teste1@teste.com", "teste123", true);

			

			string message = TestTools.driver.FindElement(By.CssSelector(".alert")).Text;
			Assert.That(message == "×\r\nRegistro salvo com sucesso!");
		}

		[Test]
		public void CTI07CadastroDeFuncionarioNomeVazio()
		{

			TestTools.LoginAndAccess();
			AcessoDashboardFuncionarios();

			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("button[title='Cadastrar funcionário']"))).Click();

			PreenchimentoDeFormulario("", 1, "Administrador Teste1", "teste1@teste.com", "teste1234", true);

			var emailElement = TestTools.FindElement(By.XPath("//*[@id='Nome']"));
			string tooltip = emailElement.GetAttribute("ValidationMessage");
			Assert.IsNotEmpty(tooltip);


			Assert.That(TestTools.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("×\r\nVerifique os dados informados."));
		}

		[Test]
		public void CTI08CadastroDeFuncionarioPerfilDeUsuarioNSelecionado()
		{
			TestTools.LoginAndAccess();
			AcessoDashboardFuncionarios();

			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("button[title='Cadastrar funcionário']"))).Click();

			PreenchimentoDeFormulario("teste1", 0, "Administrador Teste1", "teste1@teste.com", "teste1234", true);

			var emailElement = TestTools.FindElement(By.XPath("//*[@id='PerfilFuncionario']"));
			string tooltip = emailElement.GetAttribute("ValidationMessage");
			Assert.IsNotEmpty(tooltip);


			Assert.That(TestTools.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("×\r\nVerifique os dados informados."));
		}

		[Test]
		public void CTI09CadastroDeFuncionarioNomeDeUsuarioVazio()
		{

			TestTools.LoginAndAccess();
			AcessoDashboardFuncionarios();

			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("button[title='Cadastrar funcionário']"))).Click();

			PreenchimentoDeFormulario("teste1", 1, "", "teste1@teste.com", "teste1234", true);

			var field = TestTools.FindElement(By.XPath("//*[@id='NomeUsuario']"));
			string tooltip = field.GetAttribute("ValidationMessage");
			Assert.IsNotEmpty(tooltip);


			Assert.That(TestTools.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("×\r\nVerifique os dados informados."));
		}

		[Test]
		public void CTI10CadastroDeFuncionarioEmailVazio()
		{
			TestTools.LoginAndAccess();
			AcessoDashboardFuncionarios();

			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("button[title='Cadastrar funcionário']"))).Click();

			PreenchimentoDeFormulario("teste1", 1, "Administrador Teste1", "", "teste1234", true);

			var field = TestTools.FindElement(By.XPath("//*[@id='NomeUsuario']"));
			string tooltip = field.GetAttribute("ValidationMessage");
			Assert.IsNotEmpty(tooltip);


			Assert.That(TestTools.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("×\r\nVerifique os dados informados."));
		}

		[Test]
		public void CTI11CadastroDeFuncionarioSenhaVazia()
		{

			TestTools.LoginAndAccess();
			AcessoDashboardFuncionarios();

			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("button[title='Cadastrar funcionário']"))).Click();

			PreenchimentoDeFormulario("teste1", 1, "Administrador Teste1", "teste1@teste.com", "", true);

			var field = TestTools.FindElement(By.XPath("//*[@id='Senha']"));
			string tooltip = field.GetAttribute("ValidationMessage");
			Assert.IsNotEmpty(tooltip);


			Assert.That(TestTools.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("×\r\nVerifique os dados informados."));
		}

		#region Em desenvolvimento

		[Test]
		public void CTI012AlteraçãoDeCadastroComEmailInvalidoClick()
		{

			//ACESSA O MENU DE FUNCIONÁRIOS


			//TESTA SE HÁ O CADASTRO DE TESTE (DEFINIDO PELA VARIÁVEL email) E ACESSA A EDIÇÃO DO CADASTRO
			try
			{
				var row = TestTools.timespan.Until(ExpectedConditions.ElementExists(By.XPath(string.Format("//tr/td[contains(text(), '{0}')]//parent::tr", email))));
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			catch (InvalidSelectorException)
			{

				throw new Exception("Botão de Edição ou cadastro não encontrado na página");
			}

			IWebElement emailInput = TestTools.driver.FindElement(By.XPath("//input[@id='Email']"));
			emailInput.Clear();
			emailInput.SendKeys(email.Replace("@", ""));
			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"))).Click();
			string tooltip = TestTools.driver.FindElement(By.XPath("//input[@id='Email']")).GetAttribute("validationMessage");
			//get the parent of the email input and search for a class named field-validation-valid
			Assert.That(tooltip, Is.EqualTo("Inclua um \"@\" no endereço de e-mail. \"raphael.mota14hotmail.com\" está com um \"@\" faltando."));
		}


		[Test]
		public void CTI13AlteraçãoDeCadastroComEmailInvalidoSubmit()
		{

			//ACESSA O MENU DE FUNCIONÁRIOS
			TestTools.driver.Navigate().GoToUrl("https://admin.ditecdistribuidora.com.br/");
			TestTools.driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
			TestTools.driver.FindElement(By.Id("Usuario")).Click();
			TestTools.driver.FindElement(By.Id("Usuario")).SendKeys("suporte@forsh.com.br");
			TestTools.driver.FindElement(By.Id("Senha")).SendKeys("1234");
			TestTools.driver.FindElement(By.CssSelector(".btn")).Click();
			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Dashboard Admin"))).Click();
			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Distribuidoras"))).Click();
			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.LinkText("Funcionários"))).Click();
			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("td")));

			//TESTA SE HÁ O CADASTRO DE TESTE (DEFINIDO PELA VARIÁVEL email) E ACESSA A EDIÇÃO DO CADASTRO
			try
			{
				var row = TestTools.timespan.Until(ExpectedConditions.ElementExists(By.XPath(string.Format("//tr/td[contains(text(), '{0}')]//parent::tr", email))));
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			catch (InvalidSelectorException)
			{

				throw new Exception("Botão de Edição ou cadastro não encontrado na página");
			}

			IWebElement emailInput = TestTools.driver.FindElement(By.XPath("//input[@id='Email']"));
			emailInput.Clear();
			emailInput.SendKeys(email.Replace("@", ""));
			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"))).Submit();
			string errorMessage = emailInput.GetAttribute("validationMessage");
			//*[@id="page-wrapper"]/div[3]/div
			Assert.That(errorMessage, Is.EqualTo("Inclua um \"@\" no endereço de e-mail. \"raphael.mota14hotmail.com\" está com um \"@\" faltando."));
		}


		[Test]
		public void CTI14AlteraçãoDeCadastroComSenhaInvalida()
		{
			TestTools.LoginAndAccess();
			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("td")));

			try
			{
				var row = TestTools.timespan.Until(ExpectedConditions.ElementExists(By.XPath(string.Format("//tr/td[contains(text(), '{0}')]//parent::tr", email))));
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			catch (InvalidSelectorException)
			{

				throw new Exception("Botão de Edição ou cadastro não encontrado na página");
			}
			TestTools.driver.FindElement(By.Id("Ativo")).Click();
			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='alterarSenha']"))).Click();
			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='Ativo']"))).Click();

			IWebElement passwordInput = TestTools.driver.FindElement(By.XPath("//*[@id='Senha']"));
			passwordInput.SendKeys("1");
			Console.WriteLine();
			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"))).Click();
			try
			{
				string message = TestTools.timespan.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='page-wrapper']/div[3]/div"))).Text;
				Assert.IsFalse(message == "×\r\nRegistro salvo com sucesso!");
			}
			catch (ElementNotSelectableException)
			{
				//CASO NÃO ENCONTRE A MENSAGEM DE SUCESSO
				Assert.IsTrue(true);
			}
		}


		[Test]
		public void CTI15AlteraçãoDeCadastroComSenhaValida()
		{
			TestTools.LoginAndAccess();
			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.CssSelector("td")));

			try
			{
				var row = TestTools.timespan.Until(ExpectedConditions.ElementExists(By.XPath(string.Format("//tr/td[contains(text(), '{0}')]//parent::tr", email))));
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			catch (InvalidSelectorException)
			{

				throw new Exception("Botão de Edição ou cadastro não encontrado na página");
			}
			TestTools.driver.FindElement(By.Id("Ativo")).Click();
			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='alterarSenha']"))).Click();
			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='Ativo']"))).Click();

			IWebElement passwordInput = TestTools.driver.FindElement(By.XPath("//*[@id='Senha']"));
			passwordInput.SendKeys("1");
			Console.WriteLine();
			TestTools.timespan.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"))).Click();
			try
			{
				string message = TestTools.timespan.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='page-wrapper']/div[3]/div"))).Text;
				Assert.IsFalse(message == "×\r\nRegistro salvo com sucesso!");
			}
			catch (ElementNotSelectableException)
			{
				//CASO NÃO ENCONTRE A MENSAGEM DE SUCESSO
				Assert.IsTrue(true);
			}
		}


		#endregion
		[Test]
		public void CTI19ExclusaoDeCadastroExistente()
		{
			TestTools.LoginAndAccess();

			AcessoDashboardFuncionarios();
			

			IWebElement search = TestTools.FindElement(By.CssSelector("#iptSearch"));
			search.Click();

			string email = this.email;

			for (int i = 0; i < email.Length; i++)
			{
				search.SendKeys(email[i].ToString());
				Thread.Sleep(10);
			}

			var button1 = TestTools.driver.FindElement(By.CssSelector("button#btnSearch > i[class='fa fa-search']"));
			button1.Click();

			TestTools.timespan.Until(ExpectedConditions.ElementExists((By.CssSelector(".btn.btn-sm.btn-danger")))).Click();
			IWebElement confirmButton = TestTools.timespan.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Sim')]")));
			confirmButton.Click();
			search.Clear();
			TestTools.driver.Navigate().Refresh();
			bool assertion = (TestTools.driver.PageSource.Contains(email));
			Assert.That(!assertion);

			//TestTools.driver.FindElement(By.CssSelector(".sweet-confirm")).Click();
			//Assert.That(TestTools.driver.FindElement(By.CssSelector(".toast-message")).Text, Is.EqualTo("Registro excluido com sucesso!"));
		}
		
		/// <summary>
		/// Acessa o Dashboard de funcionários e checa a tabela está visível
		/// </summary>
		void AcessoDashboardFuncionarios()
		{
			TestTools.FindElement(By.XPath("//*[. = 'Dashboard Admin']")).Click();
			TestTools.FindElement(By.XPath("//*[. = 'Distribuidoras']")).Click();
			Thread.Sleep(1000); 
			TestTools.FindElement(By.XPath("//*[. = 'Funcionários']")).Click();
			TestTools.FindElement(By.XPath("//h2[. = 'Funcionários']"));
		}

		bool ExcluirElemento(string email)
		{
			TestTools.driver.Navigate().GoToUrl("https://admin.ditecdistribuidora.com.br/funcionario");
			IWebElement searchbar = TestTools.FindElement(By.CssSelector("#iptSearch"));
			searchbar.Click();

			string search = email;

			for (int i = 0; i < search.Length; i++)
			{
				searchbar.SendKeys(search[i].ToString());
				Thread.Sleep(10);
			}

			var button1 = TestTools.FindElement(By.CssSelector("button#btnSearch > i[class='fa fa-search']"));
			button1.Click();

			try
			{
				var row = TestTools.FindElement(By.XPath(string.Format("//tr/td[contains(text(), '{0}')]//parent::tr", email)));
				row.FindElement(By.CssSelector(".btn.btn-sm.btn-danger")).Click();
			}
			catch (StaleElementReferenceException)
			{

				var row = TestTools.FindElement(By.XPath(string.Format("//tr/td[contains(text(), '{0}')]//parent::tr", email)));
				row.FindElement(By.CssSelector(".btn.btn-sm.btn-danger")).Click();
			}
			catch(NoSuchElementException)
			{
				return false; 
			}
			catch(WebDriverTimeoutException)
			{
				return false; 
			}

			IWebElement confirmButton = TestTools.timespan.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Sim')]")));

			confirmButton.Click();
			searchbar.Clear();
			TestTools.driver.Navigate().Refresh();

			try
			{
				TestTools.FindElement(By.XPath(string.Format("//*[. = '{0}']", email)));
			}
			catch (NoSuchElementException)
			{
				return true;
			}
			catch(WebDriverTimeoutException)
			{
				return true; 
			}

			return false; 
		}

		/// <summary>
		/// Preenchimento de Formulário de cadastro de Funcionário
		/// </summary>
		/// <param name="nome"></param>
		/// <param name="perfilDeUsuario"> um número de 0 a 2 
		/// 0 - Selecione
		/// 1 - Administrador
		/// 2 - Analista de Cadastro</param>
		/// <param name="nomeDeUsuario"></param>
		/// <param name="email"></param>
		/// <param name="senha"></param>
		/// <param name="ativo"></param>
		void PreenchimentoDeFormulario(string nome, int perfilDeUsuario, string nomeDeUsuario, string email, string senha, bool ativo )
		{
			//NOME
			TestTools.FindElement(By.Id("Nome")).Click();
			TestTools.FindElement(By.Id("Nome")).SendKeys(nome);

			//PERFIL DE USUARIO
			string perfil = "";
			switch (perfilDeUsuario)
			{
				case 0:
					perfil = "Selecione";
					break;
				case 1:
					perfil = "Administrador";
					break;
				case 2:
					perfil = "AnalistaCadastro";
					break;
				default:
					throw new Exception("Valor de Perfil de Usuario deve estar no intervalo de 0 a 2"); 
					break;
			}
			TestTools.FindElement(By.Id("PerfilFuncionario")).Click();
			var dropdown = TestTools.FindElement(By.Id("PerfilFuncionario"));
			dropdown.FindElement(By.XPath(string.Format("//option[. = '{0}']", perfil))).Click();

			//NOME DE USUARIO
			TestTools.FindElement(By.Id("NomeUsuario")).Click();
			TestTools.FindElement(By.Id("NomeUsuario")).SendKeys(nomeDeUsuario);

			//EMAIL
			TestTools.FindElement(By.Id("Email")).Click();
			TestTools.FindElement(By.Id("Email")).SendKeys(email);

			//SENHA 
			TestTools.FindElement(By.Id("Senha")).SendKeys(senha);

			//ATIVO
			if (ativo)
			{
				TestTools.FindElement(By.Id("Ativo")).Click();
			}

			//SUMISSÃO POR BOTÃO
			TestTools.FindElement(By.CssSelector(".btn-success")).Click();
		}
	}

}
