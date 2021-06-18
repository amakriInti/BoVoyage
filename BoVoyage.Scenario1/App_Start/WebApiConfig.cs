using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BoVoyage.Scenario1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuration et services API Web

            // Itinéraires de l'API Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "RoutePaiement",
                routeTemplate: "api/Paiement/{id}",//Controleur Photo du WebApi. Donc pas une page web mais juste un appel à une ressource
            defaults: new { controller = "Paiement", id = RouteParameter.Optional }
            );
        }
    }
}
