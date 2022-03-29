using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HomeCloud.Desktop.Iterators
{
    public class ViewsIterator : IEnumerator<ContentControl>
    {
        /// <summary>
        /// Position in the list
        /// </summary>
        private int _position = -1;

        /// <summary>
        /// Array of ContentControls object which we manipulate
        /// </summary>
        private ContentControl[]? _controls;

        /// <summary>
        /// Get the Current ContentControl Element of the Array by Calling Current Getter
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// Get the Current ContentControl Element
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public ContentControl Current
        {
            get
            {
                if (_controls is null) throw new NullReferenceException(nameof(_controls));
                if (_position < 0 || _position >= _controls.Length) throw new ArgumentOutOfRangeException(nameof(_position));
                return _controls[_position];
            }
        }

        /// <summary>
        /// Instanciate a new ViewsIterator Enumerator
        /// </summary>
        public ViewsIterator()
        {
            _position = 0;
            _controls = new ContentControl[0];
        }

        /// <summary>
        /// Get an element of the Enumerator by its name
        /// </summary>
        /// <param name="name">ContentControl's name</param>
        /// <returns>The ContentControl with the given name. If no object is found, returns null.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public ContentControl? this[string name]
        {
            get
            {
                if (_controls is null) throw new NullReferenceException(nameof(_controls));
                for (int i = 0; i < _controls.Length; i++)
                {
                    if (_controls[i].Name.Equals(name))
                    {
                        return _controls[i];
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Get an element of the Enumerator by its index in the Enumerator
        /// </summary>
        /// <param name="index">ContentControl's index</param>
        /// <returns>The element at the given index </returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public ContentControl this[int index]
        {
            get
            {
                if (_controls is null) throw new NullReferenceException(nameof(_controls));
                if (index < 0 || index >= _controls.Length) throw new ArgumentOutOfRangeException(nameof(index));
                return _controls[index];
            }
        }

        /// <summary>
        /// Add a new ContentControl in the Enumerator
        /// </summary>
        /// <param name="contentControl"></param>
        /// <exception cref="NullReferenceException"></exception>
        public void Add(ContentControl contentControl)
        {
            if (_controls is null) throw new NullReferenceException(nameof(_controls));
            ContentControl[] controls = new ContentControl[_controls.Length + 1];
            Array.Copy(_controls, controls, controls.Length);
            controls[controls.Length - 1] = contentControl;
            _controls = controls;
        }

        /// <summary>
        /// Dispose ViewsIterator
        /// </summary>
        public void Dispose()
        {
            _controls = null;
            _position = -1;
        }

        /// <summary>
        /// Move to the next element in the Enumerator
        /// </summary>
        /// <returns>true If moving next is possible. false Otherwise</returns>
        /// <exception cref="NullReferenceException"></exception>
        public bool MoveNext()
        {
            if (_controls is null) throw new NullReferenceException(nameof(_controls));
            if (_position + 1 < _controls.Length)
            {
                _position++;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Empty the enumerator and reset the position
        /// </summary>
        public void Reset()
        {
            _controls = new ContentControl[0];
            _position = -1;
        }
    }
}
