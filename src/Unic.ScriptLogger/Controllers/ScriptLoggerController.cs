namespace Unic.ScriptLogger.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;
    using Unic.ScriptLogger.Authorization;
    using Unic.ScriptLogger.Services;

    /// <summary>
    /// Mvc controller for the script logger
    /// </summary>
    public class ScriptLoggerController : Controller
    {
        /// <summary>
        /// The log file service
        /// </summary>
        private readonly ILogFileService logFileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptLoggerController"/> class.
        /// </summary>
        public ScriptLoggerController() : this(new LogFileService())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptLoggerController"/> class.
        /// </summary>
        /// <param name="logFileService">The log file service.</param>
        public ScriptLoggerController(ILogFileService logFileService)
        {
            this.logFileService = logFileService;
        }

        /// <summary>
        /// Index action of the controller, called when controller is called without any other action specified.
        /// </summary>
        /// <returns>The newest logfile configured in the config file.</returns>
        [AdministratorOnly]
        public ActionResult Index()
        {
            var content = this.logFileService.GetLogFileContent();
            if (string.IsNullOrWhiteSpace(content)) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return this.Content(content, "text/plain");
        }

        /// <summary>
        /// Gets the content of a specific logfile.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Content of the specified logfile.</returns>
        [AdministratorOnly]
        public ActionResult File(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var content = this.logFileService.GetLogFileContent(fileName);
            if (string.IsNullOrWhiteSpace(content)) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return this.Content(content, "text/plain");
        }

        /// <summary>
        /// Lists all available log files.
        /// </summary>
        /// <returns>List of available log files.</returns>
        [AdministratorOnly]
        public ActionResult ListFiles()
        {
            var files = string.Join(Environment.NewLine, this.logFileService.GetAllFiles());
            var content = this.logFileService.GetFileContent("/sitecore modules/Web/ScriptLogger/ListFiles.txt");
            return this.Content(content.Replace("{files}", files), "text/plain");
        }

        /// <summary>
        /// Get the help text with available commands for the module.
        /// </summary>
        /// <returns>Help text with available commands.</returns>
        [AdministratorOnly]
        public ActionResult Help()
        {
            return this.Content(this.logFileService.GetFileContent("/sitecore modules/Web/ScriptLogger/Help.txt"), "text/plain");
        }
    }
}