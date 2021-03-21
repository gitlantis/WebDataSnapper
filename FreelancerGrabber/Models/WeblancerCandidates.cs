using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FreelancerGrabber.Models
{
    public class WeblancerCandidates
    {
        [Key]
        public int Url_Id { get; set; }
        public string Cont_Content { get; set; }
        public string Phone { get; set; }
        public string E_mail { get; set; }
        public string Login { get; set; }        
    }
}
