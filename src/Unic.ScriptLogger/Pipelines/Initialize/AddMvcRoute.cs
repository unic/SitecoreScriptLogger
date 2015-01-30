namespace Unic.ScriptLogger.Pipelines.Initialize
{
    using Sitecore.Pipelines;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Pipeline processor to add the Mvc route needed for this module.
    /// </summary>
    public class AddMvcRoute
    {
        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(PipelineArgs args)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// Registers the needed route.
        /// </summary>
        /// <param name="routes">The routes collection.</param>
        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("ScriptLoggerRoute", "scriptlogger/{action}/{filename}", new { controller = "ScriptLogger", action = "Index", filename = UrlParameter.Optional });
        }
    }
}