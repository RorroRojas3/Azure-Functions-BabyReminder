using System;
using System.Collections.Generic;
using System.Text;

namespace Rodrigo.Tech.Models.Response
{
    public class EmailBodyResponse
    {
        public Guid Id { get; set; }

        public string Html { get; set; }

        public Guid LanguageId { get; set; }
    }
}
