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

        public static Repository Repo = new Repository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadCsv()
        {
            Repo.ReadCsv();
            return View("Index");
        }

        public ActionResult Dossier()
        {
            var Doss2 = Repo.GetAllDossiersEmp(User.Identity.GetUserName());
            ViewBag.DossEnum = new DossierEnum();
            return View(Doss2);
        }

        public ActionResult AffecterCommercial(Guid? id)
        {
            bool valid = Repo.ChangeCommercial(id, User.Identity.GetUserName());
            return RedirectToAction("Dossier");
        }

        public ActionResult ValidDossier(Guid? id)
        {
            bool valid = Repo.ValiderDossier(id);
            //ViewBag.Id_current = Context.Employes.Where(e => e.Login == User.Identity.GetUserName())
            return RedirectToAction("Dossier");
        }

        public ActionResult RefuseDossier(Guid? id)
        {
            bool valid = Repo.RefuserDossier(id);
            //ViewBag.Id_current = Context.Employes.Where(e => e.Login == User.Identity.GetUserName())
            return RedirectToAction("Dossier");
        }
        public ActionResult AbandonnerDossier(Guid? id)
        { 
            bool valid = Repo.AbandonnerDossier(id);
            //ViewBag.Id_current = Context.Employes.Where(e => e.Login == User.Identity.GetUserName())
            return RedirectToAction("Dossier");
        }

        public ActionResult Details_Dossier(Guid? id)
        {
            //var Doss = Repo.GetAllDossiers();
            var Doss = Repo.GetDetailsDossier(id);
            return View(Doss);
        }

        public ActionResult SupprimerDossier(Guid? id)
        {
            if (id != null)
            {
                Dossier Doss = Repo.GetDossier(id);//On récupère le dossier et pour chaque voyageur inscrit on supprime le dossiers
                foreach (var v in Doss.Voyageurs)
                {
                    Voyageur vygr = Repo.GetVoyageur(v.Id);
                    vygr.Dossiers.Remove(Doss);
                }
                Repo.SupprimerDossier(id);
            }
              return RedirectToAction("Dossiers", "Com");
        }

    }
}