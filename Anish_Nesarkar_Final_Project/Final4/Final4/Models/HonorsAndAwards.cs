using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final4.Models
{

    public class HonorsAndAwards
    {
        public int HonorsAndAwardsId { get; set; }
        public string AwardName { get; set; }
        public string AwardYear { get; set; }
        public string AwardImage { get; set; }
    }
}
