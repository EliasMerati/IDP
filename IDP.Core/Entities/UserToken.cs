using System.ComponentModel.DataAnnotations;

namespace IDP.Core.Entities;

public class UserToken
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime TokenExpire { get; set; }

}
