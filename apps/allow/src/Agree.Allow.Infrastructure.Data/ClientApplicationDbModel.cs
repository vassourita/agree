namespace Agree.Allow.Infrastructure.Data;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ClientApplications")]
public class ClientApplicationDbModel
{
    [Key]
    public Guid Id { get; private set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; private set; }

    [Required]
    [MaxLength(100)]
    public string AudienceName { get; private set; }

    [Required]
    [MaxLength(100)]
    public string AccessKey { get; private set; }
}