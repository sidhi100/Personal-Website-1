using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final4.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Details { get; set; }
        public string Duration { get; set; }
        public string Skills { get; set; }
        public string link { get; set; }
        public string projectImage { get; set; }
    }

}
