using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Park
    {
        public int Park_Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Establish_Date { get; set; }
        public int Area { get; set; }
        public int Visitors { get; set; }
        public string Description { get; set; }

        //public Park(int park_Id, string name, string location, DateTime establish_Date, int area, int visitors, string description)
        //{
        //    this.Park_Id = park_Id;
        //    this.Name = name;
        //    this.Location = location;
        //    this.Establish_Date = establish_Date;
        //    this.Area = area;
        //    this.Visitors = visitors;
        //    this.Description = description;
        //}
    }
}
