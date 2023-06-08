using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartMauiTest
{
    public class SavePayslipCsv
    {
        // Method to save the payslip to CSV
        public void SavePayslipToCSV(Payslip payslip)
        {
            try
            {
                string filename = @"C:\Users\jacob\Documents\Tafe Cert 4\c#\Wednesday_Shaun_OOP\Assesments\Project_14June\StartMauiTest\StartMauiTest\Payslips.csv";
                using (StreamWriter writer = new StreamWriter(filename, true))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    // Write the payslip values to the CSV
                    csv.WriteField(payslip.Id);
                    csv.WriteField(payslip.EmployeeId);
                    csv.WriteField(payslip.GrossPayment);
                    csv.WriteField(payslip.NetPayment);
                    csv.WriteField(payslip.TaxAmount);
                    csv.WriteField(payslip.SuperAmount);
                    csv.WriteField(payslip.Approved);
                    csv.NextRecord();
                }

                Console.WriteLine("Success", "Payslip saved.", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error", "Payslip failed to save " + ex.Message, "OK");
            }
        }

        // Define the class for a payslip
        public class Payslip
        {
            private readonly string id;
            private readonly string employeeId;
            private readonly decimal grossPayment;
            private decimal netPayment;
            private decimal taxAmount;
            private decimal superAmount;
            private bool approved;

            public Payslip(string id, string employeeId, decimal grossPayment, decimal netPayment, decimal taxAmount, decimal superAmount)
            {
                this.id = id;
                this.employeeId = employeeId;
                this.grossPayment = grossPayment;
                this.netPayment = netPayment;
                this.taxAmount = taxAmount;
                this.superAmount = superAmount;
                this.approved = false;
            }

            public string Id
            {
                get { return id; }
            }

            public string EmployeeId
            {
                get { return employeeId; }
            }

            public decimal GrossPayment
            {
                get { return grossPayment; }
            }

            public decimal NetPayment
            {
                get { return netPayment; }
                set { netPayment = value; }
            }

            public decimal TaxAmount
            {
                get { return taxAmount; }
                set { taxAmount = value; }
            }

            public decimal SuperAmount
            {
                get { return superAmount; }
                set { superAmount = value; }
            }

            public bool Approved
            {
                get { return approved; }
                set { approved = value; }
            }
        }
    }
}
