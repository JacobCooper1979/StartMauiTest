using CsvHelper;
using StartMauiTest;
using System.Globalization;
using System.Collections.Generic;
using System.IO;


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
        }



        //PAY CACLUATOR TEST METHODS

        /// <summary>
        /// GRODD
        /// </summary>
        /*[TestMethod]
        public void GrossCalculation()
        {
            //Arrange
            PayCalculator grosscalc = new PayCalculator();

            //Act
            var actual = grosscalc.GetGross(); //work hrs and hours work
            var control = 10000;

            //Assert
            Assert.AreEqual(control, actual);
        }*/
    }
}