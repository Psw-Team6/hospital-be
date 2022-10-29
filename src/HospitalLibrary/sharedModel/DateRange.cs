﻿using System;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.sharedModel
{
    [Owned]
    public class DateRange
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public bool IsValidRange()
        {
            return From < To;
        }
    }
}