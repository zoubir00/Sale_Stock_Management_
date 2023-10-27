using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Sale_Management.Clients;
using Sale_Management.Clients.Repository;
using Sale_Management.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Sale_Management.Clients
{
    public class ClientManager : DomainService
    {
        public async Task<Client> CreateClientAsync(string fName, string lName, string email, string phoneNumber)
        {
            if (fName == null && lName == null && email == null && phoneNumber == null)
            {
                throw new Exception("Client fileds must not be empty");
            }
            return new Client
            (
                GuidGenerator.Create(),
                 fName,
                 lName,
                 email,
                 phoneNumber

            );
        }
    }
}
