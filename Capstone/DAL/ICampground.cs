﻿using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface ICampground
    {
        IList<Campground> GetAllCampgrounds(int park_Id);
    }
}
