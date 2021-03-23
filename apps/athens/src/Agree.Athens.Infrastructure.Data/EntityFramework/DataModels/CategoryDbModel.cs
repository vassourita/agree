using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.DataModels
{
    [Table("Categories")]
    public class CategoryDbModel : DbModel
    {
        public CategoryDbModel()
        {
            TextChannels = new Collection<TextChannelDbModel>();
        }

        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public Guid ServerId { get; set; }
        public ServerDbModel Server { get; set; }

        public ICollection<TextChannelDbModel> TextChannels { get; set; }
    }
}