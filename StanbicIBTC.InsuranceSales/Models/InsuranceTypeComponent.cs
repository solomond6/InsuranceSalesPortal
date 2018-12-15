using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StanbicIBTC.InsuranceSales.Models
{
    public class InsuranceTypeComponent
    {
        public int ID { get; set; }
        public float Percentage { get; set; }
        public Components Components { get; set; }
        public InsuranceType InsuranceType { get; set; }
        public Company Company { get; set; }

    }
}