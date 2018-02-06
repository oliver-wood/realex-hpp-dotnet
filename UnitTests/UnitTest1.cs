using global.cloudis.RealexHPP.sdk;
using global.cloudis.RealexHPP.sdk.domain;
using global.cloudis.RealexHPP.sdk.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Resources;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestHPPRequest()
        {
            HPPRequest req = new HPPRequest();

            ValidationContext context = new ValidationContext(req, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid;

            string secret = "MY SORDID SECRET";

            // Merchant ID
            context.MemberName = "MerchantID";
            
            req.MerchantID = "";
            isValid = Validator.TryValidateProperty(req.MerchantID, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);

            req.MerchantID = "!@#$$%";
            isValid = Validator.TryValidateProperty(req.MerchantID, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            
            req.MerchantID = "ABC123";
            isValid = Validator.TryValidateProperty(req.MerchantID, context, results);
            Assert.IsTrue(isValid);

            // Account
            context.MemberName = "Account";
            
            req.Account = "";
            isValid = Validator.TryValidateProperty(req.Account, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);

            req.Account = "!@#$$%";
            isValid = Validator.TryValidateProperty(req.Account, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);

            req.Account = "ABC 123";
            isValid = Validator.TryValidateProperty(req.Account, context, results);
            Assert.IsTrue(isValid);


            // OrderID
            context.MemberName = "OrderID";

            req.OrderID = "";
            isValid = Validator.TryValidateProperty(req.OrderID, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);

            req.OrderID = "!@#$$%";
            isValid = Validator.TryValidateProperty(req.OrderID, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);

            req.OrderID = "a1_ABC-123";
            isValid = Validator.TryValidateProperty(req.OrderID, context, results);
            Assert.IsTrue(isValid);


            // Amount 
            context.MemberName = "Amount";

            req.Amount = "NOT A NUMBER";
            isValid = Validator.TryValidateProperty(req.Amount, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);


            req.Amount = "1234567890123453423423";
            isValid = Validator.TryValidateProperty(req.Amount, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);

            req.Amount = "3000";
            isValid = Validator.TryValidateProperty(req.Amount, context, results);
            Assert.IsTrue(isValid);



            // Currency
            context.MemberName = "Currency";

            req.Currency = "EU";
            isValid = Validator.TryValidateProperty(req.Currency, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);

            req.Currency = "GB3";
            isValid = Validator.TryValidateProperty(req.Currency, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);

            req.Currency = "GBP";
            isValid = Validator.TryValidateProperty(req.Currency, context, results);
            Assert.IsTrue(isValid);


            // Timestamp
            context.MemberName = "Timestamp";

            req.Timestamp = "123456";
            isValid = Validator.TryValidateProperty(req.Timestamp, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);

            req.Timestamp = "NOT A NUM";
            isValid = Validator.TryValidateProperty(req.Timestamp, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);

            req.Timestamp = "20170724170232";
            isValid = Validator.TryValidateProperty(req.Timestamp, context, results);
            Assert.IsTrue(isValid);



            // SHA1HASH
            if (!req.IsReady)
            {
                req.Prepare(secret);
                Assert.IsTrue(req.IsReady);
            }
            Assert.IsFalse(string.IsNullOrEmpty(req.Hash));
            Console.WriteLine(@"Hash = " + req.Hash);
            context.MemberName = "Hash";

            isValid = Validator.TryValidateProperty(req.Hash, context, results);
            Assert.IsTrue(isValid);


            string json = JsonConvert.SerializeObject(req, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            Console.WriteLine("====================");
            Console.WriteLine(json);
            Console.WriteLine("====================");


            // Auto settle 
            context.MemberName = "AutoSettleFlag";
            req.AutoSettleFlag = @"NOT VALID";
            isValid = Validator.TryValidateProperty(req.AutoSettleFlag, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);

            req.AutoSettleFlag = @"ON";
            isValid = Validator.TryValidateProperty(req.AutoSettleFlag, context, results);
            Assert.IsTrue(isValid);

            // Comment 1
            context.MemberName = "Comment1";
            req.Comment1 = @"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis,.";
            isValid = Validator.TryValidateProperty(req.Comment1, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.Comment1 = @"This is a comment";
            isValid = Validator.TryValidateProperty(req.Comment1, context, results);
            Assert.IsTrue(isValid);

            // Comment 2
            context.MemberName = "Comment2";
            req.Comment2 = @"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis,.";
            isValid = Validator.TryValidateProperty(req.Comment2, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.Comment2 = @"This is a comment";
            isValid = Validator.TryValidateProperty(req.Comment2, context, results);
            Assert.IsTrue(isValid);

            // Return TSS
            context.MemberName = "ReturnTSS";
            req.ReturnTSS = @"invalid";
            isValid = Validator.TryValidateProperty(req.ReturnTSS, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.ReturnTSS = @"0";
            isValid = Validator.TryValidateProperty(req.ReturnTSS, context, results);
            Assert.IsTrue(isValid);

            // Shipping Code
            context.MemberName = "ShippingCode";
            req.ShippingCode = @"No Good $%$%#$%";
            isValid = Validator.TryValidateProperty(req.ShippingCode, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.ShippingCode = @"A098-TY.| ";
            isValid = Validator.TryValidateProperty(req.ShippingCode, context, results);
            Assert.IsTrue(isValid);

            // Shipping Country
            context.MemberName = "ShippingCountry";
            req.ShippingCountry = @"No Good $%$%#$%";
            isValid = Validator.TryValidateProperty(req.ShippingCountry, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.ShippingCountry = @"United Kingdom";
            isValid = Validator.TryValidateProperty(req.ShippingCountry, context, results);
            Assert.IsTrue(isValid);

            // Billing Code
            context.MemberName = "BillingCode";
            req.BillingCode = @"No Good $%$%#$%";
            isValid = Validator.TryValidateProperty(req.BillingCode, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.BillingCode = @"A098-TY.| ";
            isValid = Validator.TryValidateProperty(req.BillingCode, context, results);
            Assert.IsTrue(isValid);

            // Billing Country
            context.MemberName = "BillingCountry";
            req.BillingCountry = @"No Good $%$%#$%";
            isValid = Validator.TryValidateProperty(req.BillingCountry, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.BillingCountry = @"United Kingdom";
            isValid = Validator.TryValidateProperty(req.BillingCountry, context, results);
            Assert.IsTrue(isValid);

            // Customer Number
            context.MemberName = "CustomerNumber";
            req.CustomerNumber = @"INVALID #$";
            isValid = Validator.TryValidateProperty(req.CustomerNumber, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.CustomerNumber = @"DT1234_sdf-@R1";
            isValid = Validator.TryValidateProperty(req.CustomerNumber, context, results);
            Assert.IsTrue(isValid);

            // Variable Reference
            context.MemberName = "VariableReference";
            req.VariableReference = @"INVALID #$";
            isValid = Validator.TryValidateProperty(req.VariableReference, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.VariableReference = @"DT1234_sdf-@R1";
            isValid = Validator.TryValidateProperty(req.VariableReference, context, results);
            Assert.IsTrue(isValid);

            // Product ID 
            context.MemberName = "ProductID";            
            req.ProductID = @"\\""**&&";
            isValid = Validator.TryValidateProperty(req.ProductID, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.ProductID = "ABC-@Ef123";
            isValid = Validator.TryValidateProperty(req.ProductID, context, results);
            Assert.IsTrue(isValid);

            // Language
            context.MemberName = "Language";
            req.Language = @"INVALID #$";
            isValid = Validator.TryValidateProperty(req.Language, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.Language = @"EN_GB";
            isValid = Validator.TryValidateProperty(req.Language, context, results);
            Assert.IsTrue(isValid);

            // CardPaymentButtonText
            context.MemberName = "CardPaymentButtonText";
            req.CardPaymentButtonText = @"Donate Now!";
            isValid = Validator.TryValidateProperty(req.CardPaymentButtonText, context, results);
            Assert.IsTrue(isValid);


            // CardStorageEnable
            context.MemberName = "CardStorageEnable";
            req.CardStorageEnable = @"INVALID";
            isValid = Validator.TryValidateProperty(req.CardStorageEnable, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.CardStorageEnable = @"1";
            isValid = Validator.TryValidateProperty(req.CardStorageEnable, context, results);
            Assert.IsTrue(isValid);


            // OfferSaveCard
            context.MemberName = "OfferSaveCard";
            req.OfferSaveCard = @"INVALID";
            isValid = Validator.TryValidateProperty(req.OfferSaveCard, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.OfferSaveCard = @"1";
            isValid = Validator.TryValidateProperty(req.OfferSaveCard, context, results);
            Assert.IsTrue(isValid);


            // PayerReference
            context.MemberName = "PayerReference";
            req.PayerReference = @"INVALID.Ref";
            isValid = Validator.TryValidateProperty(req.PayerReference, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.PayerReference = @"DT1234-99 a";
            isValid = Validator.TryValidateProperty(req.PayerReference, context, results);
            Assert.IsTrue(isValid);


            // PaymentReference
            context.MemberName = "PaymentReference";
            req.PaymentReference = @"INVALID.Ref";
            isValid = Validator.TryValidateProperty(req.PaymentReference, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.PaymentReference = @"DTABC_78";
            isValid = Validator.TryValidateProperty(req.PaymentReference, context, results);
            Assert.IsTrue(isValid);


            // PayerExists
            context.MemberName = "PayerExists";
            req.PayerExists = @"INVALID";
            isValid = Validator.TryValidateProperty(req.PayerExists, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.PayerExists = @"1";
            isValid = Validator.TryValidateProperty(req.PayerExists, context, results);
            Assert.IsTrue(isValid);


            // ValidateCardOnly
            context.MemberName = "ValidateCardOnly";
            req.ValidateCardOnly = @"INVALID";
            isValid = Validator.TryValidateProperty(req.ValidateCardOnly, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.ValidateCardOnly = @"1";
            isValid = Validator.TryValidateProperty(req.ValidateCardOnly, context, results);
            Assert.IsTrue(isValid);


            // DCCEnable
            context.MemberName = "DCCEnable";
            req.DCCEnable = @"INVALID";
            isValid = Validator.TryValidateProperty(req.DCCEnable, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.DCCEnable = @"1";
            isValid = Validator.TryValidateProperty(req.DCCEnable, context, results);
            Assert.IsTrue(isValid);


            // HPPFraudFilterMode
            context.MemberName = "HPPFraudFilterMode";
            req.HPPFraudFilterMode = @"INVALID";
            isValid = Validator.TryValidateProperty(req.HPPFraudFilterMode, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.HPPFraudFilterMode = @"OFF";
            isValid = Validator.TryValidateProperty(req.HPPFraudFilterMode, context, results);
            Assert.IsTrue(isValid);


            // HPPVersion
            context.MemberName = "HPPVersion";
            req.HPPVersion = @"INVALID";
            isValid = Validator.TryValidateProperty(req.HPPVersion, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.HPPVersion = @"1";
            isValid = Validator.TryValidateProperty(req.HPPVersion, context, results);
            Assert.IsTrue(isValid);


            // HPPSelectStoredCard
            context.MemberName = "HPPSelectStoredCard";
            req.HPPSelectStoredCard = @"INVALID @@";
            isValid = Validator.TryValidateProperty(req.HPPSelectStoredCard, context, results);
            Assert.IsFalse(isValid);
            if (!isValid) WriteValidationResults(results);
            req.HPPSelectStoredCard = @"1";
            isValid = Validator.TryValidateProperty(req.HPPSelectStoredCard, context, results);
            Assert.IsTrue(isValid);


            // Overall 
            context = new ValidationContext(req, null, null);
            isValid = Validator.TryValidateObject(req, context, results, false);
            if (!isValid) WriteValidationResults(results);

            req.Encode();

            json = JsonConvert.SerializeObject(req, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            Console.WriteLine("====================");
            Console.WriteLine(json);
            Console.WriteLine("====================");

            req.Decode();

            json = JsonConvert.SerializeObject(req, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            Console.WriteLine("====================");
            Console.WriteLine(json);
            Console.WriteLine("====================");
        }

        [TestMethod]
        public void TestValidateRequestHash()
        {
            string timeStamp = "20170727073914";
            string merchantId = "dogstrusttest";
            string orderId = "2000019";
            string amount = "1000";
            string currency = "GBP";
            string secret = "secret";

            HPPRequest req = new HPPRequest()
            {
                Timestamp = timeStamp,
                MerchantID = merchantId,
                OrderID = orderId,
                Amount = amount,
                Currency = currency
            };
            req.Prepare(secret);

            string expectedHash = "a86180c3bdf1950cdff0c9b6a75af437522cca95";

            Assert.AreEqual(expectedHash, req.Hash);
        }

        [TestMethod]
        public void TestRequestAsNVC()
        {
            string timeStamp = "20170727073914";
            string merchantId = "dogstrusttest";
            string orderId = "2000019";
            string amount = "1000";
            string currency = "GBP";
            string secret = "secret";

            HPPRequest req = new HPPRequest()
            {
                Timestamp = timeStamp,
                MerchantID = merchantId,
                OrderID = orderId,
                Amount = amount,
                Currency = currency
            };
            req.Prepare(secret);

            Console.WriteLine("====================");
            Console.WriteLine(FormUtils.HPPRequestToFormFields(req));
            Console.WriteLine("====================");


        }



        [TestMethod]
        public void TestDeserialiseHPPResponse()
        {
            string filename;

            filename = "response-enc4.json";
            string json = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Data\", filename));
            HPPResponse resp = JsonConvert.DeserializeObject<HPPResponse>(json);
            resp.IsEncoded = true;

            resp.Decode();
            
            Console.WriteLine("====================");
            Console.WriteLine();
            Console.WriteLine("====================");


            RealexHPP hpp = new RealexHPP() { Secret = "secret" };

            filename = "response-fail-enc.json";
            string json2 = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Data\", filename));

            HPPResponse resp2 = hpp.ResponseFromJson(json2);

            Assert.AreEqual(resp2.Result, "101");

            ResourceManager rm = global.cloudis.RealexHPP.HPPMessages.ResourceManager;
            Console.WriteLine(rm.GetString("hppResponse_message_unknown"));

            Assert.IsNull(MessageUtils.GetMessage("No message"));

            Assert.IsNotNull(MessageUtils.GetMessageForResult("101"));

            Assert.IsNotNull(MessageUtils.GetMessageForResult("508"));

            Console.WriteLine(MessageUtils.GetMessageForResult("508"));
        }


        private void WriteValidationResults(List<ValidationResult> results)
        {
            foreach (var validationResult in results)
            {
                Console.WriteLine(validationResult.ErrorMessage);
            }
            results.Clear();
        }
    }

}
