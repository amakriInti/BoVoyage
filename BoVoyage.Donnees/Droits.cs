using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BoVoyage.Donnees
{
    public class Droits
    {
        private Repository repository = new Repository();

        /*----------------------------------------
        //Initialisation des roles
        ----------------------------------------*/
        public void Load()
        {
            // aspnet_regsql.exe -S .\SQLEXPRESS -E -A all -d BoVoyage

            // Mettre dans web.config dans sytem.web :     
            //< roleManager enabled = "true" cacheRolesInCookie = "true" cookieName = ".ASPROLES" defaultProvider = "DefaultRoleProvider" >
            //         < providers >
            //           < add connectionStringName = "BoVoyage" applicationName = "/" name = "DefaultRoleProvider" type = "System.Web.Security.SqlRoleProvider" />
            //                </ providers >
            //              </ roleManager >
            
            //Création des roles
            if (!Roles.RoleExists(StatutEnum.Commercial.ToString())) Roles.CreateRole(StatutEnum.Commercial.ToString());
            if (!Roles.RoleExists(StatutEnum.Admin.ToString())) Roles.CreateRole(StatutEnum.Admin.ToString());

            //Attribution des roles admin
            if (!Roles.IsUserInRole("krystal.ml.frances@gmail.com", StatutEnum.Admin.ToString())) Roles.AddUserToRole("krystal.ml.frances@gmail.com", StatutEnum.Admin.ToString());
            if (!Roles.IsUserInRole("vincentrigoni68@gmail.com", StatutEnum.Admin.ToString())) Roles.AddUserToRole("vincentrigoni68@gmail.com", StatutEnum.Admin.ToString());
            if (!Roles.IsUserInRole("alexandre.argento@ymail.com", StatutEnum.Admin.ToString())) Roles.AddUserToRole("alexandre.argento@ymail.com", StatutEnum.Admin.ToString());
            if (!Roles.IsUserInRole("lefevre_quentin@outlook.fr", StatutEnum.Admin.ToString())) Roles.AddUserToRole("lefevre_quentin@outlook.fr", StatutEnum.Admin.ToString());
            if (!Roles.IsUserInRole("diego.striegel@gmail.com", StatutEnum.Admin.ToString())) Roles.AddUserToRole("diego.striegel@gmail.com", StatutEnum.Admin.ToString());

            //Attribution des roles commercial
            var mails = repository.GetAllMails(StatutEnum.Commercial);
            foreach (var mail in mails)
            {
                if (!Roles.IsUserInRole(mail, StatutEnum.Commercial.ToString())) Roles.AddUserToRole(mail, StatutEnum.Commercial.ToString());
            }
        }
    }
}