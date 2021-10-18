using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace connxWebApi
{
    public enum CreditCardType { AMEX, DISCOVER, MASTERCARD, VISA, UNKONWN }
    public class CardType
    {   
        public int CardTypeId { get; set; }
        public String CardTypeName{ get; set; }

    }
}