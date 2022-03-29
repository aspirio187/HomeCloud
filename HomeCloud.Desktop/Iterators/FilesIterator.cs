using HomeCloud.Helpers;
using HomeCloud.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Desktop.Iterators
{
    /// <summary>
    /// Collection of <see cref="Change"/> objects
    /// </summary>
    public class FilesIterator : IEnumerable<Change>
    {
        /// <summary>
        /// Array of type Change on which we work
        /// </summary>
        private Change[]? _changes = null;

        /// <summary>
        /// Position in FilesIterator
        /// </summary>
        private int _position = -1;

        /// <summary>
        /// Delegate invoked when an element is added in FilesIterator
        /// </summary>
        public event Action? OnElementAdded;

        /// <summary>
        /// Delegate invoked when an element is deleted in FilesIterator
        /// </summary>
        public event Action? OnElementDeleted;

        /// <summary>
        /// Instanciate an empty FilesIterator
        /// </summary>
        public FilesIterator()
        {
            _changes = Array.Empty<Change>();
        }

        /// <summary>
        /// Add an element in FilesIterator and invoke <see cref="OnElementAdded"/> event
        /// </summary>
        /// <param name="change">Object <see cref="Change"/> to add in FilesIterator</param>
        /// <exception cref="NullReferenceException"></exception>
        public void Add(Change change)
        {
            if (_changes is null) throw new NullReferenceException(nameof(_changes));

            Change[] changes = new Change[_changes.Length + 1];
            Array.Copy(_changes, changes, _changes.Length);
            changes[changes.Length - 1] = change;
            _changes = changes;
            OnElementAdded?.Invoke();
        }

        /// <summary>
        /// Removes an element in FilesIterator at a given index and invoke <see cref="OnElementDeleted"/> event
        /// </summary>
        /// <param name="index">Index of the element to delete</param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void RemoveAt(int index)
        {
            if (_changes is null) throw new NullReferenceException(nameof(_changes));
            if (index < 0 || index >= _changes.Length) throw new IndexOutOfRangeException(nameof(index));

            _changes = _changes.RemoveAt(index);
        }

        /// <summary>
        /// Returns a enumerator that iterate through the collection
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through FilesIterator</returns>
        /// <exception cref="NullReferenceException"></exception>
        public IEnumerator<Change> GetEnumerator()
        {
            if (_changes is null) throw new NullReferenceException(nameof(_changes));
            return (_changes as IEnumerable<Change>).GetEnumerator();
        }

        /// <summary>
        /// Returns a enumerator that iterate through the collection
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through FilesIterator</returns>
        /// <exception cref="NullReferenceException"></exception>
        IEnumerator IEnumerable.GetEnumerator()
        {
            if (_changes is null) throw new NullReferenceException(nameof(_changes));
            return (_changes as IEnumerable<Change>).GetEnumerator();
        }
    }
}
