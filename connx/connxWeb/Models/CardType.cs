using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace connxWeb.Models
{
    public enum CreditCardType { AMEX, DISCOVER, MASTERCARD, VISA,UNKNOWN }
    public class CardType
    {
        public int CardTypeId { get; set; }
        public String CardTypeName { get; set; }
    }
}