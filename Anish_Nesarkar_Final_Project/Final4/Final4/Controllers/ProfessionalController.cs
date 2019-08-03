using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Final4.Models;
using Final4.Data;
using Microsoft.AspNetCore.Authorization;

namespace Final4.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ProfessionalController : Controller
    {
        private readonly ApplicationDbContext context_;

        public ProfessionalController(ApplicationDbContext context)
        {
            context_ = context;
            if (context_.internships.Count() == 0)
            {
                context_.internships.Add(new Internship { Role = "Embedded Software Developer", Company = "MIcrosoft Innovation Lab", Location = "PES University, Bangalore, India", Details = "Built Hand gesture controlled robotic arm", Duration = "3 months", Skills = "Embedded C, Sensor Interfacing, Control Algorithm" , InternImage = "/Images/mlab.png" } );
                context_.SaveChanges();
            }
        }

        [HttpGet]
        public IActionResult ProfessionalIndex()
        {
            return View();
        }

        //>---------------------< Internship methods >--------------------------

        public ActionResult InternView()
        {
            return View(context_.internships.ToList<Internship>());
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddIntern(int id)
        {
            var model = new Internship();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddIntern(int id, Internship intern)
        {
            intern.InternImage = "/Images/" + intern.InternImage;
            context_.internships.Add(intern);
            context_.SaveChanges();
            return RedirectToAction("InternView");
        }

        //Edit Internship

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditIntern(int? id)
        {
            if (id == null)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Internship intern = context_.internships.Find(id);
            if (intern == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(intern);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditIntern(int? id, Internship inter)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var internship = context_.internships.Find(id);
            if (internship != null)
            {
                internship.Role = inter.Role;
                internship.Company = inter.Company;
                internship.Details = inter.Details;
                internship.Duration = inter.Duration;
                internship.Skills = inter.Skills;
                internship.Location = inter.Location;
                internship.InternImage = "/Images/" + inter.InternImage;
                try
                {
                    context_.SaveChanges();
                }
                catch (Exception)
                {
                    // do nothing for now
                }
            }
            return RedirectToAction("InternView");
        }


        //View Intern Details

        public ActionResult InternDetails(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            Internship intern = context_.internships.Find(id);

            if (intern == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(intern);
        }

        //Delete Internship
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteIntern(int? id)
        {
            Internship intern = context_.internships.Find(id);
            context_.Entry(intern).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context_.SaveChanges();
            return RedirectToAction("InternView");
        }


        //>------------------< Job Methods >--------------------<

        // [HttpGet("{id}")]
        [HttpGet]
        public ActionResult JobView()
        {      
            return View(context_.jobs.ToList<Job>());
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddJob(int id)
        {
            var model = new Job();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddJob(int id, Job job)
        {
            job.JobImage = "/Images/" + job.JobImage;
            context_.jobs.Add(job);
            context_.SaveChanges();
            return RedirectToAction("JobView");
        }

        //Edit Job

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditJob(int? id)
        {
            if (id == null)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Job job = context_.jobs.Find(id);
            if (job == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(job);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditJob(int? id, Job job)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var job_ = context_.jobs.Find(id);
            if (job_ != null)
            {
                job_.Role = job.Role;
                job_.Company = job.Company;
                job_.Details = job.Details;
                job_.Duration = job.Duration;
                job_.Skills = job.Skills;
                job_.Location = job.Location;
                job_.JobImage = "/Images/" + job.JobImage;
                try
                {
                    context_.SaveChanges();
                }
                catch (Exception)
                {
                    // do nothing for now
                }
            }
            return RedirectToAction("JobView");
        }


        //View Job Details

        public ActionResult JobDetails(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            Job job = context_.jobs.Find(id);

            if (job == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(job);
        }

        //Delete Job

        public IActionResult DeleteJob(int? id)
        {
            Job job = context_.jobs.Find(id);
            context_.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context_.SaveChanges();
            return RedirectToAction("JobView");
        }

    }
}