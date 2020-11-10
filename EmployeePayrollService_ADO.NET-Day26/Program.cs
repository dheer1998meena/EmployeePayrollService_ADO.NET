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
            // UC1 Ensuring the database connection using the sql connection string
            repository.EnsureDataBaseConnection();
        }
    }
}
