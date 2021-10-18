using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace connxWeb.ViewModels
{
    public class CreditCard
    {
        [Required]
        public string CardNumber { get; set; }

        public string SelectedCardTypeId { get; set; }

        public IEnumerable<SelectListItem> CardTypes { get; set; }        

        public Boolean isValidCreditCard { get; set; }

        public string Message { get; set; }
    }
}