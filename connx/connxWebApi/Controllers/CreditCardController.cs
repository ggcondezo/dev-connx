using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;

namespace connxWebApi
{
    public class CreditCardPatterns
    {
        public static readonly int[] AMEX_LENGTHS = { 15 };
        public static readonly string[] AMEX_STARTS = { "34", "37" };

        public static readonly int[] DISCOVER_LENGTHS =  { 16 };
        public static readonly string[] DISCOVER_STARTS = { "6011" };

        public static readonly int[] MASTERCARD_LENGTHS =  { 16 };
        public static readonly string[] MASTERCARD_STARTS = { "51", "52", "53", "54", "55" };

        public static readonly int[] VISA_LENGTHS = { 13, 16 };
        public static readonly string[] VISA_STARTS =  { "4" };
    }

    public class CreditCardController : ApiController {
       
        private static readonly Dictionary<char, int> CharacterMap = "0123456789"
        .Select((x, i) => (x, i))
        .ToDictionary(x => x.x, x => x.i);

        [HttpGet]
        [Route("api/CreditCard/{cardTypeId}/{cardNumber}")]
        /* http://localhost:58142/api/CreditCard/3/4408041234567893   */
        public CreditCard CreditCard(CreditCardType cardTypeId, string cardNumber)        
        {
            var creditCard = new CreditCard();

            var cardType = VerifyCardTypePattern(cardNumber);

            if (cardType == CreditCardType.UNKONWN) {
                creditCard.isValidCreditCard = false;
            }
            else if (cardTypeId != CreditCardType.UNKONWN && cardTypeId != cardType) {
                creditCard.isValidCreditCard = false;
            }
            else if (ValidateWithLuhn(cardNumber)) {
                creditCard.isValidCreditCard = true;
            } 
            else {
                creditCard.isValidCreditCard = false;
            }
 
            creditCard.CardNumber = cardNumber;
            creditCard.CardTypes = CardTypes();
            creditCard.CardTypeId = cardTypeId;

            if (cardTypeId == CreditCardType.UNKONWN) {
                creditCard.CardTypeId = cardType;
            }
 
            return creditCard;
        }

        [HttpGet]
        [Route("api/CreditCard/cardTypes")]
        public IEnumerable<CardType> CardTypes()
        {
            var cardTypes = new List<CardType>() { 
                new CardType(){CardTypeId = (int)CreditCardType.AMEX, CardTypeName="AMEX"},
                new CardType(){CardTypeId = (int)CreditCardType.DISCOVER, CardTypeName="DISCOVER"},
                new CardType(){CardTypeId = (int)CreditCardType.MASTERCARD, CardTypeName="MASTERCARD"},
                new CardType(){CardTypeId = (int)CreditCardType.VISA, CardTypeName="VISA"},
                new CardType(){CardTypeId = (int)CreditCardType.UNKONWN, CardTypeName="UNKNOWN"},
            };
            return cardTypes;
        }
       
        private Boolean ValidateWithLuhn(string cardNumber)
        {
            int @base = CharacterMap.Count;
            var parity = false;
            var sum = 0;
            var isValidCard = false;

            // process the number from right to left
            foreach (var c in cardNumber.Reverse())
            {
                var digit = CharacterMap[c];

                // From the rightmost digit, which is the check digit, and moving left, double the value of every second digit.
                if (parity)
                {
                    digit *= 2;

                    // If the result of this doubling operation is greater than 9, then add the digits of the product.
                    if (digit >= @base)
                        digit = digit % @base + 1;
                }

                parity = !parity;
                sum += digit;
            }
            isValidCard = sum % @base == 0? true : false;
            return isValidCard;
        }

        private CreditCardType VerifyCardTypePattern(string cardNumber) {
            var cardType = CreditCardType.UNKONWN;

            if (!cardNumber.All(CharacterMap.ContainsKey)) {
                return cardType;
            }

            if (CreditCardPatterns.AMEX_LENGTHS.Contains(cardNumber.Length) && 
                CreditCardPatterns.AMEX_STARTS.Any(s => cardNumber.StartsWith(s)) )  {
                cardType = CreditCardType.AMEX;
            }
            else if (CreditCardPatterns.DISCOVER_LENGTHS.Contains(cardNumber.Length) && 
                     CreditCardPatterns.DISCOVER_STARTS.Any(s => cardNumber.StartsWith(s))) {
                cardType = CreditCardType.DISCOVER;
            }
            else if (CreditCardPatterns.MASTERCARD_LENGTHS.Contains(cardNumber.Length) &&
                     CreditCardPatterns.MASTERCARD_STARTS.Any(s => cardNumber.StartsWith(s))) {
                cardType = CreditCardType.MASTERCARD;
            }
            else if (CreditCardPatterns.VISA_LENGTHS.Contains(cardNumber.Length) &&
                     CreditCardPatterns.VISA_STARTS.Any(s => cardNumber.StartsWith(s))) {
                cardType = CreditCardType.VISA;
            }
             
            return cardType;
        }
    }
}
