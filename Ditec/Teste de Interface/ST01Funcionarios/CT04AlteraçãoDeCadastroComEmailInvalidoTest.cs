using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ditec.Teste_de_Interface.ST01Funcionarios
{
   [TestFixture]
   public class CT04AlteraçãoDeCadastroComEmailInvalidoTest
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
         timespan = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
      }

      //[TearDown]
      //protected void TearDown()
      //{
      //   driver.Quit();
      //}

      [Test]
      public void CT04AlteraçãoDeCadastroComEmailInvalidoClick()
      {
         string email = "raphael.mota14@hotmail.com";

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
            var row = timespan.Until(ExpectedConditions.ElementExists(By.XPath(string.Format("//tr/td[contains(text(), '{0}')]//parent::tr",email))));
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
      public void CT04AlteraçãoDeCadastroComEmailInvalidoSubmit()
      {
         string email = "raphael.mota14@hotmail.com";

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
         Assert.That(errorMessage, Is.EqualTo("Inclua um \"@\" no endereço de e-mail. \"raphael.mota14hotmail.com\" está com um \"@\" faltando."));
      }


   }
}
