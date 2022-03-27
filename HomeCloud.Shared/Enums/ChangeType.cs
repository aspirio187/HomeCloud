using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Shared
{
    /// <summary>
    /// Represent the different types of changes that can occur
    /// </summary>
    public enum ChangeType
    {
        Changed,
        Deleted,
        Created,
        Renamed
    }
}
