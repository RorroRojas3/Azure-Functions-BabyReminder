using Rodrigo.Tech.Repository.Pattern.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rodrigo.Tech.Repository.Tables
{
    [Table(nameof(EmailBody))]
    public class EmailBody : IEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Html {get; set;}

        [Required]
        public Guid LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
    }
}
