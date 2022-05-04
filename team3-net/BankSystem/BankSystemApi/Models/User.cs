using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BankSystemApi.Models
{
    public class User : BaseClass
    {
        [Required]
        [NotNull]
        public string Login { get; set; }
        [Required]
        [NotNull]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
}
