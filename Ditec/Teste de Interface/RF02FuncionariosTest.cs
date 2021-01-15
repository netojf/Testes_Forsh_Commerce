using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using Tools;

namespace Ditec.Teste_de_Interface
{
	[TestFixture]
	public class RF02FuncionariosTest
	{
		string email = "teste@teste.com";
		string emailTeste = "teste1@teste.com";
		public static IWebDriver driver;
		public static IJavaScriptExecutor js;

		[SetUp]
		public void SetUp()
		{
			driver = new ChromeDriver();
			js = (IJavaScriptExecutor)driver;


			driver.Navigate().GoToUrl("https://admin.ditecdistribuidora.com.br/");
			driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
			driver.FindElement(By.Id("Usuario")).Click();
			driver.FindElement(By.Id("Usuario")).SendKeys("suporte@forsh.com.br");
			driver.FindElement(By.Id("Senha")).SendKeys("1234");
			driver.FindElement(By.CssSelector(".btn")).Click();

			AcessoDashboardFuncionarios(); 
		}

		[TearDown]
		protected void TearDown()
		{
			ExcluirElemento(emailTeste); 
			driver.Quit();
		}


		[Test]
		public void CTI01CadastroDeFuncionarioCamposVazios()
		{


			TestTools.WaitUntilElementExists(By.CssSelector("button[title='Cadastrar funcionário']"), driver).Click();
			driver.FindElement(By.CssSelector(".btn-success")).Click();
			
			//todo: captura dos avisos quando consertar o erro 404
			Assert.That(driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("×\\\\nVerifique os dados informados."));
			Assert.That(driver.FindElement(By.CssSelector(".form-group:nth-child(2) .field-validation-error")).Text, Is.EqualTo("Preencha o campo Nome"));
			Assert.That(driver.FindElement(By.CssSelector(".form-group:nth-child(3) .field-validation-error")).Text, Is.EqualTo("The value \\\'Selecione\\\' is not valid for Perfil do Usuário."));
			Assert.That(driver.FindElement(By.CssSelector(".form-group:nth-child(4) .field-validation-error")).Text, Is.EqualTo("Preencha o campo Nome de Usuário"));
			Assert.That(driver.FindElement(By.CssSelector(".form-group:nth-child(5) .field-validation-error")).Text, Is.EqualTo("Preencha o campo E-mail"));
			Assert.That(driver.FindElement(By.CssSelector("#divSenha .field-validation-error")).Text, Is.EqualTo("Preencha o campo Senha"));
		}


		[Test]
		public void CTI02CadastroDeFuncionariosCamposValidos()
		{
			AcessoCadastro(); 
			string message = TestTools.WaitUntilElementExists(By.CssSelector(".alert"),driver).Text;
			Assert.That(message == "×\r\nRegistro salvo com sucesso!");
		}


		[Test]
		public void CTI03CadastroDeFuncionarioEmailExistente()
		{
			AcessoCadastro("teste1", 1, "Administrador Teste1", email, "teste1234", true);

			var emailElement = TestTools.WaitUntilElementExists(By.XPath("//input[@id='Email']"),driver);
			string tooltip = emailElement.GetAttribute("ValidationMessage");
			Assert.IsNotEmpty(tooltip);

			Assert.That(TestTools.WaitUntilElementExists(By.CssSelector(".alert"),driver).Text, Is.EqualTo("×\r\nVerifique os dados informados."));
		}


		[Test]
		public void CTI04CadastroDeFuncionarioNomeExistente()
		{

			AcessoCadastro("teste", 1, "Administrador Teste1", "teste1@teste.com", "teste1234", true);

			string message = driver.FindElement(By.CssSelector(".alert")).Text;
			Assert.That(message == "×\r\nRegistro salvo com sucesso!");

		}


		[Test]
		public void CTI05CadastroDeFuncionarioNomeDeUsuarioExistente()
		{
			AcessoCadastro("teste1", 1, "Administrador Teste", "teste1@teste.com", "teste1234", true);

			var emailElement = TestTools.WaitUntilElementExists(By.XPath("//*[@id='NomeUsuario']"),driver);
			string tooltip = emailElement.GetAttribute("ValidationMessage");
			Assert.IsNotEmpty(tooltip);

			
			Assert.That(TestTools.WaitUntilElementExists(By.CssSelector(".alert"),driver).Text, Is.EqualTo("×\r\nVerifique os dados informados."));
		}


		[Test]
		public void CTI06CadastroDeFuncionarioSenhaExistente()
		{

			AcessoCadastro("teste1", 1, "Administrador Teste1", "teste1@teste.com", "teste123", true);

			string message = driver.FindElement(By.CssSelector(".alert")).Text;
			Assert.That(message == "×\r\nRegistro salvo com sucesso!");
		}


		[Test]
		public void CTI07CadastroDeFuncionarioNomeVazio()
		{
			
			AcessoCadastro("", 1, "Administrador Teste1", "teste1@teste.com", "teste1234", true);

			var emailElement = TestTools.WaitUntilElementExists(By.XPath("//*[@id='Nome']"),driver);
			string tooltip = emailElement.GetAttribute("ValidationMessage");
			Assert.IsNotEmpty(tooltip);


			Assert.That(TestTools.WaitUntilElementExists(By.CssSelector(".alert"),driver).Text, Is.EqualTo("×\r\nVerifique os dados informados."));
		}


		[Test]
		public void CTI08CadastroDeFuncionarioPerfilDeUsuarioNSelecionado()
		{
			AcessoCadastro("teste1", 0, "Administrador Teste1", "teste1@teste.com", "teste1234", true);

			var emailElement = TestTools.WaitUntilElementExists(By.XPath("//*[@id='PerfilFuncionario']"),driver);
			string tooltip = emailElement.GetAttribute("ValidationMessage");
			Assert.IsNotEmpty(tooltip);


			Assert.That(TestTools.WaitUntilElementExists(By.CssSelector(".alert"),driver).Text, Is.EqualTo("×\r\nVerifique os dados informados."));
		}


		[Test]
		public void CTI09CadastroDeFuncionarioNomeDeUsuarioVazio()
		{

			AcessoCadastro("teste1", 1, "", "teste1@teste.com", "teste1234", true);

			var field = TestTools.WaitUntilElementExists(By.XPath("//*[@id='NomeUsuario']"),driver);
			string tooltip = field.GetAttribute("ValidationMessage");
			Assert.IsNotEmpty(tooltip);


			Assert.That(TestTools.WaitUntilElementExists(By.CssSelector(".alert"),driver).Text, Is.EqualTo("×\r\nVerifique os dados informados."));
		}


		[Test]
		public void CTI10CadastroDeFuncionarioEmailVazio()
		{
			AcessoCadastro("teste1", 1, "Administrador Teste1", "", "teste1234", true);

			var field =TestTools.WaitUntilElementExists(By.XPath("//*[@id='NomeUsuario']"),driver);
			string tooltip = field.GetAttribute("ValidationMessage");
			Assert.IsNotEmpty(tooltip);


			Assert.That(TestTools.WaitUntilElementExists(By.CssSelector(".alert"),driver).Text, Is.EqualTo("×\r\nVerifique os dados informados."));
		}


		[Test]
		public void CTI11CadastroDeFuncionarioSenhaVazia()
		{
			AcessoCadastro("teste1", 1, "Administrador Teste1", "teste1@teste.com", "", true);

			var field = TestTools.WaitUntilElementExists(By.XPath("//*[@id='Senha']"),driver);
			string tooltip = field.GetAttribute("ValidationMessage");
			Assert.IsNotEmpty(tooltip);


			Assert.That(TestTools.WaitUntilElementExists(By.CssSelector(".alert"),driver).Text, Is.EqualTo("×\r\nVerifique os dados informados."));
		}

	
		[Test]
		public void CTI012AlteracaoDeCadastroEmailValido()
		{
			AcessoCadastro("Teste1", 1, "Administrar Teste1", "teste2@teste.com", "teste1234", true);

			var row = RetornarCadastro("teste2@teste.com");
			if (row != null)
			{
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			else
			{
				emailTeste = "teste2@teste.com"; 
				Assert.IsTrue(false);
			}


			IWebElement emailInput = driver.FindElement(By.XPath("//input[@id='Email']"));
			emailInput.Clear();
			emailInput.SendKeys("teste1@teste.com");

			TestTools.WaitUntilElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"),driver).Click();
			string message = TestTools.WaitUntilElementExists(By.CssSelector(".alert"),driver).Text;
			Assert.That(message == "×\r\nRegistro salvo com sucesso!");
		}


		[Test]
		public void CTI013AlteracaoDeCadastroNomeDeUsuarioValido()
		{
			AcessoCadastro("Teste1", 1, "Administrar Teste1", "teste1@teste.com", "teste1234", true);

			var row = RetornarCadastro("teste1@teste.com");
			if (row != null)
			{
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			else
			{
				Assert.IsTrue(false);
			}


			IWebElement emailInput = driver.FindElement(By.XPath("//*[@id='NomeUsuario']"));
			emailInput.Clear();
			emailInput.SendKeys("Administrador Teste2");

			TestTools.WaitUntilElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"), driver).Click();
			string message = TestTools.WaitUntilElementExists(By.CssSelector(".alert"), driver).Text;
			Assert.That(message == "×\r\nRegistro salvo com sucesso!");
		}


		[Test]
		public void CTI014AlteracaoDeCadastroNomeValido()
		{
			AcessoCadastro("Teste1", 1, "Administrar Teste1", "teste1@teste.com", "teste1234", true);

			var row = RetornarCadastro("teste1@teste.com");
			if (row != null)
			{
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			else
			{
				Assert.IsTrue(false);
			}


			IWebElement emailInput = driver.FindElement(By.XPath("//*[@id='Nome']"));
			emailInput.Clear();
			emailInput.SendKeys("Teste2");

			TestTools.WaitUntilElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"), driver).Click();
			string message = TestTools.WaitUntilElementExists(By.CssSelector(".alert"), driver).Text;
			Assert.That(message == "×\r\nRegistro salvo com sucesso!");
		}


		[Test]
		public void CTI015AlteracaoDeCadastroPerfilDeUsuarioAnalista()
		{
			AcessoCadastro("Teste1", 1, "Administrar Teste1", "teste1@teste.com", "teste1234", true);

			var row = RetornarCadastro("teste1@teste.com");
			if (row != null)
			{
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			else
			{
				Assert.IsTrue(false);
			}

			TestTools.WaitUntilElementExists(By.Id("PerfilFuncionario"), driver).Click();
			var dropdown = TestTools.WaitUntilElementExists(By.Id("PerfilFuncionario"), driver);
			dropdown.FindElement(By.XPath(string.Format("//option[. = '{0}']", "AnalistaCadastro"))).Click();

			TestTools.WaitUntilElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"), driver).Click();
			string message = TestTools.WaitUntilElementExists(By.CssSelector(".alert"), driver).Text;
			Assert.That(message == "×\r\nRegistro salvo com sucesso!");
		}


		[Test]
		public void CTI16AlteraçãoDeCadastroSenhaValida()
		{

			AcessoCadastro("Teste1", 1, "Administrar Teste1", "teste1@teste.com", "teste1234", true);

			var row = RetornarCadastro("teste1@teste.com");
			if (row != null)
			{
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			else
			{
				Assert.IsTrue(false);
			}

			TestTools.WaitUntilElementExists(By.XPath("//*[@id='alterarSenha']"), driver).Click();

			IWebElement passwordInput = TestTools.WaitUntilElementInteractable(By.XPath("//*[@id='Senha']"), driver);
			passwordInput.SendKeys("Senha1234");
			TestTools.WaitUntilElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"), driver).Click();

			try
			{
				string message = TestTools.WaitUntilElementExists(By.XPath("//*[@id='page-wrapper']/div[3]/div"), driver).Text;
				Assert.IsTrue(message == "×\r\nRegistro salvo com sucesso!");
			}
			catch (ElementNotSelectableException)
			{
				Assert.IsTrue(false);
			}
		}


		[Test]
		public void CTI017AlteracaoDeCadastroNomeInvalido()
		{
			AcessoCadastro("Teste1", 1, "Administrar Teste1", "teste1@teste.com", "teste1234", true);

			var row = RetornarCadastro("teste1@teste.com");
			if (row != null)
			{
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			else
			{
				Assert.IsTrue(false);
			}


			IWebElement emailInput = driver.FindElement(By.XPath("//*[@id='Nome']"));
			emailInput.Clear();
			emailInput.SendKeys("T*-+.");

			TestTools.WaitUntilElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"), driver).Click();


			string tooltip = emailInput.GetAttribute("validationMessage");
			Assert.IsNotEmpty(tooltip);
		}


		[Test]
		public void CTI018AlteracaoDeCadastroNomeDeUsuarioInvalido()
		{
			AcessoCadastro("Teste1", 1, "Administrar Teste1", "teste1@teste.com", "teste1234", true);

			var row = RetornarCadastro("teste1@teste.com");
			if (row != null)
			{
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			else
			{
				Assert.IsTrue(false);
			}


			IWebElement emailInput = driver.FindElement(By.XPath("//*[@id='NomeUsuario']"));
			emailInput.Clear();
			emailInput.SendKeys("t*/-");

			TestTools.WaitUntilElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"), driver).Click();

			string tooltip = emailInput.GetAttribute("validationMessage");
			Assert.IsNotEmpty(tooltip);

		}


		[Test]
		public void CTI019AlteracaoDeCadastroEmailInvalido()
		{

			AcessoCadastro("Teste1", 1, "Administrar Teste1", "teste2@teste.com", "teste1234", true);

			var row = RetornarCadastro("teste2@teste.com");
			emailTeste = "teste2@teste.com"; 
			if (row != null)
			{
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			else
			{
				Assert.IsTrue(false);
			}


			IWebElement emailInput = driver.FindElement(By.XPath("//input[@id='Email']"));
			emailInput.Clear();
			emailInput.SendKeys("teste1teste.com");

			TestTools.WaitUntilElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"),driver).Click();
			string tooltip = driver.FindElement(By.XPath("//input[@id='Email']")).GetAttribute("validationMessage");

			Assert.IsNotEmpty(tooltip);
		}


		[Test]
		public void CTI20AlteraçãoDeCadastroSenhaInvalida()
		{
			AcessoCadastro("Teste1", 1, "Administrar Teste1", "teste1@teste.com", "teste1234", true);

			var row = RetornarCadastro("teste1@teste.com");
			if (row != null)
			{
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			else
			{
				Assert.IsTrue(false);
			}

			TestTools.WaitUntilElementExists(By.XPath("//*[@id='alterarSenha']"),driver).Click();

			IWebElement passwordInput = TestTools.WaitUntilElementExists(By.XPath("//*[@id='Senha']"),driver);
			passwordInput.SendKeys("1");

			TestTools.WaitUntilElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"),driver).Click();

			string tooltip = passwordInput.GetAttribute("validationMessage");
			Assert.IsNotEmpty(tooltip);
		}

		[Test]
		public void CTI21AlteraçãoDeCadastroPerfilNãoSelecionado()
		{
			AcessoCadastro("Teste1", 1, "Administrar Teste1", "teste1@teste.com", "teste1234", true);

			var row = RetornarCadastro("teste1@teste.com");
			if (row != null)
			{
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			else
			{
				Assert.IsTrue(false);
			}
			
			TestTools.WaitUntilElementExists(By.Id("PerfilFuncionario"), driver).Click();
			var dropdown = TestTools.WaitUntilElementExists(By.Id("PerfilFuncionario"), driver);
			dropdown.FindElement(By.XPath(string.Format("//option[. = '{0}']", "Selecione"))).Click();

			TestTools.WaitUntilElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"), driver).Click();

			Assert.That(TestTools.WaitUntilElementExists(By.CssSelector(".alert"), driver).Text, Is.EqualTo("×\r\nVerifique os dados informados."));
		}


		[Test]
		public void CTI22AlteracaoDeCadastroParaInativo()
		{
			AcessoCadastro("Teste1", 1, "Administrar Teste1", "teste1@teste.com", "teste1234", true);

			var row = RetornarCadastro("teste1@teste.com");
			if (row != null)
			{
				row.FindElement(By.CssSelector("i[class='fa fa-pencil']")).Click();
			}
			else
			{
				Assert.IsTrue(false);
			}
			TestTools.WaitUntilElementExists(By.XPath("//*[@id='Ativo']"), driver).Click();

			TestTools.WaitUntilElementExists(By.XPath("//*[@id='page-wrapper']/div[4]/div/div/div/div[2]/form/div/div[8]/div/button/i"), driver).Click();

			try
			{
				string message = TestTools.WaitUntilElementExists(By.XPath("//*[@id='page-wrapper']/div[3]/div"), driver).Text;
				Assert.IsTrue(message == "×\r\nRegistro salvo com sucesso!");
			}
			catch (ElementNotSelectableException)
			{
				Assert.IsTrue(false);
			}
		}


		[Test]
		public void CTI23ExclusaoDeCadastroExistente()
		{
			ExcluirElemento(email);
			Assert.IsTrue(true);	
		}
		

		/// <summary>
		/// Acessa o Dashboard de funcionários e checa a tabela está visível
		/// </summary>
		void AcessoDashboardFuncionarios()
		{
			TestTools.WaitUntilElementExists(By.XPath("//*[. = 'Dashboard Admin']"), driver).Click();
			TestTools.WaitUntilElementExists(By.XPath("//*[. = 'Distribuidoras']"),driver).Click();
			TestTools.WaitUntilElementInteractable(By.XPath("//*[. = 'Funcionários']"),driver).Click();
			TestTools.WaitUntilElementExists(By.XPath("//h2[. = 'Funcionários']"),driver);
		}


		bool ExcluirElemento(string search)
		{
			driver.Navigate().GoToUrl("https://admin.ditecdistribuidora.com.br/funcionario");


			var entrada = RetornarCadastro(search);
			if (entrada!= null)
			{
				entrada.FindElement(By.CssSelector(".btn.btn-sm.btn-danger")).Click();
			}
			else
			{
				return false; 
			}

			TestTools.WaitUntilElementExists(By.XPath("//button[contains(text(), 'Sim')]"),driver).Click();

			driver.Navigate().Refresh();

			if (RetornarCadastro(search) != null)
			{
				throw new Exception("A exclusão do Cadastro falhou"); 
			}
			else
			{
				return true; 
			}
		}


		IWebElement RetornarCadastro(string search)
		{
			IWebElement searchbar = TestTools.WaitUntilElementExists(By.CssSelector("#iptSearch"),driver);
			searchbar.Click();

			searchbar.SendKeys(search);
			searchbar.Click();


			TestTools.WaitUntilElementExists(By.CssSelector("button#btnSearch > i[class='fa fa-search']"),driver).Click();

			try
			{
				return TestTools.WaitUntilElementExists(By.XPath(string.Format("//tr/td[contains(text(), '{0}')]//parent::tr", search)),driver);
			}
			catch (StaleElementReferenceException)
			{

				return TestTools.WaitUntilElementExists(By.XPath(string.Format("//tr/td[contains(text(), '{0}')]//parent::tr", search)), driver);
			}
			catch (WebDriverTimeoutException)
			{
				return null;
			}
		}


		void AcessoCadastro(string nome = "teste", int perfilDeUsuario = 1, string nomeDeUsuario = "Administrador Teste", string email = "teste@teste.com", string senha = "teste123", bool ativo = true)
		{
			TestTools.WaitUntilElementExists(By.CssSelector("button[title='Cadastrar funcionário']"), driver).Click();

			PreenchimentoDeFormulario(nome, perfilDeUsuario, nomeDeUsuario, email, senha, ativo);

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
			TestTools.WaitUntilElementExists(By.Id("Nome"),driver).Click();
			TestTools.WaitUntilElementExists(By.Id("Nome"),driver).SendKeys(nome);

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
			}
			TestTools.WaitUntilElementExists(By.Id("PerfilFuncionario"),driver).Click();
			var dropdown = TestTools.WaitUntilElementExists(By.Id("PerfilFuncionario"),driver);
			dropdown.FindElement(By.XPath(string.Format("//option[. = '{0}']", perfil))).Click();

			//NOME DE USUARIO
			TestTools.WaitUntilElementExists(By.Id("NomeUsuario"),driver).Click();
			TestTools.WaitUntilElementExists(By.Id("NomeUsuario"),driver).SendKeys(nomeDeUsuario);

			//EMAIL
			TestTools.WaitUntilElementExists(By.Id("Email"),driver).Click();
			TestTools.WaitUntilElementExists(By.Id("Email"),driver).SendKeys(email);

			//SENHA 
			TestTools.WaitUntilElementExists(By.Id("Senha"),driver).SendKeys(senha);

			//ATIVO
			if (ativo)
			{
				TestTools.WaitUntilElementExists(By.Id("Ativo"),driver).Click();
			}

			//SUMISSÃO POR BOTÃO
			TestTools.WaitUntilElementExists(By.CssSelector(".btn-success"),driver).Click();
		}
	}

}
