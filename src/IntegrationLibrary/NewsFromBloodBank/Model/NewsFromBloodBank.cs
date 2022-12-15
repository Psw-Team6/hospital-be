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
        public String apiKey { get; set; }
        public NewsFromHospitalStatus newsStatus { get; set; }
        public String base64image { get; set; }
        public String bloodBankName { get; set; }

        public NewsFromBloodBank() 
        { }

        public NewsFromBloodBank(string title, string content, string apiKey, NewsFromHospitalStatus newsStatus, string base64image, string bloodBankName)
        {
            this.title = title;
            this.content = content;
            this.apiKey = apiKey;
            this.newsStatus = newsStatus;
            this.base64image = base64image;
            this.bloodBankName = bloodBankName;
            Validate();
        }

        public NewsFromBloodBank(Guid id, string title, string content, string apiKey, NewsFromHospitalStatus newsStatus, string base64image, string bloodBankName)
        {
            this.Id = id;
            this.title = title;
            this.content = content;
            this.apiKey = apiKey;
            this.newsStatus = newsStatus;
            this.base64image = base64image;
            this.bloodBankName = bloodBankName;
            Validate();
        }

        public bool IsActive()
        {
            if (this.newsStatus == NewsFromHospitalStatus.ACTIVE)
                return true;
            else
                return false;
        }

        public bool IsRefused()
        {
            if (this.newsStatus == NewsFromHospitalStatus.REFUSED)
                return true;
            else
                return false;
        }
        public bool IsOnHold()
        {
            if (this.newsStatus == NewsFromHospitalStatus.ON_HOLD)
                return true;
            else
                return false;
        }

        public void PublishNews()
        {
            if (!IsOnHold())
                throw new Exception();

            this.newsStatus = NewsFromHospitalStatus.ACTIVE;
        }

        private void Validate()
        {
            if (this.content.Equals(""))
                throw new Exception("Content can not be empty!");
            else if (this.title.Equals(""))
                throw new Exception("Title can not be empty!");
            else if (this.apiKey.Equals(""))
                throw new Exception("Api key can not be empty!");
            else if (this.bloodBankName.Equals(""))
                throw new Exception("Blood bank name can not be empty!");
        }
    }
}
