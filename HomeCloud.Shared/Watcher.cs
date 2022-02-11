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
    public delegate void ErrorOccured(string message);

    public class Watcher
    {
        public event ErrorOccured Erreur;
        public FileSystemWatcher FileWatcher { get; private set; }
        public string ReceiverFullPath { get; private set; }

        public Watcher(string folderFullPath, string receiverFullPath)
        {
            if (string.IsNullOrEmpty(folderFullPath)) throw new ArgumentNullException(nameof(folderFullPath));
            if (string.IsNullOrEmpty(receiverFullPath)) throw new ArgumentNullException(nameof(receiverFullPath);
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

            ReceiverFullPath = receiverFullPath;

            FileWatcher = new FileSystemWatcher(folderFullPath);
        }

        public void Start()
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

        public void Stop()
        {
            FileWatcher.Dispose();
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
    }
}
