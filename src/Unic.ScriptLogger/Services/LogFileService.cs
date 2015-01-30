using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Unic.ScriptLogger.Services
{
    using System.IO;
    using System.Web.Hosting;
    using Sitecore;
    using Sitecore.Configuration;

    public class LogFileService : ILogFileService
    {
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

        public virtual IEnumerable<string> GetAllFiles()
        {
            var directory = this.GetLogFileDirectory();
            var files = directory.GetFiles("*.txt");
            return files.Select(file => file.Name.Replace(".txt", string.Empty)).Except(new[] { "readme" });
        }

        private string GetFullPath(string fileName)
        {
            return fileName.Contains(":\\") ? fileName : HostingEnvironment.MapPath(fileName);
        }

        private string GetLogFileDirectoryPath()
        {
            return Settings.GetSetting("ScriptLogger.LogFileDirectory");
        }

        private DirectoryInfo GetLogFileDirectory()
        {
            return new DirectoryInfo(this.GetFullPath(this.GetLogFileDirectoryPath()));
        }

        private string GetLatestFile(string type)
        {
            var directory = this.GetLogFileDirectory();
            var files = directory.GetFiles(string.Format("{0}*.txt", type));
            var lastFile = files.OrderByDescending(file => file.LastWriteTime).FirstOrDefault();
            return lastFile != null ? lastFile.Name : string.Empty;
        }
    }
}