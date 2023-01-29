namespace Agree.Allow.Infrastructure.Data;

using System;
using Agree.SharedKernel;
using System.Security.Claims;
using Agree.Allow.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

[Table("UserAccounts")]
[Index("Tag", "Username", IsUnique = true)]
public class UserAccountDbModel
{
    [Key]
    public Guid Id { get; private set; }

    [Required]
    [MaxLength(255)]
    public string EmailAddress { get; private set; }

    [Required]
    [MaxLength(40)]
    public string Username { get; private set; }

    [Required]
    [MaxLength(400)]
    public string PasswordHash { get; private set; }

    [Required]
    [MaxLength(4)]
    public ushort Tag { get; private set; }

    public DateTime CreatedAt { get; private set; }
}