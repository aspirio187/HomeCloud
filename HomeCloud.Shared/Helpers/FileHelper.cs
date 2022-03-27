using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Shared.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        /// Check if the path represents a directory or a file. 
        /// </summary>
        /// <param name="path">The path to check</param>
        /// <returns>true If it is a file. false If it is a directory</returns>
        public static bool IsFile(string path)
        {
            FileAttributes attr = File.GetAttributes(path);

            if (attr.HasFlag(FileAttributes.Directory)) return false;

            return true;
        }
    }
}
