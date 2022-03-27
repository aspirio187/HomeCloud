using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Shared.Helpers
{
    public static class FileHelper
    {
        public static bool IsFile(string path)
        {
            if (File.Exists(path) || Directory.Exists(path))
            {
                FileAttributes attr = File.GetAttributes(path);

                if (attr.HasFlag(FileAttributes.Directory)) return false;
            }

            return true;
        }
    }
}
