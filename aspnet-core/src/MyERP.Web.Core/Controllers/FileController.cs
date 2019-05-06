using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyERP.Base;
using MyERP.Models;
using MyERP.Models.Files;
using MyERP.Net.MimeTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Controllers
{
    [Route("api/[controller]/[action]")]
    public class FileController : MyERPControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// 上传设备图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="fileName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [RequestFormLimits(MultipartBodyLengthLimit = 1024 * 1024 * 10)]
        [HttpPost]
        public async Task<FileUploadDto> Upload(FileUploadInput input)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            foreach (var formFile in input.Files)
            {
                if (formFile.Length == 0)
                {
                    continue;
                }
                string fileExt = Path.GetExtension(formFile.FileName); //文件扩展名，不含“.”
                long fileSize = formFile.Length; //获得文件大小，以字节为单位
                var name = Guid.NewGuid().ToString();
                string newName = name + fileExt; //新的文件名
                var fileDire = Path.Combine(webRootPath, "File");
                if (!Directory.Exists(fileDire))
                {
                    Directory.CreateDirectory(fileDire);
                }
                var filePath = Path.Combine(fileDire, newName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                var serverPath = filePath.Substring(webRootPath.Length);

                return new FileUploadDto()
                {
                    Ext = fileExt,
                    Size = fileSize,
                    Id = name,
                    Name = formFile.FileName,
                    Url = serverPath
                };

            }

            return null;
        }



        [HttpPost]
        public async Task<List<FileUploadDto>> UploadMany(FileUploadInput input)
        {
            var list = new List<FileUploadDto>();

            string webRootPath = _hostingEnvironment.WebRootPath;
            foreach (var formFile in input.Files)
            {
                if (formFile.Length == 0)
                {
                    continue;
                }
                string fileExt = Path.GetExtension(formFile.FileName); //文件扩展名，不含“.”
                long fileSize = formFile.Length; //获得文件大小，以字节为单位
                var name = Guid.NewGuid().ToString();
                string newName = name + fileExt; //新的文件名
                var fileDire = Path.Combine(webRootPath, "file");
                if (!Directory.Exists(fileDire))
                {
                    Directory.CreateDirectory(fileDire);
                }
                var filePath = Path.Combine(fileDire, newName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                var serverPath = filePath.Substring(webRootPath.Length);

                list.Add(new FileUploadDto()
                {

                    Ext = fileExt,
                    Size = fileSize,
                    Id = name,
                    Name = formFile.FileName,
                    Url = serverPath
                });

            }

            return list;
        }

        [HttpGet]
        public async Task<ActionResult> Download(FileDownInput input)
        {
            var ext = Path.GetExtension(input.Name);
            var fileName = input.Id + ext;
            // 得到服务端路径
            string webRootPath = _hostingEnvironment.WebRootPath;
            var filePath = Path.Combine(webRootPath, "file", fileName);
            if (System.IO.File.Exists(filePath))
            {
                var by = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(by, MimeTypeNamesHelper.GetMimeType(ext), input.Name);
            }
            return null;
        }
    }
}