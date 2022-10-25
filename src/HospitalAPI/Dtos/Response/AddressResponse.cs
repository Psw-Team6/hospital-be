using System;

namespace HospitalAPI.Dtos
{
    public class AddressResponse
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string StreetNumber{ get; set; }
        public string Country{ get; set; }
        public string Street{ get; set; }
        public int Postcode{ get; set; }
    }
}