using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Shared
{
    public class Change
    {
        public WatcherChangeTypes ChangeType { get; set; }
        public string? ChangeName { get; set; }
        public string? FullPath { get; set; }
    }
}
