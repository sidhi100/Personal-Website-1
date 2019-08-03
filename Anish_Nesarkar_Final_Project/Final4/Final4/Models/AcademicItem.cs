using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final4.Models
{
    public class AcademicItem
    {
        [Key]
        public int AcademicItemId { get; set; }
        public string AcademicName { get; set; }
        public string CourseWork { get; set; }
        public string Major { get; set; }
        public string College { get; set; }
        public float gpa { get; set; }
        public string yearOfCompletion { get; set; }
        public string AcadImage { get; set; }
    }
        

}
