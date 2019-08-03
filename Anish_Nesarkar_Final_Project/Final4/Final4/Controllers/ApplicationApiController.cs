using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Final4.Data;
using Final4.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Final4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationApiController : ControllerBase
    {
        private readonly ApplicationDbContext context_;
        private readonly IHostingEnvironment hostingEnvironment_;
        private string webRootPath = null;
        private string filePath = null;
        private string fileImagePath = null;
        public ApplicationApiController(IHostingEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            context_ = context;
            hostingEnvironment_ = hostingEnvironment;
            webRootPath = hostingEnvironment_.WebRootPath;
            filePath = Path.Combine(webRootPath, "FileStorage");
            fileImagePath = Path.Combine(webRootPath, "Images");

            //List<string> filestest = null;
            //filestest = Directory.GetFiles(filePath).ToList<string>();
            //for (int i = 0; i < filestest.Count; ++i)
            //{
            //    var model = new FilesMetadata();
            //    model.FileName = filestest[i];
            //    context_.filesMetadatas.Add(model);
            //    context_.SaveChanges();
            //    // AchievementItem achievement = context_.AchievementItems;
            //    filestest[i] = Path.GetFileName(filestest[i]);

            //}
        }

        // GET: api/ApplicationApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilesMetadata>>> GetProfessionalItem()
        {
            return await context_.filesMetadatas.ToListAsync();
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



        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            var request = HttpContext.Request;
            foreach (var file in request.Form.Files)
            {
                if (file.Length > 0)
                {
                    var path = Path.Combine(filePath, file.FileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        var modeladd = new FilesMetadata();
                        modeladd.FileName = file.FileName;
                        modeladd.FileImage = "/Images/fileImage.png";
                        context_.filesMetadatas.Add(modeladd);
                        context_.SaveChanges();
                        await file.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            return Ok();
        }



        // DELETE: api/ApplicationApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FilesMetadata>> DeleteProfessionalItem(int id)
        {
            var professionalItem = await context_.filesMetadatas.FindAsync(id);
            if (professionalItem == null)
            {
                return NotFound();
            }

            context_.filesMetadatas.Remove(professionalItem);
            await context_.SaveChangesAsync();

            return professionalItem;
        }

        private bool ProfessionalItemExists(int id)
        {
            return context_.filesMetadatas.Any(e => e.FilesMetadataId == id);
        }
    }
}
