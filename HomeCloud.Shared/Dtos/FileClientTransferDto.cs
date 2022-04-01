using HomeCloud.FSWatcher;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCloud.Shared.Dtos
{
    public class FileClientTransferDto
    {
        public IFormFile File { get; set; }
        public Change Change { get; set; }
    }
}
