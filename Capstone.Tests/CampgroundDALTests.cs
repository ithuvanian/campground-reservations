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
    public class CampgroundDALTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=Campgrounds;Integrated Security=True";
        private int numberOfCampgrounds = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM campground;", conn);
                numberOfCampgrounds = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO campground (park_id, name, open_from_MM, open_to_MM, daily_fee) VALUES (1,'TestCampground', 1, 1, 1); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void ViewCampgroundsTest()
        {
            CampgroundDAL campgroundDAL = new CampgroundDAL(connectionString);

            Park newPark = new Park();

            List<Campground> campground = campgroundDAL.ViewCampgrounds(newPark);

            Assert.AreEqual(0, campground.Count);
        }

    }
}