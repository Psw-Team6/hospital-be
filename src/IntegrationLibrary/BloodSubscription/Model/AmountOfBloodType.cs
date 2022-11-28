using IntegrationLibrary.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodSubscription.Model
{
    public class AmountOfBloodType
    {
        private BloodRequests.Model.BloodType apos;
        private int v;

        public AmountOfBloodType(BloodRequests.Model.BloodType apos, int v)
        {
            this.apos = apos;
            this.v = v;
        }

        public BloodType bloodType { get; set; }
        public int amount { get; set; }
    }
}
