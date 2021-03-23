using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.DataModels
{
    [Table("Servers")]
    public class ServerDbModel : DbModel
    {
        public ServerDbModel()
        {
            Categories = new Collection<CategoryDbModel>();
            Users = new Collection<UserDbModel>();
            Roles = new Collection<RoleDbModel>();
        }

        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        public ICollection<CategoryDbModel> Categories { get; set; }

        public ICollection<UserDbModel> Users { get; set; }

        public ICollection<RoleDbModel> Roles { get; set; }

    }
}