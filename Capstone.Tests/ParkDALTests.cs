using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Transactions;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using Capstone.DAL;
using Capstone.Models;



namespace Capstone.DAL.Tests
{
    [TestClass]
    public class ParkDALTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=Campgrounds;Integrated Security=True";
        private int numberOfParks = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM park;", conn);
                numberOfParks = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO park (name, state, establish_year, area, visitors, description) VALUES ('Test Park', 'testlocation', 2001, 1, 1, 'testdescription'); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void ViewParksMenuTest()
        {
            ParkDAL parkDAL = new ParkDAL(connectionString);

            List<Park> park = parkDAL.ViewParksMenu();

            Assert.AreEqual(numberOfParks + 1, park.Count);
        }

        [TestMethod]
        public void DisplayParkInfoTest()
        {
            Park newPark = new Park
            {
                ParkId = 1
            };
            ParkDAL parkDAL = new ParkDAL(connectionString);

            Park park = parkDAL.DisplayParkInfo(newPark);

            Assert.AreEqual(1, park.ParkId);
        }

        //[TestMethod]
        //public void UpdateDepartmentTest()
        //{
        //    DepartmentSqlDAL departmentSqldal = new DepartmentSqlDAL(connectionString);

        //    List<Department> departments = departmentSqldal.GetDepartments();

        //    //int testDepartmentID = 0;
        //    Department testDepartment = new Department();

        //    foreach (Department thisDepartment in departments)
        //    {
        //        if (thisDepartment.Name == "Test Department")
        //        {
        //            testDepartment = thisDepartment;
        //        }
        //    }

        //    testDepartment.Name = "Altered Name";

        //    bool didUpdate = departmentSqldal.UpdateDepartment(testDepartment);

        //    Assert.AreEqual(true, didUpdate);

        //}
    }
}