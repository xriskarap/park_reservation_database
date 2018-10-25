using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface ISite
    {
        IList<Site> Search_Site_Number(int campground_Id, DateTime startDate, DateTime endDate); 
    }
}