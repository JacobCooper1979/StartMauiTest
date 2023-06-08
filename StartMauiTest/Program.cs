using StartMauiTest;
using static StartMauiTest.PayCalculator;
using static StartMauiTest.CsvImporter;

var list = new List<CsvMap>();
list = ImportSomeRecords();

//PRINT EMPLOYEE CSV
foreach (var item in list)
{
    Console.WriteLine(item.employeeID);
    Console.WriteLine(item.firstName);
    Console.WriteLine(item.lastName);
    Console.WriteLine(item.taxthreshold);
    Console.WriteLine(item.typeEmployee);
}

//KEEP CONSOLE OPEN
Console.ReadKey();  