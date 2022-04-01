using HomeCloud.FSWatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Desktop.BLL.Services
{
    public class FileService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public FileService()
        {
            _httpClient = new HttpClient();
            _url = "https://localhost:44305/api/";
        }

        public async Task<bool> SendFile(Change change)
        {
            return true;
        }
    }
}
