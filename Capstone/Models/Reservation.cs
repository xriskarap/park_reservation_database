using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Reservation
    {
        public int Reservation_Id { get; set; }

        public int Site_Id { get; set; }

        public string Name { get; set; }

        public DateTime From_Date { get; set; }

        public DateTime To_Date { get; set; }

        public DateTime Create_Date { get; set; }
        
        public Reservation(int reservation_Id, int site_Id, string name, DateTime from_Date, DateTime to_Date, DateTime create_Date)
        {
            this.Reservation_Id = reservation_Id;
            this.Site_Id = site_Id;
            this.Name = name;
            this.From_Date = from_Date;
            this.To_Date = to_Date;
            this.Create_Date = create_Date;
        }
    }
}
