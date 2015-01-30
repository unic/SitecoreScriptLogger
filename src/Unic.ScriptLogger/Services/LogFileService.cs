namespace Unic.ScriptLogger.Services
{
    using Sitecore.Configuration;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Hosting;

    /// <summary>
    /// Service implementation for log files.
    /// </summary>
    public class LogFileService : ILogFileService
    {
        /// <summary>
        /// Gets the content of a log file, or the default log file if not specified.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// Raw content of the file.
        /// </returns>
        public virtual string GetLogFileContent(string fileName = "")
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = this.GetLatestFile(Settings.GetSetting("ScriptLogger.DefaultLog"));
            }

            var filePath = this.GetLogFileDirectoryPath() + "/" + fileName;
            if (!filePath.EndsWith(".txt")) filePath += ".txt";

            return this.GetFileContent(this.GetFullPath(filePath));
        }

        /// <summary>
        /// Gets the content of a file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// Raw content of the file.
        /// </returns>
        public virtual string GetFileContent(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return string.Empty;

            var path = this.GetFullPath(fileName);
            if (!File.Exists(path)) return string.Empty;

            using (var reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Gets all files available in the log directory.
        /// </summary>
        /// <returns>
        /// List of file names.
        /// </returns>
        public virtual IEnumerable<string> GetAllFiles()
        {
            var directory = this.GetLogFileDirectory();
            var files = directory.GetFiles("*.txt");
            return files.Select(file => file.Name.Replace(".txt", string.Empty)).Except(new[] { "readme" });
        }

        /// <summary>
        /// Gets the full path of a file..
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Mapped full path</returns>
        private string GetFullPath(string fileName)
        {
            return fileName.Contains(":\\") ? fileName : HostingEnvironment.MapPath(fileName);
        }

        /// <summary>
        /// Gets the log file directory path.
        /// </summary>
        /// <returns>Path to the directory.</returns>
        private string GetLogFileDirectoryPath()
        {
            return Settings.GetSetting("ScriptLogger.LogFileDirectory");
        }

        /// <summary>
        /// Gets the log file directory.
        /// </summary>
        /// <returns>Directory info object.</returns>
        private DirectoryInfo GetLogFileDirectory()
        {
            return new DirectoryInfo(this.GetFullPath(this.GetLogFileDirectoryPath()));
        }

        /// <summary>
        /// Gets the latest file of specific type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>File name of the latest log file by type.</returns>
        private string GetLatestFile(string type)
        {
            var directory = this.GetLogFileDirectory();
            var files = directory.GetFiles(string.Format("{0}*.txt", type));
            var lastFile = files.OrderByDescending(file => file.LastWriteTime).FirstOrDefault();
            return lastFile != null ? lastFile.Name : string.Empty;
        }
    }
}