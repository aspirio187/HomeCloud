using HomeCloud.Desktop.Iterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Desktop.Managers
{
    public class FilesManager
    {
        public FilesIterator FilesToLoad { get; set; } = new FilesIterator();

        public Task FilesSync { get; private set; }

        public FilesManager()
        {
            FilesToLoad.OnElementAdded += FileAdded;
            FilesToLoad.OnElementDeleted += FileDeleted;

            FilesSync = new Task(SyncFile);
        }

        private void FileDeleted()
        {
            throw new NotImplementedException();
        }

        private void FileAdded()
        {
            if (FilesSync.Status != TaskStatus.Running)
            {
                FilesSync.Start();
            }
        }

        public void SyncFile()
        {
            int i = 0;
            while(FilesToLoad.Length != 0)
            {

            }
        }
    }
}
