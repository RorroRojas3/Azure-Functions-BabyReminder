using System;
using System.ComponentModel.DataAnnotations;

namespace Rodrigo.Tech.Models.Request
{
    public class EmailRequest
    {
        [Required]
        public string EmailAddress { get; set; }

        public Guid LanguageId { get; set; }
    }
}
