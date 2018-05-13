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
    public class ReservationDALTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=Campgrounds;Integrated Security=True";
        private int numberOfReservations = 0;
        private int numberOfReservations2 = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM reservation;", conn);
                numberOfReservations = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("SELECT COUNT(*) FROM  reservation JOIN site ON site.site_id = reservation.site_id JOIN campground ON campground.campground_id = site.campground_id JOIN park ON park.park_id = campground.park_id WHERE from_date <= DATEADD(day, 30, getdate()) AND park.park_id = 1;", conn);
                numberOfReservations2 = (int)cmd.ExecuteScalar();




                cmd = new SqlCommand("INSERT INTO reservation (site_id, name, from_date, to_date, create_date) VALUES (1, 'John Smith Party', '2001-01-01', '2001-01-02', '2001-01-03'); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void ViewUpcomingReservationsTest()
        {
            ReservationDAL reservationDAL = new ReservationDAL(connectionString);
            Park newPark = new Park
            {
                ParkId = 1
            };

            List<Reservation> reservation = reservationDAL.ViewUpcomingReservations(newPark);

            Assert.AreEqual(numberOfReservations2 + 1, reservation.Count);
        }

        [TestMethod]
        public void SendConfirmationTest()
        {
            ReservationDAL reservationDAL = new ReservationDAL(connectionString);

            int reservationNumber = reservationDAL.SendConfirmation();

            Assert.AreEqual(numberOfReservations + 1, reservationNumber);
        }

        [TestMethod]
        public void AddReservationTest()
        {
            ReservationDAL reservationDAL = new ReservationDAL(connectionString);
            Reservation newReservation = new Reservation();
            List<Reservation> reservation = new List<Reservation>();
            newReservation.FromDate = DateTime.Now;
            newReservation.ToDate = DateTime.Now;
            newReservation.CreateDate = DateTime.Now;
            newReservation.SiteId = 1;
            reservationDAL.AddReservation(newReservation);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM reservation;", conn);
                int newNumberOfReservations = (int)cmd.ExecuteScalar();


                Assert.AreEqual(numberOfReservations + 2, newNumberOfReservations);
            }


        }
    }
}
