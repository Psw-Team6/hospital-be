using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.BloodUnits.Model
{
    public class BloodUnitForStatisticDto
    {
        private Guid Id { get; set; }
        private BloodType BloodType { get; set; }
        private int Amount { get; set; }
        private String BloodBankName { get; set; }
        private DateTime Date { get; set; } //date of acquisition
        private String Source { get; set; }
    }
}
