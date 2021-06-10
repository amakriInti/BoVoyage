using BoVoyage.Scenario1.Dal;
using BoVoyage.Scenario1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BoVoyage.Scenario1
{
    public static class Droits
    {
        private static Repository Repo = new Repository();
        internal static void Load()
        {
            // aspnet_regsql.exe -S .\SQLEXPRESS -E -A all -d BoVoyage

            // Mettre dans web.config dans sytem.web :     
            //< roleManager enabled = "true" cacheRolesInCookie = "true" cookieName = ".ASPROLES" defaultProvider = "DefaultRoleProvider" >
            //         < providers >
            //           < add connectionStringName = "BoVoyage" applicationName = "/" name = "DefaultRoleProvider" type = "System.Web.Security.SqlRoleProvider" />
            //                </ providers >
            //              </ roleManager >

            if (!Roles.RoleExists(StatutEnum.Commercial.ToString())) Roles.CreateRole(StatutEnum.Commercial.ToString());
            if (!Roles.RoleExists(StatutEnum.Admin.ToString())) Roles.CreateRole(StatutEnum.Admin.ToString());
            if (!Roles.IsUserInRole("ali@bovoyage.com", StatutEnum.Admin.ToString())) Roles.AddUserToRole("ali@bovoyage.com", StatutEnum.Admin.ToString());

            var mails = Repo.GetAllMails(StatutEnum.Commercial);
            foreach (var mail in mails)
            {
                if (!Roles.IsUserInRole(mail, StatutEnum.Commercial.ToString())) Roles.AddUserToRole(mail, StatutEnum.Commercial.ToString());
            }
        }
    }
}