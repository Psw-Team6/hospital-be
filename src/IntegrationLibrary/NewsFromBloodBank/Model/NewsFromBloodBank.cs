using IntegrationLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.NewsFromBloodBank.Model
{
    public class NewsFromBloodBank
    {
        public Guid Id { get; set; }
        public String title { get; set; }
        public String content { get; set; }
        public String bloodBankName { get; set; }
        public NewsFromHospitalStatus newsStatus { get; set; }
    }
}
