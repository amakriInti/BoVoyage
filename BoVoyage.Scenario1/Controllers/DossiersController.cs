using BoVoyage.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BoVoyage.Scenario1.Controllers
{
    /*--------------------
    //Controleur API
    --------------------*/
    public class DossiersController : ApiController
    {
        //à vérifier
        private ClassMetier metier = new ClassMetier();
        // GET: api/Dossiers
        public object GetDossiers()
        {
            return metier.GetDossiers();
        }

        // GET: api/Dossiers/5
        public string Get(int id)
        {
            return "value";
        }
        

    }
}
