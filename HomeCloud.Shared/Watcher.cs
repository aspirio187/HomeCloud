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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e">Exception</param>
    public delegate void OnErrorOccured(Exception e);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="change"></param>
    public delegate void OnChangesOccured(Change change);

    /// <summary>
    /// Watch files in a directory and invoke events if changes happen
    /// </summary>
    public class Watcher
    {
        /// <summary>
        /// Represent to watched directory absolute path
        /// </summary>
        private readonly string _directoryAbsolutePath;

        /// <summary>
        /// Event invoked in an error occured during watch
        /// </summary>
        public event OnErrorOccured? OnErrorOnccured;

        /// <summary>
        /// Event invoked after a change has occured
        /// </summary>
        public event OnChangesOccured? OnChangesOccured;

        public FileSystemWatcher? FileWatcher { get; private set; }

        /// <summary>
        /// Instance of Watcher that takes the absolute path to a folder.
        /// </summary>
        /// <param name="folderFullPath">Absolute path to a folder</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public Watcher(string folderFullPath)
        {
            if (string.IsNullOrEmpty(folderFullPath)) throw new ArgumentNullException(nameof(folderFullPath));
            if (!Directory.Exists(folderFullPath))
            {
                Directory.CreateDirectory(folderFullPath);
            }
            else
            {
                if (FileHelper.IsFile(folderFullPath))
                    throw new IOException($"Element at path \"{folderFullPath}\" must be a directory");

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

            _directoryAbsolutePath = folderFullPath;
        }

        /// <summary>
        /// Start the watcher. Must be subscribed to <see cref="OnChangesOccured"/> and <see cref="OnErrorOnccured"/>
        /// before otherwise it won't work.
        /// </summary>
        public void Start()
        {
            if (FileWatcher is not null) throw new Exception($"Stop the previous Watcher before starting a new one!");

            FileWatcher = new FileSystemWatcher(_directoryAbsolutePath);

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

        /// <summary>
        /// Stop the watcher and unsubscribed every handler from <see cref="OnErrorOnccured"/> and <see cref="OnChangesOccured"/>
        /// </summary>
        public void Stop()
        {
            if (FileWatcher is null) throw new Exception($"Watcher cannot be stopped if it hasn't been started!");

            FileWatcher.Changed -= OnChanged;
            FileWatcher.Created -= OnCreated;
            FileWatcher.Deleted -= OnDeleted;
            FileWatcher.Renamed -= OnRenamed;
            FileWatcher.Error -= OnError;

            OnErrorOnccured = null;
            OnChangesOccured = null;

            FileWatcher.Dispose();
        }

        /// <summary>
        /// Called if an error happen during directory watch and invoke <see cref="OnErrorOnccured"/> delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnError(object sender, ErrorEventArgs e)
        {
            OnErrorOnccured?.Invoke(e.GetException());
        }

        /// <summary>
        /// Called if a renaming occured during directory watch and invoke <see cref="OnChangesOccured"/> delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            Change change = new Change(ChangeType.Renamed, e.FullPath, e.OldFullPath);

            OnChangesOccured?.Invoke(change);
        }

        /// <summary>
        /// Called if a deletion occured during directory watch and invoke <see cref="OnChangesOccured"/> delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Change change = new Change(ChangeType.Deleted, e.FullPath);
            OnChangesOccured?.Invoke(change);
        }

        /// <summary>
        /// Called if a creation occured during directory watch and invoke <see cref="OnChangesOccured"/> delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            Change change = new Change(ChangeType.Created, e.FullPath);
            OnChangesOccured?.Invoke(change);
        }

        /// <summary>
        /// Called if a change occured during directory watch and invoke <see cref="OnChangesOccured"/> delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            Change change = new Change(ChangeType.Changed, e.FullPath);
            OnChangesOccured?.Invoke(change);
        }
    }
}
