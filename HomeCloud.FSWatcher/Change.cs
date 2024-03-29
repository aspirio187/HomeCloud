﻿using HomeCloud.FSWatcher.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.FSWatcher
{
    public class Change
    {
        /// <summary>
        /// Describes the change type that occured.
        /// </summary>
        public ChangeType ChangeType { get; private set; }

        /// <summary>
        /// Describes the type of data that changed (File or Directory).
        /// </summary>
        public ElementType Type { get; private set; }

        /// <summary>
        /// Describes the full file path.
        /// </summary>
        public string FileFullPath { get; private set; }

        /// <summary>
        /// Describes the file's old name with the full path to the file
        /// Changes only in case of Renamed change type.
        /// </summary>
        public string OldPath { get; private set; }

        /// <summary>
        /// Change object
        /// </summary>
        /// <param name="changes">The change type that occured</param>
        /// <param name="fullPath">The absolute file path</param>
        /// <param name="oldPath">The absolute old file path. Needed if <paramref name="changes"/> is
        /// <code>ChangesEnum.Renamed</c> <</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Change(ChangeType changes, string fullPath, string oldPath = "")
        {
            if (fullPath is null)
            {
                throw new ArgumentNullException(nameof(fullPath));
            }

            if (fullPath.Trim().Length <= 0)
            {
                throw new ArgumentException($"{nameof(fullPath)} cannot be an empty string!");
            }

            ChangeType = changes;

            if (changes == ChangeType.Deleted)
            {
                Type = ElementType.Unavailable;
            }
            else
            {
                Type = FileHelper.IsFile(fullPath) ? ElementType.File : ElementType.Directory;
            }

            FileFullPath = fullPath;

            if (changes == ChangeType.Renamed)
            {
                if (oldPath is null)
                {
                    throw new ArgumentNullException(nameof(oldPath));
                }

                if (oldPath.Trim().Length <= 0)
                {
                    throw new ArgumentException($"{nameof(oldPath)} cannot be an empty string!");
                }

                OldPath = oldPath;
            }
            else
            {
                OldPath = fullPath;
            }
        }
    }
}
