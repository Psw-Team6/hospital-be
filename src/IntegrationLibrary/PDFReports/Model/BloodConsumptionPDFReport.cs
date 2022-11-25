using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.PDFReports.Model
{
    public class BloodConsumptionPDFReport
    {
        public Guid Id { get; set; }
        public Guid BloodUnitId { get; set; }
        public BloodUnit BloodUnit { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        public Guid DoctorId { get; set; }
        public String Purpose { get; set; }
    }
}
