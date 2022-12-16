using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodRequests.Model
{
    public class BloodRequest
    {
        public Guid Id { get; set; }
        public BloodType Type { get; set; }
        public double Amount { get; set; }
        public String Reason { get; set; }
        public DateTime Date { get; set; }
        public String DoctorUsername { get; set; } //treba da se promijeni na ulogovanog doktora
        public Status Status { get; set; }
        public String Comment { get; set; }

        public Guid BloodBankId { get; set; }

    }
}
