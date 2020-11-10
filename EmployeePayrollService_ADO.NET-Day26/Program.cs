// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Dheer Singh Meena"/>
// --------------------------------------------------------------------------------------------------------------------
using System;

namespace EmployeePayrollService_ADO.NET_Day26
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll Services Using ADO.NET");
            ///Creating a instance object of EmployeeRepository class.
            EmployeeRepository repository = new EmployeeRepository();
            ///UC1 Creating a method for checking for the validity of the connection.
            repository.EnsureDataBaseConnection();
            ///UC3 Updates the given empname with given salary into database.
            Console.WriteLine(repository.UpdateSalaryIntoDatabase("Terissa", 50000) ? "Update done successfully " : "Update Failed");
            /// UC5 Gets the employees details for a particular date range.
            repository.GetEmployeesFromForDateRange("2018 - 05 - 03");
            /// UC6 Getting the detail of salary ofthe employee joining grouped by gender and searched for a particular gender.
            repository.FindGroupedByGenderData("M");
            Console.ReadKey();
        }
    }
}
