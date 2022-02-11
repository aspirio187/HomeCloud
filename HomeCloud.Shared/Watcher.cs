using HomeCloud.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Shared
{
    public class Watcher
    {
        public FileSystemWatcher FileWatcher { get; private set; }

        public Watcher(string folderFullPath)
        {
            if (string.IsNullOrEmpty(folderFullPath)) throw new ArgumentNullException(nameof(folderFullPath));
            if (!Directory.Exists(folderFullPath))
            {
                Directory.CreateDirectory(folderFullPath);
            }
            else
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    DirectoryHelper.DirectoryHasWriteAccess(folderFullPath, WindowsIdentity.GetCurrent().Name);
                }
                else
                {
                    DirectoryHelper.DirectoryHasWriteAccess(folderFullPath);
                }
            }

            FileWatcher = new FileSystemWatcher(folderFullPath);
        }

        private void Start(string folderFullPath)
        {
            FileWatcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.Size;

            FileWatcher.Changed += OnChanged;
            FileWatcher.Created += OnCreated;
            FileWatcher.Deleted += OnDeleted;
            FileWatcher.Renamed += OnRenamed;
            FileWatcher.Error += OnError;

            FileWatcher.IncludeSubdirectories = true;
            FileWatcher.EnableRaisingEvents = true;
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Stop()
        {
            FileWatcher.Dispose();
        }
    }
}
