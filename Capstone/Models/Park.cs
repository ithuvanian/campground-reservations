using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Capstone.Models
{
    public class Park
    {
        public int ParkId { get; set; }
        public int Marker { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public int EstablishYear { get; set; }
        public int Area { get; set; }
        public int Vistors { get; set; }
        public string Description { get; set; }
    }
}
