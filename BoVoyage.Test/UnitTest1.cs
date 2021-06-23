using BoVoyage.Donnees;
using BoVoyage.Metier;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Security;

namespace BoVoyage.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRepo()
        {
            // Attention à vérifier la connexion string dans le App.config du projet BoVoyage.Test
            Repository repository = new Repository();
            repository.LoadDroits();
        }

        [TestMethod]
        public void TestLoadDroits()
        {
            // Attention à vérifier la connexion string dans le App.config du projet BoVoyage.Test
            ClassMetier metier = new ClassMetier();
            metier.Load();
            Assert.IsTrue(Roles.IsUserInRole("krystal.ml.frances@gmail.com", StatutEnum.Admin.ToString()));
            Assert.IsTrue(Roles.IsUserInRole("vincentrigoni68@gmail.com", StatutEnum.Admin.ToString()));
            Assert.IsTrue(Roles.IsUserInRole("alexandre.argento@ymail.com", StatutEnum.Admin.ToString()));
            Assert.IsTrue(Roles.IsUserInRole("lefevre_quentin@outlook.fr", StatutEnum.Admin.ToString()));
            Assert.IsTrue(Roles.IsUserInRole("diego.striegel@gmail.com", StatutEnum.Admin.ToString()));
        }

        /*
        [TestMethod]
        public void TestDossiers()
        {
            Repository repo = new Repository();

            var l = repo.GetDossiers();
        }
        */
    }
}
