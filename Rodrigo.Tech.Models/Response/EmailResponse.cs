using System;
using System.Collections.Generic;
using System.Text;

namespace Rodrigo.Tech.Models.Response
{
    public class EmailResponse
    {
        public Guid Id { get; set; }

        public string EmailAddress { get; set; }

        public Guid LanguageId { get; set; }
    }
}
