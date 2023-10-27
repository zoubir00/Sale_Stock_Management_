using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Sale_Management.Entities
{
    public class Client:Entity<Guid>
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        //navigation
        public string PhoneNumber { get; set; }

        public Client(Guid id,string fName,string lName,string email,string phoneNumber):base(id)
        {
            FName = fName;
            LName = lName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
