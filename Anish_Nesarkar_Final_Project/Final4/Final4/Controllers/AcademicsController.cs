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
    
    public class AcademicsController : Controller
    {
        private readonly ApplicationDbContext context_;

        public AcademicsController(ApplicationDbContext context)
        {
            context_ = context;
            if (context_.academicItems.Count() == 0)
            {
                context_.academicItems.Add(new AcademicItem { AcademicName = "Under-Graduate", College = "PES Institute of Technology", CourseWork = "Embedded Systems, Computer Networks, Data Structures, Internet Of Things", Major = "Electronics and Communication", yearOfCompletion = "2018" , gpa = 3.8F , AcadImage = "/Images/pesit.jpg"  }  );
                context_.academicItems.Add(new AcademicItem { AcademicName = "Graduate", College = "Syracuse University", CourseWork = "Design and Analysis of Algorithms, Software Modelling And Analysis, Internet Programming, Operating Systems, Object Oriented Programming", Major = "Computer Science", yearOfCompletion = "2020" , gpa = 3.4F , AcadImage = "/Images/SUImage.jpg" } );
                context_.SaveChanges();
            }
        }

        [HttpGet]
        public IActionResult AcademicIndex()
        {
            return View();
        }


        // [HttpGet("{id}")]
        public ActionResult AcadView(int id)
        {
            AcademicItem academicItem = context_.academicItems.Find(id);
            if (academicItem == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            List<AcademicItem> temp = new List<AcademicItem>();
            temp.Add(academicItem);
            return View(temp);
        }



        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddAcad(int id)
        {
            var model = new AcademicItem();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddAcad(int id, AcademicItem acad)
        {
            acad.AcadImage = "/Images/" + acad.AcadImage;
            context_.academicItems.Add(acad);
            context_.SaveChanges();
            return RedirectToAction("AcademicIndex");
        }

        //Edit Academic

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditAcad(int? id)
        {
            if (id == null)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            AcademicItem acad = context_.academicItems.Find(id);
            if (acad == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(acad);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditAcad(int? id, AcademicItem acad)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var academic = context_.academicItems.Find(id);
            if (academic != null)
            {
                academic.AcademicName = acad.AcademicName;
                academic.College = acad.College;
                academic.CourseWork = acad.CourseWork;
                academic.Major = acad.Major;
                academic.yearOfCompletion = acad.yearOfCompletion;
                academic.gpa = acad.gpa;
                academic.AcadImage = "/Images/" + acad.AcadImage;
                try
                {
                    context_.SaveChanges();
                }
                catch (Exception)
                {
                    // do nothing for now
                }
            }
            return RedirectToAction("AcademicIndex");
        }


        //View Academic Details

        public ActionResult AcadDetails(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            AcademicItem acad = context_.academicItems.Find(id);

            if (acad == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(acad);
        }

        //Delete Academic
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteAcad(int? id)
        {
            AcademicItem acad = context_.academicItems.Find(id);
            context_.Entry(acad).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context_.SaveChanges();
            return RedirectToAction("AcademicIndex");
        }




    }
}