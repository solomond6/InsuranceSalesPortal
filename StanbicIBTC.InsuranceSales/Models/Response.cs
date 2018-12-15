using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StanbicIBTC.InsuranceSales.Models
{
    public class Response
    {
        public string name { get; set; }

        public InsuranceTypeComponent Items { get; set; }
    }
}