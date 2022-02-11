using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Shared
{
    public class Renaming : Change
    {
        public string? OldPath { get; set; }
        public string? NewPath { get; set; }
    }
}
