using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllianceUniversity.ViewModel
{
    public class EnrolledCourse
    {
        public int CourseId { get; set; }
        public String Title { get; set; }
        public bool Enrolled { get; set; }
    }
}