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
    public class SiteDALTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
        private int numberOfSites = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM site JOIN campground ON campground.campground_id = site.campground_id WHERE campground.campground_id = 1; ", conn);
                numberOfSites = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO site (campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) VALUES (1, 1, 1, 1, 1, 1); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void AllSitesInCampgroundTest()
        {
            SiteDAL siteDal = new SiteDAL(connectionString);
            int campGroundId = 1;

            List<Site> sites = siteDal.AllSitesInCampground(campGroundId);

            Assert.AreEqual(numberOfSites + 1, sites.Count);
        }

    }
}