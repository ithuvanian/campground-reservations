using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;

            NationalParkCLI cli = new NationalParkCLI(connectionString);
            cli.RunCLI();
        }
    }
}
