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
        /*--------------------------------------
        //Controleur API
        ---------------------------------------*/
        //lieu = colonne de la table et txtlieu si on veux un élément en particulier
        public object GetVoyage(string lieu, string txtlieu)
        {
            try
            {
                if (txtlieu == "undefined")
                {
                    txtlieu = null;
                }
                var ps = metier.DBVoyages(lieu, txtlieu);
                return ps;
            }
            catch (Exception)
            {
                return ("Non trouvé");
            }

        }
    }
}