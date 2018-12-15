using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StanbicIBTC.InsuranceSales.Models
{
    public class Users
    {
        public int ID { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNUmber { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastLogon { get; set; }
    }
}