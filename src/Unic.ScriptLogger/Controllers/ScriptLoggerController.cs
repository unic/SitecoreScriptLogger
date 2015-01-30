namespace Unic.ScriptLogger.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;
    using Unic.ScriptLogger.Services;

    public class ScriptLoggerController : Controller
    {
        private readonly ILogFileService logFileService;

        public ScriptLoggerController() : this(new LogFileService())
        {
        }

        public ScriptLoggerController(ILogFileService logFileService)
        {
            this.logFileService = logFileService;
        }

        public ActionResult Index()
        {
            var content = this.logFileService.GetLogFileContent();
            if (string.IsNullOrWhiteSpace(content)) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return this.Content(content, "text/plain");
        }

        public ActionResult File(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var content = this.logFileService.GetLogFileContent(fileName);
            if (string.IsNullOrWhiteSpace(content)) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return this.Content(content, "text/plain");
        }

        public ActionResult ListFiles()
        {
            var files = string.Join(Environment.NewLine, this.logFileService.GetAllFiles());
            var content = this.logFileService.GetFileContent("/sitecore modules/Web/ScriptLogger/ListFiles.txt");
            return this.Content(content.Replace("{files}", files), "text/plain");
        }

        public ActionResult Help()
        {
            return this.Content(this.logFileService.GetFileContent("/sitecore modules/Web/ScriptLogger/Help.txt"), "text/plain");
        }
    }
}