using PrimeiroProjeto.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeiroProjeto.Model
{
    [Table("Users")]
    public class User:BaseEntity
    {
        [Column("user_name")]
        public string Username {  get; set; }
        [Column("full_name")]
        public string FullName {  get; set; }
        [Column("password")]
        public string Password {  get; set; }
        [Column("refresh_token")]
        public string? RefreshToken {  get; set; }
        [Column("refresh_token_expiry_time")]
        public DateTime? RefreshTokenExpiryTime {  get; set; }
    }
}
