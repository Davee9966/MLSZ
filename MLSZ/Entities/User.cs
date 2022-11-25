using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MLSZ.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public int? Phone { get; set; }
        public string? Org { get; set; }
        public string? Position { get; set; }
        public string? Role { get; set; }
        public byte[]? PwSalt { get; set; }
        public byte[]? PwHash { get; set; }

        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpires { get; set; }

        public User(string? uname = null)
        {
            //_ = uname ?? throw new ArgumentNullException(uname);
            if (uname != null) Name = uname;
            else Name = String.Empty;
        }
        public User()
        {

        }
    }
}
