using System;

namespace EmployeePayrollService_ADO.NET_Day26
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll Services Using ADO.NET");
            //Creating a instance object of EmployeeRepository class.
            EmployeeRepository repository = new EmployeeRepository();
            /// UC2 Ability for Employee Payroll Service to retrieve the Employee Payroll from the Database
            repository.GetAllEmployeeData();
            /// Adding Employee To Database
            EmployeeModel model = new EmployeeModel();
            model.EmployeeName = "Rhul";
            model.Address = "Jabalpur";
            model.BasicPay = 70000;
            model.Deductions = 500;
            model.Department = "Testor";
            model.Gender = "M";
            model.PhoneNumber = 9567986354;
            model.NetPay = 73000;
            model.Tax = 1000;
            model.StartDate = DateTime.Now;
            model.TaxablePay = 500;

            Console.WriteLine(repository.AddEmployee(model) ? "Record inserted successfully " : "Failed");
            repository.GetAllEmployeeData();
        }
    }
}
