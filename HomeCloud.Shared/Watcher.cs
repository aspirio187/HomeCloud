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
    public delegate void OnErrorOccured(Exception e);
    public delegate void OnChangesOccured(Change change);

    public class Watcher
    {
        public event OnErrorOccured? OnErrorOnccured;
        public event OnChangesOccured? OnChangesOccured;

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
                    if (!DirectoryHelper.DirectoryHasWriteAccess(folderFullPath, WindowsIdentity.GetCurrent().Name))
                    {
                        throw new UnauthorizedAccessException(folderFullPath);
                    }
                }
                else
                {
                    if (!DirectoryHelper.DirectoryHasWriteAccess(folderFullPath))
                    {
                        throw new UnauthorizedAccessException(folderFullPath);
                    }
                }
            }

            FileWatcher = new FileSystemWatcher(folderFullPath);
        }

        public void Start()
        {
            FileWatcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.Size
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite;

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
            OnErrorOnccured?.Invoke(e.GetException());
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            Change change = new Change(ChangesEnum.Renamed, e.FullPath, e.OldFullPath);

            OnChangesOccured?.Invoke(change);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Change change = new Change(ChangesEnum.Deleted, e.FullPath);
            OnChangesOccured?.Invoke(change);
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            Change change = new Change(ChangesEnum.Created, e.FullPath);
            OnChangesOccured?.Invoke(change);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Change change = new Change(ChangesEnum.Changed, e.FullPath);
            OnChangesOccured?.Invoke(change);
        }
    }
}
