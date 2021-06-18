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
        private Model1 db = new Model1();

        [HttpGet]
        public byte GetPaiement()
        {
            Random rnd = new Random(); 
             byte nombre_validation =  (byte)Math.Round(rnd.NextDouble()*5);
            return nombre_validation;
        }

    }
}
