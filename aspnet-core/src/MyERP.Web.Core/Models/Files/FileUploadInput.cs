using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.Models.Files
{
    public class FileUploadInput
    {
       public IFormFile[] Files { get; set; }

    }
}
