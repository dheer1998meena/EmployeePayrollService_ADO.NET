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

        //Specifying the connection string from the sql server connection
        public static string connectionString = @"Data Source=DESKTOP-4849HJR;Initial Catalog=payroll_service;Integrated Security=True;User ID=dheermeena;Password=Dheer@1998";
        // Establishing the connection using the Sql
        SqlConnection connection = new SqlConnection(connectionString);
        //ReInitiallizing the connection using the sql for GetAllEmployeeData method.
        SqlConnection connection1 = new SqlConnection(connectionString);
        //ReInitiallizing the connection using the sql for update employee method.
        SqlConnection connection2 = new SqlConnection(connectionString);


        /// <summary>
        ///UC1 Creating a method for checking for the validity of the connection.
        /// </summary>
        public void EnsureDataBaseConnection()
        {
            this.connection2.Open();
            using (connection)
            {
                Console.WriteLine("The Connection is created");
            }
            this.connection2.Close();
        }
        /// <summary>
        /// UC2 Ability for Employee Payroll Service to retrieve the Employee Payroll from the Database
        /// </summary>
        public void GetAllEmployeeData()
        {
            //Creating Employee model class object
            EmployeeModel employee = new EmployeeModel();
            try
            {
                using (connection1)
                {
                    //Query to get all the data from table.
                    string query = @"select * from dbo.employee_payroll";
                    //Opening the connection to the statrt mapping.
                    this.connection1.Open();
                    //Implementing the command on the connection fetched database table.
                    SqlCommand command = new SqlCommand(query, connection1);
                    //Executing the Sql datareaeder to fetch the all records.
                    SqlDataReader dataReader = command.ExecuteReader();
                    //Checking datareader has rows or not.
                    if (dataReader.HasRows)
                    {
                        //using while loop for read multiple rows.
                        // Mapping the data to the employee model class object.
                        while (dataReader.Read())
                        {
                            employee.EmployeeId = dataReader.GetInt32(0);
                            employee.EmployeeName = dataReader.GetString(1);
                            employee.BasicPay = dataReader.GetDouble(2);
                            employee.StartDate = dataReader.GetDateTime(3);
                            employee.Gender = dataReader.GetString(4);
                            employee.PhoneNumber = dataReader.GetInt64(5);
                            employee.Department = dataReader.GetString(6);
                            employee.Address = dataReader.GetString(7);
                            employee.Deductions = dataReader.GetDouble(8);
                            employee.TaxablePay = dataReader.GetDouble(9);
                            employee.Tax = dataReader.GetDouble(10);
                            employee.NetPay = dataReader.GetDouble(11);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", employee.EmployeeId, employee.EmployeeName,
                                employee.Gender, employee.Address, employee.BasicPay, employee.StartDate, employee.PhoneNumber, employee.Address,
                                employee.Department, employee.Deductions, employee.TaxablePay, employee.Tax, employee.NetPay);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("no data found ");
                    }
                    dataReader.Close();
                }
            }
            /// Catching the null record exception
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //Always ensuring the closing of the connection
            finally
            {
                this.connection1.Close();
            }

        }
        /// <summary>
        /// Adding Employee To Database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddEmployee(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    //Creating a stored Procedure for adding employees into database
                    SqlCommand command = new SqlCommand("dbo.SpAddEmployeeDetails", this.connection);
                    //Command type is set as stored procedure
                    command.CommandType = CommandType.StoredProcedure;
                    //Adding values from employeemodel to stored procedure using disconnected architecture
                    //connected architecture will only read the data
                    command.Parameters.AddWithValue("@EmpName", model.EmployeeName);
                    command.Parameters.AddWithValue("@basic_Pay", model.BasicPay);
                    command.Parameters.AddWithValue("@StartDate", model.StartDate);
                    command.Parameters.AddWithValue("@gender", model.Gender);
                    command.Parameters.AddWithValue("@phoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@department", model.Department);
                    command.Parameters.AddWithValue("@address", model.Address);
                    command.Parameters.AddWithValue("@deductions", model.Deductions);
                    command.Parameters.AddWithValue("@taxable_pay", model.TaxablePay);
                    command.Parameters.AddWithValue("@income_tax", model.Tax);
                    command.Parameters.AddWithValue("@net_pay", model.NetPay);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();

                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
    }
}