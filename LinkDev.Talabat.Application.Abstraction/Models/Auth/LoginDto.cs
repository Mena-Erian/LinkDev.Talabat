using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Models.Auth
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        //[DataType(DataType.EmailAddress)] // that for display in ui 
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
