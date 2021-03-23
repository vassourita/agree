using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.DataModels
{
    [Table("TextChannels")]
    public class TextChannelDbModel : DbModel
    {
        public TextChannelDbModel()
        {
            Messages = new Collection<MessageDbModel>();
        }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Name { get; set; }

        public CategoryDbModel Category { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public ICollection<MessageDbModel> Messages { get; set; }
    }
}