using System;
using System.ComponentModel.DataAnnotations;
using Agree.Athens.Domain.Entities;

namespace Agree.Athens.Domain.Security
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        public string Token { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }

        public DateTime ExpiryOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedByIp { get; set; }

        public DateTime RevokedOn { get; set; }

        public string RevokedByIp { get; set; }
    }
}