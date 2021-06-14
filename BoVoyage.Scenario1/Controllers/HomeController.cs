using BoVoyage.Scenario1.Dal;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
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
            //Récupère tous les fichier .csv du répertoir et le met dans un tableau de string
            string[] files = Directory.GetFiles(@"D:\formation .net\Module C#\DemoASP\test_voyage\BoVoyage.Scenario1\Voyage_csv\","*.csv");

            //boucle pour récuperer le fichier spécifique du tableau de string
            for (int i=0; i < files.Count(); i++)
            { 
                //boucle pour intégrer les données du fichier csv dans la BDD
                foreach (var ligne in System.IO.File.ReadAllLines(files[i]))
                {
                    var tab = ligne.ToString().Split(';');
                    Context.Voyages.Add(new Voyage
                    {
                        Id = Guid.NewGuid(),
                        DateAller = DateTime.Parse(tab[0]),
                        DateRetour = DateTime.Parse(tab[1]),
                        MaxVoyageur = byte.Parse(tab[2]),
                        Fournisseur = Guid.NewGuid(),
                        PrixAchatTotal = decimal.Parse(tab[4]),
                        PrixVenteUnitaire = decimal.Parse(tab[5]),
                        Description = tab[6]
                    });
                    Context.SaveChanges();
                }
                //supprime un fichier arès l'avoir lu
                System.IO.File.Delete(files[i]);
            }
            List<Voyage> ps = new List<Voyage>();
            ps=Context.Voyages.ToList();
            return View(ps);
        }
    }
}