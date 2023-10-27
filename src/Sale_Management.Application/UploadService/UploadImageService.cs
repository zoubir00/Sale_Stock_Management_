using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Sale_Management.UploadService
{
    public class UploadImageService:ITransientDependency
    {

        private readonly IHostingEnvironment _host;

        [Obsolete]
        public UploadImageService(IHostingEnvironment host)
        {
            _host = host;
        }

        [Obsolete]
        public  async Task<string> UploadImage(IFormFile img)
        {
            var filePath = Path.Combine(@"C:\\Users\\HP\\OneDrive\\Bureau\\Internship\\AbpTutorial\\GestionVente\\angular\\src\\assets\\images\\articles", img.FileName);
            using(FileStream stream =new FileStream(filePath, FileMode.Create))
            {
               await img.CopyToAsync(stream);
            }
            return  img.FileName;
        }
    }
}
