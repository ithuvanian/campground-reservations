using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;
using System.Configuration;
using System.Globalization;


namespace Capstone
{
    public class NationalParkCLI
    {
        string DatabaseConnection;


        public NationalParkCLI(string connectionString)
        {
            DatabaseConnection = connectionString;
        }


        public void RunCLI()
        {
            //parks menu displayed as long as the program is open

            while (true)
            {
                ViewParksMenu();
            }
        }


        private void ViewParksMenu()
        {
            Park selectedPark = new Park();

            //assemble and display list of parks from database

            ParkDAL dal = new ParkDAL(DatabaseConnection);
            List<Park> parks = dal.ViewParksMenu();

            Console.WriteLine();
            Console.WriteLine("VIEW PARKS INTERFACE");
            Console.WriteLine();
            Console.WriteLine("Select a Park for Further Details");

            foreach (Park thisPark in parks)
            {
                Console.WriteLine(thisPark.Marker + ") " + thisPark.Name);
            }
            Console.WriteLine("Q) Quit");

            //user chooses option, loop through parks to match by ID

            string userInput = Console.ReadLine().ToUpper();
            if (userInput == "Q")
            {
                Environment.Exit(0);
            }
            int userInt = int.TryParse(userInput, out userInt) ? userInt : 0;

            bool parkAssigned = false;
            foreach (Park thisPark in parks)
            {
                if (thisPark.Marker == userInt)
                {
                    selectedPark = thisPark;
                    parkAssigned = true;
                }
            }
            if (parkAssigned)
            {
                DisplayParkInfo(selectedPark);
            }
        }


        private void DisplayParkInfo(Park selectedPark)
        {
            int dateRangeSelected = -1;
            int userInput = 0;

            Console.WriteLine();
            Console.WriteLine("PARK INFORMATION SCREEN");
            Console.WriteLine();

            //access park info from database

            ParkDAL dal = new ParkDAL(DatabaseConnection);
            selectedPark = dal.DisplayParkInfo(selectedPark);

            Console.WriteLine(selectedPark.Name + " National Park");
            Console.WriteLine("State: " + selectedPark.State);
            Console.WriteLine("Established: " + selectedPark.EstablishYear);
            Console.WriteLine("Area: " + string.Format("{0:n0}", selectedPark.Area) + " sq km");
            Console.WriteLine("Annual Visitors: " + string.Format("{0:n0}", selectedPark.Vistors));
            Console.WriteLine();

            //word wrap for console display

            String[] words = selectedPark.Description.Split(' ');
            StringBuilder buffer = new StringBuilder();

            foreach (String word in words)
            {
                buffer.Append(word);

                if (buffer.Length >= Console.WindowWidth)
                {
                    String line = buffer.ToString().Substring(0, buffer.Length - word.Length);
                    Console.WriteLine(line);
                    buffer.Clear();
                    buffer.Append(word);
                }
                buffer.Append(" ");
            }
            Console.WriteLine(buffer.ToString());
            Console.WriteLine();
            Console.WriteLine("Select a Command:");
            Console.WriteLine("1) View Campgrounds in " + selectedPark.Name);
            Console.WriteLine("2) View Existing Reservations for the Next 30 Days");
            Console.WriteLine("3) Make a Reservation");
            Console.WriteLine("4) Return to Previous Screen");

            userInput = CLIHelper.GetInteger("");

            //user input implements corresponding method, default returns to ViewParksMenu()

            List<Campground> campgroundsInPark = new List<Campground>();
            switch (userInput)
            {
                case 1:
                    ViewCampgrounds(selectedPark, campgroundsInPark);
                    break;
                case 2:
                    ViewUpcomingReservations(selectedPark);
                    break;
                case 3:
                    while (dateRangeSelected == -1)
                    {
                        dateRangeSelected = ChooseCampground(selectedPark, campgroundsInPark, dateRangeSelected);
                    }
                    break;
                default:
                    Console.WriteLine();
                    break;
            }
        }


        private List<Campground> ViewCampgrounds(Park selectedPark, List<Campground> campgroundsInPark)
        {
            //assemble list of campgrounds in park from database

            CampgroundDAL dal = new CampgroundDAL(DatabaseConnection);
            campgroundsInPark = dal.ViewCampgrounds(selectedPark);

            int counter = 1;
            Console.WriteLine();
            Console.WriteLine("{0,-5}{1,-35}{2,-13}{3,-14}{4,-15}", " ", "Name", "Open", "Close", "Daily Fee");
            foreach (Campground thisCampground in campgroundsInPark)
            {
                Console.WriteLine("{0,-5}{1,-35}{2,-13}{3,-14}{4,-15}", counter + ")", thisCampground.Name, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(thisCampground.OpenFromMM), CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(thisCampground.OpenToMM), "$" + thisCampground.DailyFee + ".00");
                counter++;
            }
            Console.WriteLine();
            return campgroundsInPark;

            //returns to ViewParksMenu()
        }


        private void ViewUpcomingReservations(Park selectedPark)
        {
            //assemble list of reservations from database, display if any exist

            ReservationDAL dal = new ReservationDAL(DatabaseConnection);
            List<Reservation> reservations = dal.ViewUpcomingReservations(selectedPark);
            if (reservations.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("{0,-1}{1,-29}{2,-14}{3,-30}{4,-30}", " ", "Campground Name", "Site ID", "Reservation Start Date", "Reservation End Date");
                foreach (Reservation thisReservation in reservations)
                {
                    Console.WriteLine("{0,-2}{1,-30}{2,-18}{3,-29}{4,-30}", " ", thisReservation.Name, thisReservation.SiteId, thisReservation.FromDate.ToString("MM/dd/yyyy"), thisReservation.ToDate.ToString("MM/dd/yyyy"));
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("No Reservations Scheduled During the Next 30 Days");
                Console.WriteLine();
                Console.WriteLine();
            }
        }


        private int ChooseCampground(Park selectedPark, List<Campground> campgroundsInPark, int dateRangeSelected)
        {
            //run ViewCampgrounds() then ask for user input

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("SEARCH FOR CAMPSITE RESERVATION");
            campgroundsInPark = ViewCampgrounds(selectedPark, campgroundsInPark);
            int inputCampgroundNumber = CLIHelper.GetInteger("Which campground (enter 0 to cancel)?");
            Console.WriteLine();

            //input of zero returns to ViewParksMenu()
            //invalid number exits method and calls it again (while dateRangeSelected == -1)
            //valid number calls next method

            if (inputCampgroundNumber == 0)
            {
                Console.WriteLine();
                dateRangeSelected = 0;
            }
            else if (inputCampgroundNumber > campgroundsInPark.Count)
            {
                Console.WriteLine("Invalid Input. Please select a campground number from the list:");
            }
            else
            {
                while (dateRangeSelected == -1)
                {
                    dateRangeSelected = SearchForAvailableSites(inputCampgroundNumber, campgroundsInPark);
                }
            }
            return dateRangeSelected;
        }


        private int SearchForAvailableSites(int inputCampgroundNumber, List<Campground> campgroundsInPark)
        {
            //determine index of selected campground in list, based on input

            Campground selectedCampground = campgroundsInPark[inputCampgroundNumber - 1];
            Console.WriteLine("Selected " + selectedCampground.Name + ".");
            Console.WriteLine();

            //user specifies dates

            DateTime userFromDate = new DateTime();
            DateTime userToDate = new DateTime();
            userFromDate = CLIHelper.GetDateTime("What is the arrival date? (Please enter in \"yyyy-mm-dd\" format)");
            Console.WriteLine();
            userToDate = CLIHelper.GetDateTime("What is the departure date? (Please enter in \"yyyy-mm-dd\" format)");
            int fromMonth = userFromDate.Month;
            int toMonth = userToDate.Month;

            //ensure the arrival date is before the departure date

            if (userFromDate > userToDate)
            {
                Console.WriteLine("Invalid input. Arrival date must come before departure date.");
                Console.WriteLine();
            }

            //ensure dates have not already passed
            else if (userFromDate < DateTime.Now || userToDate < DateTime.Now)
            {
                Console.WriteLine("Invalid input. Dates must be in the future.");
                Console.WriteLine();
            }

            //ensure the park is open during desired dates
            else if (fromMonth < selectedCampground.OpenFromMM || toMonth > selectedCampground.OpenToMM)
            {
                Console.WriteLine();
                Console.WriteLine("Sorry, the campground is not open during your specified date range.");
                Console.WriteLine();
                Console.WriteLine("Press 0 to cancel or enter to use alternate dates.");
                string userInput = Console.ReadLine();
                if (userInput == "0")
                {
                    return 0;
                }
            }

            //query database for list of *already booked* reservations that interfere with desired dates
            //assemble list of their site IDs to run against all site IDs in campground

            else
            {
                CampgroundDAL dal = new CampgroundDAL(DatabaseConnection);
                List<Reservation> BookedReservations = dal.FindBookedReservations(selectedCampground, userFromDate, userToDate);
                List<int> bookedSiteIds = new List<int>();

                foreach (Reservation thisReservation in BookedReservations)
                {
                    bookedSiteIds.Add(thisReservation.SiteId);
                }

                //get list of available sites from method
                //if no sites are available, return to ViewParksMenu() by pressing 0...
                //or exit method and start ChooseCampground() again (while dateRangeSelected == -1)

                List<Site> availableSites = GetAvailableSites(selectedCampground, bookedSiteIds);

                if (availableSites.Count == 0)
                {
                    Console.WriteLine("No sites available for chosen date range. Press 0 to cancel or enter to use alternate dates.");
                    string userInput = Console.ReadLine();
                    if (userInput == "0")
                    {
                        return 0;
                    }
                }
                else
                {
                    DisplaySitesInfo(availableSites, userFromDate, userToDate, selectedCampground.DailyFee);
                    return 1;
                }
            }
            return -1;
        }


        private List<Site> GetAvailableSites(Campground selectedCampground, List<int> bookedSiteIds)
        {
            //assemble list of sites in campground from database
            //if a site's ID is NOT in the list of booked site IDs, it is added to list of *available sites*

            SiteDAL dal = new SiteDAL(DatabaseConnection);
            List<Site> allSitesInCampground = dal.AllSitesInCampground(selectedCampground.CampgroundId);

            List<Site> availableSites = new List<Site>();
            foreach (Site thisSite in allSitesInCampground)
            {
                bool booked = false;
                foreach (int bookedId in bookedSiteIds)
                {
                    if (thisSite.SiteId == bookedId)
                    {
                        booked = true;
                    }

                }
                if (booked == false)
                {
                    availableSites.Add(thisSite);
                }
            }
            return availableSites;
        }


        private string ToYesNoString(bool trueFalse)
        {
            //to display in console

            if (trueFalse == true)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }


        private void DisplaySitesInfo(List<Site> availableSites, DateTime userFromDate, DateTime userToDate, decimal dailyFee)
        {

            //display up to five available sites from list, per instructions

            Console.WriteLine();
            Console.WriteLine("Results Matching Your Criteria");
            Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}", "Site no.", "Max. Occupancy", "Accessible", "Max RV Length", "Utilities", "Cost");

            decimal numberOfDays = (decimal)(userToDate - userFromDate).TotalDays;
            decimal totalFee = dailyFee * numberOfDays;
            int end = Math.Min(availableSites.Count, 5);

            for (int i = 0; i < end; i++)
            {
                Site thisSite = availableSites[i];
                string accessible = ToYesNoString(thisSite.Accessible);
                string utilities = ToYesNoString(thisSite.Utilities);
                string rvLength = thisSite.MaxRvLength.ToString();
                if (rvLength == "0")
                {
                    rvLength = "N/A";
                }
                Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}", thisSite.SiteNumber, thisSite.MaxOccupancy, accessible, rvLength, utilities, totalFee.ToString("$0"));
            }

            //create new site to populate with data if user makes valid choice in SelectSite()

            Site potentialSite = new Site();
            bool firstAttempt = true;
            while (potentialSite.SiteId == 0)
            {
                if (firstAttempt == false)
                {
                    Console.WriteLine("Invalid input.");
                }
                potentialSite = SelectSite(availableSites, userFromDate, userToDate, potentialSite);
                firstAttempt = false;
            }
        }


        private Site SelectSite(List<Site> availableSites, DateTime userFromDate, DateTime userToDate, Site potentialSite)
        {
            Console.WriteLine();
            int userInput = CLIHelper.GetInteger("Which site number should be reserved? (enter 0 to cancel)");

            //if input is zero, unpopulated Site is returned
            //else, loop through available sites and look for a match by site number

            if (userInput == 0)
            {
                return potentialSite;
            }
            else
            {
                foreach (Site thisSite in availableSites)
                {
                    if (thisSite.SiteNumber == userInput)
                    {
                        potentialSite = thisSite;
                        Console.WriteLine();
                        Console.WriteLine($"Site {thisSite.SiteNumber} selected.");
                        Console.WriteLine();
                    }
                }
                if (potentialSite.SiteId != 0)
                {
                    string reservationName = CLIHelper.GetString("What name should the reservation be made under? ") + " Reservation";
                    AddReservation(potentialSite, userFromDate, userToDate, reservationName);
                }
            }
            return potentialSite;
        }


        private void AddReservation(Site selectedSite, DateTime userFromDate, DateTime userToDate, string reservationName)
        {
            //create Reservation object and populate it with user input data

            Reservation newReservation = new Reservation
            {
                SiteId = selectedSite.SiteId,
                Name = reservationName,
                FromDate = userFromDate,
                ToDate = userToDate,
                CreateDate = DateTime.Now
            };

            //add new reservation to database

            ReservationDAL dal = new ReservationDAL(DatabaseConnection);
            bool reservationAdded = dal.AddReservation(newReservation);
            if (reservationAdded)
            {
                Console.WriteLine();
                Console.WriteLine("****SUCCESS****");
                Console.WriteLine();
                SendConfirmation();
            }
            else
            {
                Console.WriteLine("Sorry, an error occurred.");
            }
        }


        private void SendConfirmation()
        {
            //query database for number of rows in reservation table; this serves as a confirmation number for the newest reservation

            ReservationDAL dal = new ReservationDAL(DatabaseConnection);
            int idNumber = dal.SendConfirmation();
            Console.WriteLine($"The reservation has been made, and your confirmation ID is {idNumber}.");
            Console.WriteLine();
            Console.WriteLine("Press Q to quit or enter to return to the main menu.");
            string userInput = Console.ReadLine();
            if (userInput.ToUpper() == "Q")
            {
                Environment.Exit(0);
            }
        }
    }
}