using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AllianceUniversity.Models
{
    public class Students
    {
        //properties 
        public int Id { get; set; }
        [Required(ErrorMessage="This field is required")]
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}