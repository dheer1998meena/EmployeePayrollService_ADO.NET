// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DBConnection.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Dheer Singh Meena"/>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace EmployeePayrollService_ADO.NET_Day26
{
    class DBConnection
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConnection()
        {

            //Specifying the connection string from the sql server connection
            string connectionString = @"Data Source=DESKTOP-4849HJR;Initial Catalog=payroll_service;Integrated Security=True;User ID=dheermeena;Password=Dheer@1998";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
