using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BankSystemApi.DTO
{
    public class UserDTO
    {
        [DefaultValue("admin@gmail.com")]
        [NotNull]
        public string Login { get; set; }
        [DefaultValue("1111")]
        [NotNull]
        public string Password { get; set; }
    }
}
