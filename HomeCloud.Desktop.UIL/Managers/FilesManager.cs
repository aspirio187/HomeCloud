﻿using HomeCloud.Desktop.BLL.Services;
using HomeCloud.Desktop.Iterators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Desktop.Managers
{
    /// <summary>
    /// Manager that take care of synching files on which an action happened with the server
    /// </summary>
    public class FilesManager
    {
        private readonly FileService _fileService;
        /// <summary>
        /// The enumerable in which every changes are loaded
        /// </summary>
        public FilesIterator UploadFiles { get; set; } = new FilesIterator();

        /// <summary>
        /// The task that is going to be runned after an element is added in <see cref="UploadFiles"/>
        /// </summary>
        public Task FilesSync { get; private set; }

        /// <summary>
        /// Create <see cref="FilesManager"/> object
        /// </summary>
        public FilesManager()
        {
            _fileService = new FileService();

            UploadFiles.OnElementAdded += FileAdded;
            UploadFiles.OnElementDeleted += FileDeleted;

            FilesSync = new Task(async () => await SyncFile());
        }

        /// <summary>
        /// Method invoked when an element is deleted from <see cref="UploadFiles"/>
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void FileDeleted()
        {
            Debug.WriteLine("Une erreur est survenue dans le FilesManager");
        }

        /// <summary>
        /// Method run when an element is added in <see cref="UploadFiles"/>
        /// </summary>
        private void FileAdded()
        {
            if (FilesSync.Status == TaskStatus.Created)
            {
                FilesSync.Start();
            }
            else if (FilesSync.Status == TaskStatus.RanToCompletion)
            {
                FilesSync = new Task(async () => await SyncFile());
                FilesSync.Start();
            }
            else
            {

            }
        }

        /// <summary>
        /// <see cref="FilesSync"/>'s action executed when an element is added in <see cref="UploadFiles"/>
        /// </summary>
        public async Task SyncFile()
        {
            int i = 0;
            while (UploadFiles.Length != 0)
            {
                // TODO : Instructions to send the file to the server
                // TODO : Instructions to add modifications in the logger
                Debug.WriteLine($"Type : {UploadFiles[i].ChangeType} | File : {UploadFiles[0].FileFullPath}");
                await _fileService.SendFile(UploadFiles[i]);
                UploadFiles.RemoveAt(i);
            }
        }
    }
}
