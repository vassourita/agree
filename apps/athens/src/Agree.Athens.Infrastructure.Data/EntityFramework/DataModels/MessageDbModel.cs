using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.DataModels
{
    [Table("Messages")]
    public class MessageDbModel : DbModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(800)]
        public string Content { get; set; }

        public UserDbModel User { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public TextChannelDbModel Channel { get; private set; }

        [Required]
        public Guid TextChannelId { get; private set; }
    }
}