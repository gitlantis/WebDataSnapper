using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FreelancerGrabber.Models
{
    public class DevtoCandidates
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
    }
}
