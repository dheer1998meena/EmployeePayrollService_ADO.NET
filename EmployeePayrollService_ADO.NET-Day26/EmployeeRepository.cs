// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmployeeRepository.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Dheer Singh Meena"/>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollService_ADO.NET_Day26
{
    class EmployeeRepository
    {
        //Specifying the connection string from the sql server connection.
        public static string connectionString = @"Data Source=DESKTOP-4849HJR;Initial Catalog=payroll_service;Integrated Security=True;User ID=dheermeena;Password=Dheer@1998";
        // Establishing the connection using the Sql
        SqlConnection connection = new SqlConnection(connectionString);

        /// <summary>
        ///UC1 Creating a method for checking for the validity of the connection.
        /// </summary>
        public void EnsureDataBaseConnection()
        {
            this.connection.Open();
            using (connection)
            {
                Console.WriteLine("The Connection is created");
            }
            this.connection.Close();
        }
    }
}