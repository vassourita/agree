using System;
using System.ComponentModel.DataAnnotations;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.DataModels
{
    public abstract class DbModel
    {
        protected DbModel()
        { }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}