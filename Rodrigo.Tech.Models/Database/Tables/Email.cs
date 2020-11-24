using Rodrigo.Tech.Models.Database.Repository.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rodrigo.Tech.Models.Database.Tables
{
    [Table(nameof(Email))]
    public class Email : IEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Language { get; set; }
    }
}
