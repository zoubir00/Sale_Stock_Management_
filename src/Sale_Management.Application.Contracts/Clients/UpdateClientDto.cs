using System;
using System.Collections.Generic;
using System.Text;

namespace Sale_Management.Clients
{
    public class UpdateClientDto
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        //navigation
        public string PhoneNumber { get; set; }
    }
}
