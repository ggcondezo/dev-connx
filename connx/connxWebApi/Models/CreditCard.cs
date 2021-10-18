using System;
using System.Collections.Generic;

namespace connxWebApi
{
    public class CreditCard
    {

        public CreditCardType CardTypeId { get; set; }
        public string CardNumber { get; set; }

        public IEnumerable<CardType> CardTypes { get; set; }

        public Boolean isValidCreditCard { get; set; }

    }
}