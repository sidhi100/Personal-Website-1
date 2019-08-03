using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Final4.Models;
using Final4.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Final4.Controllers
{
    public class HomeController : Controller
    {
            private readonly ApplicationDbContext context_;
            private readonly IHostingEnvironment hostingEnvironment_;
            private string webRootPath = null;
            private string filePath = null;
            public HomeController(IHostingEnvironment hostingEnvironment, ApplicationDbContext context)
            {
                context_ = context;
            int count = 1;
            
            while(context_.filesMetadatas.Count() != 0)
            {
                var x = context_.filesMetadatas.First();
                if (x == null)
                    break;
                context_.Remove(x);
                context_.SaveChanges();
                count++;
            }
            
                hostingEnvironment_ = hostingEnvironment;
                webRootPath = hostingEnvironment_.WebRootPath;
                filePath = Path.Combine(webRootPath, "FileStorage");

                List<string> filestest = null;
                filestest = Directory.GetFiles(filePath).ToList<string>();
                for (int i = 0; i < filestest.Count; ++i)
                {
                    var model = new FilesMetadata();
                    model.FileName = Path.GetFileName(filestest[i]);
                    model.FileImage = "/Images/fileImage.png";
                context_.filesMetadatas.Add(model);
                    context_.SaveChanges();
                    filestest[i] = Path.GetFileName(filestest[i]);

                }
            }

            public IActionResult Index()
        {
            return View();
        }

        public IActionResult SiteMap()
        {
            return View();
        }

        public IActionResult AcademicIndex()
        {
           return RedirectToAction("AcademicIndex","Academics");
        }

        public IActionResult ProfessionalIndex()
        {
            return RedirectToAction("ProfessionalIndex", "Professional");
        }

        public IActionResult AchievementIndex()
        {
            return RedirectToAction("AchievementIndex", "Achievement");
        }

        public IActionResult DownloadDocsIndex()
        {
            return RedirectToAction("DownloadDocsIndex", "DownloadDocs");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
