namespace Unic.ScriptLogger.Services
{
    using System.Collections.Generic;

    public interface ILogFileService
    {
        string GetLogFileContent(string fileName = "");

        string GetFileContent(string fileName);

        IEnumerable<string> GetAllFiles();
    }
}