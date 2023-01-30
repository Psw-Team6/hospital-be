﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.SendMail
{
    public class EmailOptions
    {
        public const string SendGridEmail = "EmailOptions";
        public string APIKey = Environment.GetEnvironmentVariable("Psw_Email_ApiKey");
        public string FromEmail { get; set; }
        public string FromName { get; set; }
    }
}
