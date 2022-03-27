using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Shared
{
    /// <summary>
    /// Enumeration of different element types
    /// </summary>
    public enum ElementType
    {
        /// <summary>
        /// If it is a file
        /// </summary>
        File,
        /// <summary>
        /// If it is a directory
        /// </summary>
        Directory,
        /// <summary>
        /// If the type of the element is unavailable
        /// </summary>
        Unavailable
    }
}
