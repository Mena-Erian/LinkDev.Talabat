using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Models.Auth
{
    public class AddressDto
    {
        //public int Id { get; set; }

        // That represent the name of person receiving the order
        // By Default Taken From DisplayName
        // But can be changed to other name when placing the order
        public required string FirstName { get; set; }
        public required string LastName { get; set; }


        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }

        //public required string UserId { get; set; }
    }
}
