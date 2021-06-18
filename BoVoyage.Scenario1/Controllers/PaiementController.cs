using BoVoyage.Scenario1.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace BoVoyage.Scenario1.Controllers
{
    public class PaiementController : ApiController
    {
        private BoVoyageContext db = new BoVoyageContext();

        [HttpGet]
        public string GetPaiement()//mettre l'objet en paramètre ici
        {
            return "1111";
        }
    }
}
