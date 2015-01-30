namespace Unic.ScriptLogger.Authorization
{
    using System.Net;
    using System.Web.Mvc;

    public class AdministratorOnly : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Sitecore.Context.User.IsAdministrator)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}