using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Sale_Management
{
    public class NoClientFoundException:BusinessException
    {
        public NoClientFoundException():base(Sale_ManagementDomainErrorCodes.clientOrClientsNotFound)
        {
            
        }
    }
}
