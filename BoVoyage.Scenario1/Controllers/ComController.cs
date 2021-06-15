using BoVoyage.Scenario1.Dal;
using BoVoyage.Scenario1.Models;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyage.Scenario1.Controllers
{
    [Authorize(Roles = "Commercial, Admin")]
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
            var Doss2 = Repo.GetAllDossiersEmp(User.Identity.GetUserName());
            ViewBag.DossEnum = new DossierEnum();
            var Doss3 = Doss2[0];
            return View(Doss2);
        }

        public ActionResult AffecterCommercial(Guid? id)
        {
            Repository Repo = new Repository();
            bool valid = Repo.ChangeCommercial(id, User.Identity.GetUserName());
            return View("Dossier");
        }

        public ActionResult ValidDossier(Guid? id)
        {
            Repository Repo = new Repository();
            bool valid = Repo.ValiderDossier(id);
            //ViewBag.Id_current = Context.Employes.Where(e => e.Login == User.Identity.GetUserName())
            return RedirectToAction("Dossier");
        }
    }
}