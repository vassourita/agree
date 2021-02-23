using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Agree.Athens.Domain.Aggregates.Account;
using Microsoft.EntityFrameworkCore;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.DataModels
{
    [Table("Users")]
    [Index(nameof(Email), IsUnique = true)]
    public class UserDbModel : DbModel
    {
        public UserDbModel()
        {
            Messages = new Collection<MessageDbModel>();
            Servers = new Collection<ServerDbModel>();
            Roles = new Collection<RoleDbModel>();
        }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(400)]
        public string PasswordHash { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(4)]
        public UserTag Tag { get; set; }

        [Required]
        public bool EmailVerified { get; set; }

        public DateTime? DeletedAt { get; set; }

        public ICollection<MessageDbModel> Messages { get; set; }

        public ICollection<ServerDbModel> Servers { get; set; }

        public ICollection<RoleDbModel> Roles { get; set; }
    }
}