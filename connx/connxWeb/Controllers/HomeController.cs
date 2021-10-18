using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using connxWeb.Models;
using Newtonsoft.Json;

namespace connxWeb.Controllers
{
    public class HomeController : Controller
    {
        private string Baseurl = "http://localhost:58142/api";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CreditCard() 
        {
            var creditCard = new ViewModels.CreditCard();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync(client.BaseAddress + "/CreditCard/cardTypes");
                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    var creditCardTypes = JsonConvert.DeserializeObject<List<CardType>>(response);

                    var selectedListItems = new List<SelectListItem>();
                    foreach (var c in creditCardTypes)
                    {
                        var selectedItem = new SelectListItem()
                        {
                            Text = c.CardTypeName,
                            Value = c.CardTypeId.ToString()
                        };
                        selectedListItems.Add(selectedItem);
                    }
                    creditCard.CardTypes = selectedListItems;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                return View(creditCard);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreditCard(ViewModels.CreditCard creditCard) {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.GetAsync(client.BaseAddress + "/CreditCard/" + creditCard.SelectedCardTypeId + "/" + creditCard.CardNumber);
                    if (Res.IsSuccessStatusCode)
                    {
                        var response = Res.Content.ReadAsStringAsync().Result;
                        var validateCreditCard = JsonConvert.DeserializeObject<CreditCard>(response);

                        creditCard.Message = validateCreditCard.isValidCreditCard ? " Card is Valid " : " Card is Invalid ";
                        creditCard.Message = creditCard.Message + validateCreditCard.CardTypes.First(c => c.CardTypeId == validateCreditCard.CardTypeId).CardTypeName;
                        var selectedListItems = new List<SelectListItem>();
                        foreach (var c in validateCreditCard.CardTypes)
                        {
                            var selectedItem = new SelectListItem()
                            {
                                Text = c.CardTypeName,
                                Value = c.CardTypeId.ToString()
                            };
                            selectedListItems.Add(selectedItem);
                        }
                        creditCard.CardTypes = selectedListItems;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }

                    return View(creditCard);
                }
            }

            return View(creditCard);
        }
    }
}