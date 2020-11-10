// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmployeeRepository.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Dheer Singh Meena"/>
// ---------------------------------------------------------------------------------------------------------------------
using EmployeePayrollService_ADO.NET_Day26;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeePayrollMsTest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// UC 4 Given the update salary value check if the database got updated.
        /// </summary>
        [TestMethod]
        public void GivenUpdateSalaryValue_CheckIfTheDatabaseGotUpdated()
        {
            //Arrange
            string empName = "Terissa";
            double basicPay = 60000;
            EmployeeRepository repository = new EmployeeRepository();
            EmployeeModel empModel = new EmployeeModel();
            //Act
            repository.UpdateSalaryIntoDatabase(empName, basicPay);
            double expectedPay = repository.UpdatedSalaryFromDatabase(empName);
            //Assert
            Assert.AreEqual(basicPay, expectedPay);
        }
    }
}
