using System;
using System.Collections.Generic;

namespace HospitalLibrary.sharedModel
{
    public class Address
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string StreetNumber{ get; set; }
        public string Country{ get; set; }
        public string Street{ get; set; }
        public int Postcode{ get; set; }
    }
}