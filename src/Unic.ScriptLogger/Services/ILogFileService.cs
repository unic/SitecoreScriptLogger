namespace Unic.ScriptLogger.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// Service interface for log files.
    /// </summary>
    public interface ILogFileService
    {
        /// <summary>
        /// Gets the content of a log file, or the default log file if not specified.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Raw content of the file.</returns>
        string GetLogFileContent(string fileName = "");

        /// <summary>
        /// Gets the content of a file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Raw content of the file.</returns>
        string GetFileContent(string fileName);

        /// <summary>
        /// Gets all files available in the log directory.
        /// </summary>
        /// <returns>List of file names.</returns>
        IEnumerable<string> GetAllFiles();
    }
}