using System.IO;

namespace Rodrigo.Tech.Models.Response
{
    public class EmailBodyFileResponse
    {
        public Stream File { get; set; }

        public string FileName { get; set; }
    }
}
