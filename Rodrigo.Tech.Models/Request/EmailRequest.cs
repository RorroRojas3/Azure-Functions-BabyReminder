using System;
using System.Collections.Generic;
using System.Text;

namespace Rodrigo.Tech.Models.Request
{
    public class EmailRequest
    {
        public string EmailAddress { get; set; }

        public Guid LanguageId { get; set; }
    }
}
