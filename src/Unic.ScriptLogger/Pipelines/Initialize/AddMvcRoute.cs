namespace Unic.ScriptLogger.Pipelines.Initialize
{
    using Sitecore.Pipelines;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class AddMvcRoute
    {
        public void Process(PipelineArgs args)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("ScriptLoggerRoute", "scriptlogger/{action}/{filename}", new { controller = "ScriptLogger", action = "Index", filename = UrlParameter.Optional });
        }
    }
}