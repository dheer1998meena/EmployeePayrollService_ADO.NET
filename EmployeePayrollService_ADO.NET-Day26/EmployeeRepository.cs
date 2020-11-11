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

    public class EmployeeRepository
    {
        /// Ensuring the established connection using the Sql Connection specifying the property.
        public static SqlConnection connection { get; set; }

        /// <summary>
        ///UC1 Creating a method for checking for the validity of the connection.
        /// </summary>
        public void EnsureDataBaseConnection()
        {
            /// Creates a new connection for every method to avoid "ConnectionString property not initialized" exception
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            using (connection)
            {
                Console.WriteLine("The Connection is created");
            }
            connection.Close();
        }
        /// <summary>
        /// UC2 Ability for Employee Payroll Service to retrieve the Employee Payroll from the Database
        /// </summary>
        public void GetAllEmployeeData(string query)
        {
            /// Creates a new connection for every method to avoid "ConnectionString property not initialized" exception
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            //Creating Employee model class object
            EmployeeModel employee = new EmployeeModel();
            try
            {
                using (connection)
                {
                    //Opening the connection to the statrt mapping.
                    connection.Open();
                    //Implementing the command on the connection fetched database table.
                    SqlCommand command = new SqlCommand(query, connection);
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
                connection.Close();
            }

        }
        /// <summary>
        /// Adding Employee To Database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddEmployee(EmployeeModel model)
        { /// Creates a new connection for every method to avoid "ConnectionString property not initialized" exception
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            try
            {
                using (connection)
                {
                    //Creating a stored Procedure for adding employees into database
                    SqlCommand command = new SqlCommand("dbo.SpAddEmployeeDetails", connection);
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
        /// <summary>
        /// UC3 Updates the given empname with given salary into database.
        /// </summary>
        /// <param name="empName"></param>
        /// <param name="basicPay"></param>
        /// <returns></returns>
        public bool UpdateSalaryIntoDatabase(string empName, double basicPay)
        {
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            try
            {
                using (connection)
                {
                    connection.Open();
                    string query = @"update dbo.employee_payroll set basic_pay=@p1 where EmpName=@p2";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@p1", basicPay);
                    command.Parameters.AddWithValue("@p2", empName);
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
        /// <summary>
        /// UC 4  Reads the updated salary from database.
        /// </summary>
        /// <param name="empName"></param>
        /// <returns></returns>
        public double UpdatedSalaryFromDatabase(string empName)
        {
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            try
            {
                using (connection)
                {
                    string query = @"select basic_pay from dbo.employee_payroll where EmpName=@p1";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@p1", empName);
                    return (double)command.ExecuteScalar();
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
        /// <summary>
        /// UC5 Gets the employees details for a particular date range.
        /// </summary>
        /// <param name="date">The date.</param>
        public void GetEmployeesFromForDateRange(string date)
        {
            string query = $@"select * from dbo.employee_payroll where StartDate between cast('{date}' as date) and cast(getdate() as date)";
            GetAllEmployeeData(query);
        }
        /// <summary>
        /// UC6 Getting the detail of salary ofthe employee joining grouped by gender and searched for a particular gender.
        /// </summary>
        public void FindGroupedByGenderData(string gender)
        {
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            try
            {
                using (connection)
                {
                    string query = @"select Gender,count(basic_pay) as EmpCount,min(basic_pay) as MinSalary,max(basic_pay) 
                                   as MaxSalary,sum(basic_pay) as SalarySum,avg(basic_pay) as AvgSalary from dbo.employee_payroll
                                   where Gender=@parameter group by Gender";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@parameter", gender);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int empCount = reader.GetInt32(1);
                            double minSalary = reader.GetDouble(2);
                            double maxSalary = reader.GetDouble(3);
                            double sumOfSalary = reader.GetDouble(4);
                            double avgSalary = reader.GetDouble(5);
                            Console.WriteLine($"Gender:{gender}\nEmployee Count:{empCount}\nMinimum Salary:{minSalary}\nMaximum Salary:{maxSalary}\n" +
                                $"Total Salary for {gender} :{sumOfSalary}\n" + $"Average Salary:{avgSalary}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data found");
                    }
                    reader.Close();
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
        /// <summary>
        /// UC7 Inserts data into multiple tables using transactions.
        /// </summary>
        public void InsertIntoMultipleTablesWithTransactions()
        {
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();

            Console.WriteLine("Enter EmployeeID");
            int empID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Name:");
            string empName = Console.ReadLine();

            DateTime startDate = DateTime.Now;

            Console.WriteLine("Enter Address:");
            string address = Console.ReadLine();

            Console.WriteLine("Enter Gender:");
            string gender = Console.ReadLine();

            Console.WriteLine("Enter PhoneNumber:");
            double phonenumber = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter BasicPay:");
            int basicPay = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Deductions:");
            int deductions = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter TaxablePay:");
            int taxablePay = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Tax:");
            int tax = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter NetPay:");
            int netPay = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter CompanyId:");
            int companyId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter CompanyName:");
            string companyName = Console.ReadLine();

            Console.WriteLine("Enter DeptId:");
            int deptId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter DeptName:");
            string deptName = Console.ReadLine();

            using (connection)
            {
                connection.Open();

                // Start a local transaction.
                SqlTransaction sqlTran = connection.BeginTransaction();

                // Enlist a command in the current transaction.
                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTran;

                try
                {
                    // Execute 1st command
                    command.CommandText = "insert into company values(@company_id,@company_name)";
                    command.Parameters.AddWithValue("@company_id", companyId);
                    command.Parameters.AddWithValue("@company_name", companyName);
                    command.ExecuteScalar();

                    // Execute 2nd command
                    command.CommandText = "insert into employee values(@emp_id,@EmpName,@gender,@phone_number,@address,@startDate,@company_id)";
                    command.Parameters.AddWithValue("@emp_id", empID);
                    command.Parameters.AddWithValue("@EmpName", empName);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@gender", gender);
                    command.Parameters.AddWithValue("@phone_number", phonenumber);
                    command.Parameters.AddWithValue("@address", address);
                    command.ExecuteScalar();

                    // Execute 3rd command
                    command.CommandText = "insert into payroll values(@emp_id,@Basic_Pay,@Deductions,@Taxable_pay,@Income_tax,@Net_pay)";
                    command.Parameters.AddWithValue("@Basic_Pay", basicPay);
                    command.Parameters.AddWithValue("@Deductions", deductions);
                    command.Parameters.AddWithValue("@Taxable_pay", taxablePay);
                    command.Parameters.AddWithValue("@Income_tax", tax);
                    command.Parameters.AddWithValue("@Net_pay", netPay);
                    command.ExecuteScalar();

                    // Execute 4th command
                    command.CommandText = "insert into department values(@dept_id,@dept_name)";
                    command.Parameters.AddWithValue("@dept_id", deptId);
                    command.Parameters.AddWithValue("@dept_name", deptName);
                    command.ExecuteScalar();

                    // Execute 5th command
                    command.CommandText = "insert into employee_dept values(@emp_id,@dept_id)";
                    command.ExecuteNonQuery();

                    // Commit the transaction after all commands.
                    sqlTran.Commit();
                    Console.WriteLine("All records were added into the database.");
                }
                catch (Exception ex)
                {
                    // Handle the exception if the transaction fails to commit.
                    Console.WriteLine(ex.Message);
                    try
                    {
                        // Attempt to roll back the transaction.
                        sqlTran.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        // Throws an InvalidOperationException if the connection
                        // is closed or the transaction has already been rolled
                        // back on the server.
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
        }
        /// UC 8 : Retrieves the employee details from multiple tables after implementing E-R concept.
        public void RetrieveEmployeeDetailsFromMultipleTables()
        
        {
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            EmployeeModel employee = new EmployeeModel();
            string query = @"select emp.EmpId, emp.EmpName, dept.basic_pay, emp.StartDate, emp.phoneNumber, emp.address, 
                                    dept.department, emp.gender, pay.deductions, pay.taxable_pay, pay.income_tax, pay.net_pay
                                    from employee_payroll emp, employee_department dept, payroll pay
                                    where emp.EmpId = dept.EmpId and dept.basic_pay = pay.basic_pay;";
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            employee.EmployeeId = reader.GetInt32(0);
                            employee.EmployeeName = reader.GetString(1);
                            employee.BasicPay = reader.GetInt32(2);
                            employee.StartDate = reader.GetDateTime(3);
                            employee.PhoneNumber = reader.GetInt64(4);
                            employee.Address = reader.GetString(5);
                            employee.Department = reader.GetString(6);
                            employee.Gender = reader.GetString(7);
                            employee.Deductions = reader.GetDouble(8);
                            employee.TaxablePay = reader.GetDouble(9);
                            employee.Tax = reader.GetDouble(10);
                            employee.NetPay = reader.GetDouble(11);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", employee.EmployeeId, employee.EmployeeName,
                                employee.Gender, employee.Address, employee.BasicPay, employee.StartDate, employee.PhoneNumber, employee.Address,
                                employee.Department, employee.Deductions, employee.TaxablePay, employee.Tax, employee.NetPay);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State.Equals("Open"))
                    connection.Close();
            }
        }
    }
}