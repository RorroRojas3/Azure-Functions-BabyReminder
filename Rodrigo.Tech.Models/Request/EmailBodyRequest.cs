using System;

namespace Rodrigo.Tech.Models.Request
{
    public class EmailBodyRequest
    {
        public string Html { get; set; }

        public Guid LanguageId { get; set; }
    }
}
