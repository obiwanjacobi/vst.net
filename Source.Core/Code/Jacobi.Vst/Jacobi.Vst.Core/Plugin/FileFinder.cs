using System;
using System.Collections.Generic;
using System.IO;

namespace Jacobi.Vst.Core.Plugin
{
    /// <summary>
    /// The FileFinder class locates a file on the file system based on several paths and file extensions.
    /// </summary>
    public class FileFinder
    {
        /// <summary>
        /// A default constructor.
        /// </summary>
        public FileFinder()
        {
            Extensions = new List<string>();
            Paths = new List<string>();
        }

        /// <summary>
        /// Initializing constructor.
        /// </summary>
        /// <param name="extensions">An enumerator for a list of file extensions.</param>
        /// <param name="paths">An enumerator for a list of probe paths.</param>
        public FileFinder(IEnumerable<string> extensions, IEnumerable<string> paths)
        {
            Throw.IfArgumentIsNull(extensions, "extensions");
            Throw.IfArgumentIsNull(paths, "paths");

            Extensions = new List<string>(extensions);
            Paths = new List<string>(paths);
        }

        /// <summary>
        /// Gets the list of file extensions to try.
        /// </summary>
        /// <remarks>Include the '.' in each extension.</remarks>
        public List<string> Extensions { get; private set; }

        /// <summary>
        /// Gets a list of file paths to try.
        /// </summary>
        public List<string> Paths { get; private set; }

        /// <summary>
        /// Finds a file that exists on the file system using the <see cref="Paths"/> and <see cref="Extensions"/>.
        /// </summary>
        /// <param name="fileNameWithoutExtension">The name of the file to find (without file extension).</param>
        /// <returns>Returns null if no file was found. Otherwise the full file path is returned.</returns>
        public string Find(string fileNameWithoutExtension)
        {
            Throw.IfArgumentIsNullOrEmpty(fileNameWithoutExtension, "fileNameWithoutExtension");

            foreach (string path in Paths)
            {
                foreach (string ext in Extensions)
                {
                    string filePath = Path.Combine(path, fileNameWithoutExtension + ext);

                    if (File.Exists(filePath))
                    {
                        return filePath;
                    }
                }
            }

            return null;
        }
    }
}
