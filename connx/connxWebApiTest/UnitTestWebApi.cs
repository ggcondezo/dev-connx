using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using connxWebApi;

namespace connxWebApiTest
{
    [TestClass]
    public class UnitTestWebApi
    {
        private CreditCardController conWebApi;
        public UnitTestWebApi() {
            conWebApi = new CreditCardController();
        }

        [TestMethod]
        public void TestValidVisa()
        {
            var creditCard = conWebApi.CreditCard(CreditCardType.VISA, "4408041234567893");
            Assert.IsTrue(creditCard.isValidCreditCard);
        }
        [TestMethod]
        public void TestInvalidVisa()
        {
            var creditCard = conWebApi.CreditCard(CreditCardType.VISA, "4417123456789112");
            Assert.IsFalse(creditCard.isValidCreditCard);
        }
        [TestMethod]
        public void TestValidAMEX()
        {
            var creditCard = conWebApi.CreditCard(CreditCardType.AMEX, "378282246310005");
            Assert.IsTrue(creditCard.isValidCreditCard);
        }
        [TestMethod]
        public void TestInvalidAMEX()
        {
            var creditCard = conWebApi.CreditCard(CreditCardType.AMEX, "378282246310006");
            Assert.IsFalse(creditCard.isValidCreditCard);
        }
        [TestMethod]
        public void TestValidDISCOVER()
        {
            var creditCard = conWebApi.CreditCard(CreditCardType.DISCOVER, "6011111111111117");
            Assert.IsTrue(creditCard.isValidCreditCard);
        }
        [TestMethod]
        public void TestInvalidDISCOVER()
        {
            var creditCard = conWebApi.CreditCard(CreditCardType.DISCOVER, "6011111111111116");
            Assert.IsFalse(creditCard.isValidCreditCard);
        }
 
        [TestMethod]
        public void TestValidMASTERCARD()
        {
            var creditCard = conWebApi.CreditCard(CreditCardType.MASTERCARD, "5105105105105100");
            Assert.IsTrue(creditCard.isValidCreditCard);
        }
        [TestMethod]
        public void TestInvalidMASTERCARD()
        {
            var creditCard = conWebApi.CreditCard(CreditCardType.MASTERCARD, "5105105105105106");
            Assert.IsFalse(creditCard.isValidCreditCard);
        }
        [TestMethod]
        public void TestInvalidMasterNumber() {
            var creditCard = conWebApi.CreditCard(CreditCardType.UNKONWN, "5105105105105106");
            Assert.IsTrue(creditCard.CardTypeId == CreditCardType.MASTERCARD);
            Assert.IsFalse(creditCard.isValidCreditCard);

        }
        [TestMethod]
        public void TestValidMasterCardNumber() {
            var creditCard = conWebApi.CreditCard(CreditCardType.UNKONWN, "5105105105105100");
            Assert.IsTrue(creditCard.isValidCreditCard);
            Assert.IsTrue(creditCard.CardTypeId == CreditCardType.MASTERCARD);
        }
    }
}
