using BoVoyage.Scenario1.Dal;
using BoVoyage.Scenario1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyage.Scenario1.Controllers
{
    [Authorize(Roles ="Commercial, Admin")]
    public class ComController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadCsv()
        {
            Repository Repo = new Repository();
            Repo.ReadCsv();
            return View("Index");
        }

        public ActionResult Dossier()
        {
            Repository Repo = new Repository();
            var Doss = Repo.GetAllDossiers();
            ViewBag.DossEnum = new DossierEnum();
            return View(Doss);
        }
    }
}