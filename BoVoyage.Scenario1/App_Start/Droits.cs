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
            

            if (!Roles.RoleExists(StatutEnum.Commercial.ToString())) Roles.CreateRole(StatutEnum.Commercial.ToString());
            if (!Roles.RoleExists(StatutEnum.Admin.ToString())) Roles.CreateRole(StatutEnum.Admin.ToString());
            if (!Roles.IsUserInRole("ali@bovoyage.com", StatutEnum.Admin.ToString())) Roles.AddUserToRole("ali@bovoyage.com", StatutEnum.Admin.ToString());
            if (!Roles.IsUserInRole("andre@bovoyage.com", StatutEnum.Commercial.ToString())) Roles.AddUserToRole("andre@bovoyage.com", StatutEnum.Commercial.ToString());

            var mails = Repo.GetAllMails(StatutEnum.Commercial);
            foreach (var mail in mails)
            {
                if (!Roles.IsUserInRole(mail, StatutEnum.Commercial.ToString())) Roles.AddUserToRole(mail, StatutEnum.Commercial.ToString());
            }
        }
    }
}