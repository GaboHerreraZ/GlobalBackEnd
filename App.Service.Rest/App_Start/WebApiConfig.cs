﻿using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;


namespace App.Service.Rest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Configuración y servicios de API web
            var enableCorsAttribute = new EnableCorsAttribute("*","*","*");

            config.EnableCors(enableCorsAttribute);

            config.MapHttpAttributeRoutes();
            // Rutas de API web

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

        }
    }
}
