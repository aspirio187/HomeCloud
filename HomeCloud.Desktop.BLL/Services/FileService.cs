using HomeCloud.FSWatcher;
using HomeCloud.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
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
            _url = "https://localhost:44305/api";

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_url);
        }

        public async Task<bool> SendFile(Change change)
        {
            if (change is null) throw new ArgumentNullException(nameof(change));

            FileClientTransferDto fileClient = new FileClientTransferDto()
            {
                Change = change,
            };

            using var stream = System.IO.File.OpenRead(change.FileFullPath);

            using var request = new HttpRequestMessage(HttpMethod.Post, string.Join('/', _url, "files"));

            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(stream), nameof(fileClient.File), change.FileFullPath.Split('\\').Last());

            foreach (var mi in typeof(Change).GetMembers())
            {

            }

            HttpContent content2 = content;

            content.Add(new StringContent(((int)change.Type).ToString()), $"{nameof(change)}.{nameof(change.Type)}");
            content.Add(new StringContent(((int)change.ChangeType).ToString()), $"{nameof(change)}.{nameof(change.ChangeType)}");
            content.Add(new StringContent(change.OldPath), $"{nameof(change)}.{nameof(change.OldPath)}");
            content.Add(new StringContent(change.FileFullPath), $"{nameof(change)}.{nameof(change.FileFullPath)}");

            request.Content = content;

            var result = await _httpClient.SendAsync(request);
            return true;
        }
    }
}
