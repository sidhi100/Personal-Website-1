using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Final4.Models;
using Final4.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Final4.Controllers
{

    public class DownloadDocsController : Controller
    {
        private readonly ApplicationDbContext context_;
        private readonly IHostingEnvironment hostingEnvironment_;
        private string webRootPath = null;
        private string filePath = null;
        public DownloadDocsController(IHostingEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            context_ = context;
            hostingEnvironment_ = hostingEnvironment;
            webRootPath = hostingEnvironment_.WebRootPath;
            filePath = Path.Combine(webRootPath, "FileStorage");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Download(int id)
        {
            List<string> files = null;
            string file = "";
            try
            {
                files = Directory.GetFiles(filePath).ToList<string>();
                if (0 <= id && id <= files.Count)
                    file = Path.GetFileName(files[id - 1]);
                else
                    return NotFound();
            }
            catch
            {
                return NotFound();
            }
            var memory = new MemoryStream();
            file = files[id - 1];
            using (var stream = new FileStream(file, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(file), Path.GetFileName(file));
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
      {
        {".cs", "application/C#" },
        {".txt", "text/plain"},
        {".pdf", "application/pdf"},
        {".doc", "application/vnd.ms-word"},
        {".docx", "application/vnd.ms-word"},
        {".xls", "application/vnd.ms-excel"},
        {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
        {".png", "image/png"},
        {".jpg", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".gif", "image/gif"},
        {".csv", "text/csv"}
      };
        }
        
        public IActionResult DownloadDocsIndex()
        {
            return View(context_.filesMetadatas.ToList<FilesMetadata>());
        }

   
    }
}