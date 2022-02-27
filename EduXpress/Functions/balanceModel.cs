using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduXpress.Functions
{
    //{"Currency": "EUR", "Amount": 84.2601, "ExpirationDate": "2021-11-11T12:04:39.87",}
    public class balanceModel
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string BillingTypeID { get; set; }
        public string ExpirationDate { get; set; }
    }
}
