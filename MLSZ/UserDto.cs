using Microsoft.Identity.Client;

namespace MLSZ
{
    public class UserDto
    {
        /*
        [Name]
        [Email]
        [Phone]
        [Org]
        [Position]
        [Role]
        [Password]
        */
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public int Phone { get; set; }
        public string Org { get; set; } = String.Empty;
        public string Position{ get; set; } = String.Empty;
        public string Role { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
    }
}
