using BoVoyage.Scenario1.Dal;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using System;

namespace BoVoyage.Scenario1.Controllers
{
    public class HomeController : Controller
    {
        private BoVoyageContext Context = new BoVoyageContext();
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

       public ActionResult LectureCsv()
        {

                // Lecture du fichier csv -> ps
                foreach (var ligne in System.IO.File.ReadAllLines((@"D:\formation .net\Module C#\DemoASP\test_voyage\BoVoyage.Scenario1\Voyage_csv\New_Voyage.csv")))
                {
                    var tab = ligne.Split(';');
                    Context.Voyages.Add(new Voyage { Id= Guid.NewGuid(), DateAller = DateTime.Parse(tab[0]), DateRetour = DateTime.Parse(tab[1]), MaxVoyageur = byte.Parse(tab[2]),
                        Fournisseur = Guid.NewGuid(), PrixAchatTotal = decimal.Parse(tab[4]), PrixVenteUnitaire = decimal.Parse(tab[5]), Description = tab[6] });
                    Context.SaveChanges();
                }
            //}
            List<Voyage> ps = new List<Voyage>();
            return View(ps);
        }
    }
}