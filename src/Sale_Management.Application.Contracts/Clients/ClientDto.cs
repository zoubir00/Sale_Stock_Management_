﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Sale_Management.Clients
{
    public class ClientDto:EntityDto<Guid>
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        //navigation
        public string PhoneNumber { get; set; }

    }
}
