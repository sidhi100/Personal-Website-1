using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Final4.Models;
using Final4.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Final4.Controllers
{
    public class AchievementController : Controller
    {
        private readonly ApplicationDbContext context_;
        private const string sessionId_ = "SessionId";
        
        public AchievementController(IHostingEnvironment hostingEnvironment,ApplicationDbContext context)
        {
            
            context_ = context;
            if (context_.projects.Count() == 0)
            {
               context_.projects.Add(new Project { ProjectName = "Small Scale IoT-enabled Automated greenhouse", Details  = "Built an automated greenhouse from scratch as a part of final year project", Duration = "5 months", Skills = "Embedded C, Sensor Interfacing, Control Algorithm", projectImage = "/Images/greenhouse.JPG", link = "https://drive.google.com/open?id=1j2jKbtsIIIhQlnWpo-AeTEPMBjoIzFu8" } );
                context_.honorsAndAwards.Add(new HonorsAndAwards { AwardName = "MHRD Merit Scholarship", AwardYear = "2017", AwardImage = "/Images/AwardsImage.png" });
                context_.researchPublications.Add(new ResearchPublication { researchName = "Design and Implementation of Hybrid methodology for IOT Data", Conference = "IEEE 2017 International Conference On Smart Technologies For Smart Nation (SmartTechCon)", researchYear = "2017", link = "https://ieeexplore.ieee.org/document/8358562", researchImage = "/Images/research_image.png" });
                context_.SaveChanges();
            }
        }

        public IActionResult CommentIndex()
        {
            return RedirectToAction("CommentView", "Comments");
        }

        [HttpGet]
        public IActionResult AchievementIndex()
        {
            return View();
        }


        //>--------------< Project Methods >--------------------<

        [HttpGet]
        public ActionResult ProjectView()
        {
            return View(context_.projects.ToList<Project>());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddProject(int id)
        {
            var model = new Project();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddProject(int id, Project proj)
        {
            proj.projectImage = "/Images/" + proj.projectImage;
            context_.projects.Add(proj);
            context_.SaveChanges();
            return RedirectToAction("ProjectView");
        }

        //Edit Projects

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditProject(int? id)
        {
            if (id == null)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Project project = context_.projects.Find(id);
            if (project == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(project);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditProject(int? id, Project proj)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var project = context_.projects.Find(id);
            if (project != null)
            {
                project.ProjectName = proj.ProjectName;
                project.Details = proj.Details;
                project.Duration = proj.Duration;
                project.Skills = proj.Skills;
                project.link = proj.link;
                project.projectImage = "/Images/" + proj.projectImage;
                try
                {
                    context_.SaveChanges();
                }
                catch (Exception)
                {
                    // do nothing for now
                }
            }
            return RedirectToAction("ProjectView");
        }


        //View Project Details

        public ActionResult projectDetails(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            Project project = context_.projects.Find(id);

            if (project == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(project);
        }

        //Deete Project
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteProject(int? id)
        {
            Project project = context_.projects.Find(id);
            context_.Entry(project).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context_.SaveChanges();
            return RedirectToAction("ProjectView");
        }



           //>---------------------< Honors and Awards methods >-------------------<


        public ActionResult HonorView()
        {
         
            return View(context_.honorsAndAwards.ToList<HonorsAndAwards>());
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddHonor(int id)
        {
            var model = new HonorsAndAwards();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddHonor(int id, HonorsAndAwards honor)
        {
            honor.AwardImage = "/Images/" + honor.AwardImage;
            context_.honorsAndAwards.Add(honor);
            context_.SaveChanges();
            return RedirectToAction("HonorView");
        }

        //Edit Honors

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditHonor(int? id)
        {
            if (id == null)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            HonorsAndAwards honor = context_.honorsAndAwards.Find(id);
            if (honor == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(honor);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditHonor(int? id, HonorsAndAwards hon)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var honor = context_.honorsAndAwards.Find(id);
            if (honor != null)
            {
                honor.AwardName = hon.AwardName;
                honor.AwardYear = hon.AwardYear;
                honor.AwardImage = "/Images/" + hon.AwardImage;
                try
                {
                    context_.SaveChanges();
                }
                catch (Exception)
                {
                    // do nothing for now
                }
            }
            return RedirectToAction("HonorView");
        }


        //View Honor Details

        public ActionResult HonorDetails(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            HonorsAndAwards honor = context_.honorsAndAwards.Find(id);

            if (honor == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(honor);
        }

        //Delete Honor
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteHonor(int? id)
        {
            HonorsAndAwards honor = context_.honorsAndAwards.Find(id);
            context_.Entry(honor).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context_.SaveChanges();
            return RedirectToAction("HonorView");
        }



        //>--------------------< Research methods >--------------------<



        public ActionResult ResearchView()
        {
            
            return View(context_.researchPublications.ToList<ResearchPublication>());
        }



        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddResearch(int id)
        {
            var model = new ResearchPublication();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddResearch(int id, ResearchPublication res)
        {
            res.researchImage = "/Images/" + res.researchImage;
            context_.researchPublications.Add(res);
            context_.SaveChanges();
            return RedirectToAction("ResearchView");
        }

        //Edit Research

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditResearch(int? id)
        {
            if (id == null)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            ResearchPublication res = context_.researchPublications.Find(id);
            if (res == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(res);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditResearch(int? id, ResearchPublication res)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var research = context_.researchPublications.Find(id);
            if (research != null)
            {
                research.researchName = res.researchName;
                research.researchYear = res.researchYear;
                research.Conference = res.Conference;
                research.link = res.link;
                research.researchImage = "/Images/" + res.researchImage;
                try
                {
                    context_.SaveChanges();
                }
                catch (Exception)
                {
                    // do nothing for now
                }
            }
            return RedirectToAction("ResearchView");
        }


        //View Project Details

        public ActionResult researchDetails(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            ResearchPublication res = context_.researchPublications.Find(id);

            if (res == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(res);
        }

        //Delete Research
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteResearch(int? id)
        {
            ResearchPublication res = context_.researchPublications.Find(id);
            context_.Entry(res).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context_.SaveChanges();
            return RedirectToAction("ResearchView");
        }


    }
}