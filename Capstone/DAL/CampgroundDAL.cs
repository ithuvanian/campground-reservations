using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CampgroundDAL
    {
        private string connectionString;

        public CampgroundDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Campground> ViewCampgrounds(Park selectedPark)
        {
            List<Campground> output = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM campground WHERE park_id = {selectedPark.ParkId}", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Campground c = new Campground
                        {
                            CampgroundId = Convert.ToInt32(reader["campground_id"]),
                            ParkId = Convert.ToInt32(reader["park_id"]),
                            Name = Convert.ToString(reader["name"]),
                            OpenFromMM = Convert.ToInt32(reader["open_from_mm"]),
                            OpenToMM = Convert.ToInt32(reader["open_to_mm"]),
                            DailyFee = Convert.ToInt32(reader["daily_fee"])
                        };

                        output.Add(c);
                    }
                }
                return output;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }


        public List<Reservation> FindBookedReservations(Campground selectedCampground, DateTime fromDate, DateTime toDate)
        {
            List<Reservation> bookedSites = new List<Reservation>();
            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($"select * from reservation JOIN site ON site.site_id = reservation.site_id JOIN campground ON campground.campground_id = site.campground_id JOIN park ON park.park_id = campground.park_id WHERE campground.campground_id = {selectedCampground.CampgroundId} AND((from_date BETWEEN '{fromDate}' AND '{toDate}') OR(to_date BETWEEN '{fromDate}' AND '{toDate}') OR(from_date > '{fromDate}' AND to_date < '{toDate}')); ", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Reservation r = new Reservation
                        {
                            SiteId = Convert.ToInt32(reader["site_id"]),
                            FromDate = Convert.ToDateTime(reader["from_date"]),
                            ToDate = Convert.ToDateTime(reader["to_date"])
                        };

                        bookedSites.Add(r);
                    }
                }
                return bookedSites;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
