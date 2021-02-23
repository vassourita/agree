using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Agree.Athens.Domain.Aggregates.Servers;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.DataModels
{
    [Table("Roles")]
    public class RoleDbModel : DbModel
    {
        public RoleDbModel()
        {
            Users = new Collection<UserDbModel>();
        }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(6)]
        public ColorHex ColorHex { get; private set; }

        [Required]
        public bool CanUpdateServerName { get; set; }

        [Required]
        public bool CanDeleteServer { get; set; }

        [Required]
        public bool CanAddUsers { get; set; }

        [Required]
        public bool CanRemoveUsers { get; set; }

        public ServerDbModel Server { get; set; }

        [Required]
        public Guid ServerId { get; set; }

        public ICollection<UserDbModel> Users { get; set; }
    }
}