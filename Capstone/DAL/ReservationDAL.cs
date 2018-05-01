using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ReservationDAL
    {
        private string connectionString;

        public ReservationDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Reservation> ViewUpcomingReservations(Park selectedPark)
        {
            List<Reservation> output = new List<Reservation>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT campground.name AS cname, reservation.site_id AS sid, from_date, to_date FROM  reservation JOIN site ON site.site_id = reservation.site_id JOIN campground ON campground.campground_id = site.campground_id JOIN park ON park.park_id = campground.park_id WHERE from_date <= DATEADD(day, 30, getdate()) AND park.park_id = {selectedPark.ParkId};", conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Reservation r = new Reservation
                        {
                            Name = Convert.ToString(reader["cname"]),
                            SiteId = Convert.ToInt32(reader["sid"]),
                            FromDate = Convert.ToDateTime(reader["from_date"]),
                            ToDate = Convert.ToDateTime(reader["to_date"])
                        };

                        output.Add(r);
                    }
                }
                return output;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }


        public bool AddReservation(Reservation newReservation)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($"INSERT INTO reservation(site_id, name, from_date, to_date, create_date) VALUES ({newReservation.SiteId}, '{newReservation.Name}', '{newReservation.FromDate}', '{newReservation.ToDate}', '{newReservation.CreateDate}');", conn);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }


        public int SendConfirmation()
        {
            List<Reservation> output = new List<Reservation>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM reservation;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Reservation r = new Reservation
                        {
                            ReservationId = Convert.ToInt32(reader["reservation_id"]),
                            Name = Convert.ToString(reader["name"]),
                            SiteId = Convert.ToInt32(reader["site_id"]),
                            FromDate = Convert.ToDateTime(reader["from_date"]),
                            ToDate = Convert.ToDateTime(reader["to_date"]),
                            CreateDate = Convert.ToDateTime(reader["create_date"])
                        };

                        output.Add(r);

                    }
                }
                return output.Count();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}

