using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitForms
{
    class OffNetDownloader
    {
        public string StartUrl { get; private set; }
        public int StartIndex { get; private set; }
        public int EndIndex { get; private set; }

        public OffNetDownloader(string startUrl, int startIndex, int endIndex)
        {
            StartUrl = startUrl;
            StartIndex = startIndex;
            EndIndex = endIndex;
        }

        public async Task ProcessGallery(string folderName, Action<byte[], int> imageProcessor)
        {
            for (int index = StartIndex; index <= EndIndex; index++)
            {
                var idLocation = StartUrl.LastIndexOf(StartIndex.ToString());
                var indexLength = StartIndex.ToString().Length;
                var url = $"{StartUrl.Substring(0, idLocation)}{index}{StartUrl.Substring(idLocation + indexLength)}";
                var wc = new WebClient();
                var data = await wc.DownloadDataTaskAsync(url);
                imageProcessor(data, index - StartIndex);
            }
        }
        
    }
}
