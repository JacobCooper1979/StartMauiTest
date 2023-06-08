using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Formats.Asn1;
using System.Globalization;
using Microsoft.VisualBasic.FileIO;

namespace StartMauiTest
{
    public class CsvMap
    {
        public int employeeID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string typeEmployee { get; set; }
        public int hourlyRate { get; set; }
        public string taxthreshold { get; set; }
    }

    public sealed class CsvMapMap : ClassMap<CsvMap>
    {
        public CsvMapMap()
        {
            Map(m => m.employeeID).Index(0);
            Map(m => m.firstName).Index(1);
            Map(m => m.lastName).Index(2);
            Map(m => m.typeEmployee).Index(3);
            Map(m => m.hourlyRate).Index(4);
            Map(m => m.taxthreshold).Index(5);
        }
    }

    public class CsvImporter
    {
        public static List<CsvMap> ImportSomeRecords()
        {
            string fileName = @"C:\Users\jacob\Documents\Tafe Cert 4\c#\Wednesday_Shaun_OOP\Assesments\Project_14June\StartMauiTest\StartMauiTest\employee.csv";
            var myRecords = new List<CsvMap>();

            using (var reader = new StreamReader(fileName))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<CsvMapMap>();

                    int employeeID;
                    string firstName;
                    string lastName;
                    string typeEmployee;
                    int hourlyRate;
                    string taxthreshold;

                    while (csv.Read())
                    {
                        employeeID = csv.GetField<int>(0);
                        firstName = csv.GetField<string>(1);
                        lastName = csv.GetField<string>(2);
                        typeEmployee = csv.GetField<string>(3);
                        hourlyRate = csv.GetField<int>(4);
                        taxthreshold = csv.GetField<string>(5);
                        myRecords.Add(CreateRecord(employeeID, firstName, lastName, typeEmployee, hourlyRate, taxthreshold));
                    }
                }
            }
            return myRecords;
        }

        public static CsvMap CreateRecord(int employeeID, string firstName, string lastName, string typeEmployee, int hourlyRate, string taxthreshold)
        {
            CsvMap record = new CsvMap();

            record.employeeID = employeeID;
            record.firstName = firstName;
            record.lastName = lastName;
            record.typeEmployee = typeEmployee;
            record.hourlyRate = hourlyRate;
            record.taxthreshold = taxthreshold;

            return record;
        }

        public void SavePayslipToCSV(Payslip payslip)
        {
            try
            {
                string filename = @"C:\Users\jacob\Documents\Tafe Cert 4\c#\Wednesday_Shaun_OOP\Assesments\Project_14June\TaxMaui\ProjectTemplate\Payslips.csv";
                using (StreamWriter writer = new StreamWriter(filename, true))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteField(payslip.Id);
                    csv.WriteField(payslip.EmployeeId);
                    csv.WriteField(payslip.GrossPayment);
                    csv.WriteField(payslip.NetPayment);
                    csv.WriteField(payslip.TaxAmount);
                    csv.WriteField(payslip.SuperAmount);
                    csv.WriteField(payslip.Approved);
                    csv.NextRecord();
                }

                Console.WriteLine("Success: Payslip saved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Payslip failed to save. " + ex.Message);
            }
        }
    }

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
