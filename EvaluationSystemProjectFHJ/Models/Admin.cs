using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationSystemProjectFHJ.Models
{
    public class Admin
    {

        [Key]
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Username { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 1)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 1)]
        public string Lastname { get; set; }

        public string Middlename { get; set; }

        public string Position { get; set; }

        public string State { get; set; }
    }
}