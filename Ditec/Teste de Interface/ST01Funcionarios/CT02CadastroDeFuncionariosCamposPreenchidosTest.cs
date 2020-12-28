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

namespace Ditec
{
   [TestFixture]
   public class CT02CadastroDeFuncionariosCamposPreenchidosTest
   {
      private IWebDriver driver;
      public IDictionary<string, object> vars { get; private set; }
      private IJavaScriptExecutor js;
      private WebDriverWait timespan; 

      [SetUp]
      public void SetUp()
      {
         driver = new ChromeDriver();
         js = (IJavaScriptExecutor)driver;
         vars = new Dictionary<string, object>();
         timespan = new WebDriverWait(driver, TimeSpan.FromSeconds(1)); 
      }

      //[TearDown]
      //protected void TearDown()
      //{
      //   driver.Quit();
      //}

      [Test]
      public void cT02CadastroDeFuncionariosCamposPreenchidos()
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
         Assert.That(driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("×\\\\nRegistro salvo com sucesso!"));
      }
   }
}
