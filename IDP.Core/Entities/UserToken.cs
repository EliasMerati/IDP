using System.ComponentModel.DataAnnotations;

namespace IDP.Core.Entities
{
    public class UserToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string Token { get; set; }
    }
}
