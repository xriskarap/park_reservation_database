using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Campground
    {
        public int Campground_Id { get; set; }

        public int Park_Id { get; set; }

        public string Name { get; set; }

        public DateTime Open_From_Mm { get; set; }

        public DateTime Open_To_Mm { get; set; }

        public decimal Daily_Fee { get; set; }

        //public Campground(int campground_Id, int park_Id, string name, DateTime open_From_Mm, DateTime open_To_Mm, decimal daily_Fee)
        //{
        //    this.Campground_Id = campground_Id;
        //    this.Park_Id = park_Id;
        //    this.Name = name;
        //    this.Open_From_Mm = open_From_Mm;
        //    this.Open_To_Mm = open_To_Mm;
        //    this.Daily_Fee = daily_Fee;
        //}
    }
}
