using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final4.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        public string Role { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public string Duration { get; set; }
        public string Skills { get; set; }
        public string JobImage { get; set; }
    }
        
}
