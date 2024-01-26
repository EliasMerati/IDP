using System.ComponentModel.DataAnnotations;

namespace IDP.Core.Entities;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(200)]
    public string FullName { get; set; }

    [Required]
    public int UserAge { get; set; }

    [Required]
    [MaxLength(200)]
    public string UserName { get; set; }

    [Required]
    [MaxLength(200)]
    public string Password { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.Now;

    public bool IsActive { get; set; } = true;
}
