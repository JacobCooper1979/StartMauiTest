using CsvHelper;
using StartMauiTest;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using StartMauiTest;


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



        /*[TestMethod]
        public void SavePayslipToCSV_ValidPayslip_SavesToCsv()
        {
            var payslip = new SavePayslipCsv.Payslip("123", "456", 1000, 800, 200, 100);

            // Create a unique filename for each test to avoid conflicts
            var testFilename = Path.GetTempFileName();

            try
            {
                var savePayslipCsv = new SavePayslipCsv();
                savePayslipCsv.SavePayslipToCSV(payslip);
                Assert.IsTrue(File.Exists(testFilename), "CSV file should be created");

                // Read the CSV file and verify its contents
                using (var reader = new StreamReader(testFilename))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<SavePayslipCsv.Payslip>().ToList();
                    Assert.AreEqual(1, records.Count, "CSV file should contain one record");

                    var savedPayslip = records[9];
                    Assert.AreEqual(payslip.Id, savedPayslip.Id, "Payslip ID should match");
                    Assert.AreEqual(payslip.EmployeeId, savedPayslip.EmployeeId, "Employee ID should match");
                    Assert.AreEqual(payslip.GrossPayment, savedPayslip.GrossPayment, "Gross payment should match");
                    Assert.AreEqual(payslip.NetPayment, savedPayslip.NetPayment, "Net payment should match");
                    Assert.AreEqual(payslip.TaxAmount, savedPayslip.TaxAmount, "Tax amount should match");
                    Assert.AreEqual(payslip.SuperAmount, savedPayslip.SuperAmount, "Super amount should match");
                    Assert.AreEqual(payslip.Approved, savedPayslip.Approved, "Approved status should match");
                }
            }
            finally
            {
                // Clean up the temporary file
                File.Delete(testFilename);
            }
        }*/


        [TestMethod]
        public void SavePayslipToCSV_ValidPayslip_SavesToCsv()
        {
            var payslip = new SavePayslipCsv.Payslip("123", "456", 1000, 800, 200, 100);



            // Create a unique filename for each test to avoid conflicts
            //var testFilename = Path.GetTempFileName();



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
                // Delete the test payslip file if it exists
                if (File.Exists(TestPayslipFilePath))
                {
                    File.Delete(TestPayslipFilePath);
                }
            }

            [TestMethod]
            public void SavePayslipToCSV_PayslipSavedSuccessfully()
            {
                // Arrange
                var payslip = new Payslip("1", "EMP001", 1000, 900, 100, 50);
                var csvImporter = new CsvImporter();

                // Act
                csvImporter.SavePayslipToCSV(payslip);

                // Assert
                Assert.IsTrue(File.Exists(TestPayslipFilePath), "Payslip file should exist.");



                // Clean up the test payslip file
                File.Delete(TestPayslipFilePath);
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
                    Assert.AreEqual(expectedSuperannuation, actualSuperannuation, "Superannuation amount is incorrect.");
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
                    Assert.AreEqual(expectedGrossPayment, actualGrossPayment, "Gross payment is incorrect.");
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