using CsvHelper;
using StartMauiTest;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using static System.Net.WebRequestMethods;


namespace ProjectUnitTests
{
    [TestClass]
    public class TestProjectClass
    {
        /// <summary>
        /// Check if imported file
        /// </summary>
        [TestMethod]
        public void CheckImports()
        {
            //Arrange
            List<CsvMap> list = new List<CsvMap>();

            //Act
            list = CsvImporter.ImportSomeRecords();

            //Assert
            Assert.IsNotNull(list);
        }






        [TestMethod]
        public void SavePayslipToCSV_ValidPayslip_SavesToCsv()
        {
            var payslip = new SavePayslipCsv.Payslip("123", "456", 1000, 800, 200, 100);

            var savePayslipCsv = new SavePayslipCsv();
            savePayslipCsv.SavePayslipToCSV(payslip);

            var filePathOfPayslip = @"C:\Users\jacob\Documents\Tafe Cert 4\c#\Wednesday_Shaun_OOP\Assesments\Project_14June\StartMauiTest\StartMauiTest\Payslips.csv";

            var fountId = 0;
            decimal foundGross = 0;
            using (var reader = new StreamReader(filePathOfPayslip))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                while (csv.Read())
                {
                    int tempID = csv.GetField<int>(0);
                    var tempGross = csv.GetField<decimal>(2);
                    if (payslip.Id == tempID.ToString())
                    {
                        fountId = tempID;
                        foundGross = tempGross;
                    }
                }
            }

            Assert.AreEqual(123, fountId);
            Assert.AreEqual(1000, foundGross);
        }


        [TestClass]
        public class CsvImporterTests
        {
            private const string TestPayslipFilePath = "TestPayslips.csv";

            [TestInitialize]
            public void Initialize()
            {
                
            }

            [TestMethod]
            public void SavePayslipToCSV_PayslipSavedSuccessfully()
            {
                // Arrange
                var payslip = new Payslip("123", "456", 1000, 800, 200, 100);

                var csvImporter = new CsvImporter();

                // Act
                csvImporter.SavePayslipToCSV(payslip);

                // Assert
                Assert.IsNotNull(System.IO.File.Exists(TestPayslipFilePath), "Payslip file should exist.");
            }




            [TestClass]
            public class PayCalculatorTests
            {
                [TestMethod]
                public void CalculateSuperannuation_ValidGrossPayment_ReturnsCorrectSuperannuationAmount()
                {
                    // Arrange
                    decimal grossPayment = 1000m;
                    decimal expectedSuperannuation = 150m; // 15% of 1000

                    // Act
                    decimal actualSuperannuation = PayCalculator.CalculateSuperannuation(grossPayment);

                    // Assert
                    Assert.AreEqual(expectedSuperannuation, actualSuperannuation, "Superannuation amount is incorrect");
                }

                [TestMethod]
                public void CalculateGross_ValidHourlyRateAndHoursWorked_ReturnsCorrectGrossPayment()
                {
                    // Arrange
                    decimal hourlyRate = 20m;
                    decimal hoursWorked = 40m;
                    decimal expectedGrossPayment = 800m; // 20 * 40

                    // Act
                    decimal actualGrossPayment = PayCalculator.CalculateGross(hourlyRate, hoursWorked);

                    // Assert
                    Assert.AreEqual(expectedGrossPayment, actualGrossPayment, "Gross payment is incorrect");
                }

                [TestMethod]
                public void CalculateNet_ValidGrossPaymentAndTaxAmount_ReturnsCorrectNetPayment()
                {
                    // Arrange
                    decimal grossPayment = 1000m;
                    decimal taxAmount = 200m;
                    decimal expectedNetPayment = 800m; // 1000 - 200

                    // Act
                    decimal actualNetPayment = PayCalculator.CalculateNet(grossPayment, taxAmount);

                    // Assert
                    Assert.AreEqual(expectedNetPayment, actualNetPayment, "Net payment is incorrect.");
                }
            }
        }
    }
}