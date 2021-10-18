using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace connxWeb.Models
{
    public class CreditCard
    {
 
        public string CardNumber { get; set; }
 
        public int CardTypeId { get; set; }

      
        public IEnumerable<CardType> CardTypes { get; set; }
        public Boolean isValidCreditCard { get; set; }

    }
}