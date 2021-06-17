using BoVoyage.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BoVoyage.Scenario1.Controllers
{
    public class VoyagesController : ApiController
    {
        private ClassMetier metier = new ClassMetier();
        // GET api/<controller>

        public object GetVoyage(string lieu, string txtlieu)
        {
            try
            {
                if (txtlieu == "undefined") txtlieu = null;
                var ps = metier.DBVoyages(lieu, txtlieu);
                return ps;
            }
            catch (Exception ex)
            {
                return ("Non trouvé");
            }
        }
    }
}