using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AllianceUniversity.Models
{
    public enum Grade {
           A,B,C,D,E,F
    }


    public class Enrollment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public Grade? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Students Student { get; set; }
    }
}