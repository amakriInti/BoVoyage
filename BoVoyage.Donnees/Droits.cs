using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BoVoyage.Donnees
{
    public class Droits
    {
        /*----------------------------------------
        //Initialisation des roles
        ----------------------------------------*/
        public Dictionary<string, StatutEnum> Load()
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

            //Attribution des roles
            Dictionary<string, StatutEnum> etat = new Dictionary<string, StatutEnum>();
            etat.Add("krystal.ml.frances@gmail.com", StatutEnum.Admin);
            etat.Add("vincentrigoni68@gmail.com", StatutEnum.Admin);
            etat.Add("alexandre.argento@ymail.com", StatutEnum.Admin);
            etat.Add("lefevre_quentin@outlook.fr", StatutEnum.Admin);
            etat.Add("diego.striegel@gmail.com", StatutEnum.Admin);

            //Role de commercial
            etat.Add("andre@bovayage.com", StatutEnum.Commercial);

            //Attribution des roles commercial (On garde quand même)
            /*
            var mails = repository.GetAllMails(StatutEnum.Commercial);
            foreach (var mail in mails)
            {
                if (!Roles.IsUserInRole(mail, StatutEnum.Commercial.ToString())) Roles.AddUserToRole(mail, StatutEnum.Commercial.ToString());
            }
            */
            return etat;
        }
    }
}