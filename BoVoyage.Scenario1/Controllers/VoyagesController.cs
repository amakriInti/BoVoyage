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

        public object GetVoyageFormulaire(string continent, string pays, string region)
        {
            try
            {
                if (pays == "undefined" || pays == "null")
                { 
                    pays = null;
                    region = null;
                }
                var ps = metier.GetVoyageFormulaire(continent, pays, region);
                return ps;
            }
            catch (Exception ex)
            {
                return ("Non trouvé");
            }
        }

        public object GetVoyage(string lieu, string txtlieu)
        {
            try
            {
                if (txtlieu == "undefined" || txtlieu == "null")
                {
                    txtlieu = null;
                }
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