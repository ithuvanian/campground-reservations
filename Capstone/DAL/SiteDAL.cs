using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class SiteDAL
    {
        private string connectionString;

        public SiteDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Site> AllSitesInCampground(int selectedCampgroundId)
        {
            List<Site> output = new List<Site>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT * FROM site JOIN campground ON campground.campground_id = site.campground_id WHERE campground.campground_id = {selectedCampgroundId};", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Site s = new Site
                    {
                        SiteId = Convert.ToInt32(reader["site_id"]),
                        CampgroundId = Convert.ToInt32(reader["campground_id"]),
                        SiteNumber = Convert.ToInt32(reader["site_number"]),
                        MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]),
                        Accessible = Convert.ToBoolean(reader["accessible"]),
                        MaxRvLength = Convert.ToInt32(reader["max_rv_length"]),
                        Utilities = Convert.ToBoolean(reader["utilities"])
                    };

                    output.Add(s);
                }
            }
            return output;
        }
    }
}

