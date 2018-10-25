﻿using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface IReservation
    {
        IList<Reservation> GetReservations();
        int CreateReservation(Reservation newReservation);
    }
}
