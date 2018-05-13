using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ParkDAL
    {
        private string connectionString;

        public ParkDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Park> ViewParksMenu()
        {
            List<Park> output = new List<Park>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM park ORDER BY name", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int marker = 1;
                    while (reader.Read())
                    {
                        Park p = new Park
                        {

                            ParkId = Convert.ToInt32(reader["park_id"]),
                            Marker = marker,
                            Name = Convert.ToString(reader["name"]),
                            //State = Convert.ToString(reader["state"]),
                            //EstablishYear = Convert.ToInt32(reader["establish_year"]),
                            //Area = Convert.ToInt32(reader["area"]),
                            //Vistors = Convert.ToInt32(reader["visitors"]),
                            //Description = Convert.ToString(reader["description"])
                        };
                        marker++;

                        output.Add(p);
                    }
                }
                return output;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }


        public Park DisplayParkInfo(Park selectedPark)
        {
            Park p = new Park();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM park WHERE park_id = {selectedPark.ParkId}", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        p.ParkId = Convert.ToInt32(reader["park_id"]);
                        p.Name = Convert.ToString(reader["name"]);
                        p.State = Convert.ToString(reader["state"]);
                        p.EstablishYear = Convert.ToInt32(reader["establish_year"]);
                        p.Area = Convert.ToInt32(reader["area"]);
                        p.Vistors = Convert.ToInt32(reader["visitors"]);
                        p.Description = Convert.ToString(reader["description"]);
                    }
                }
                selectedPark = p;
                return selectedPark;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
