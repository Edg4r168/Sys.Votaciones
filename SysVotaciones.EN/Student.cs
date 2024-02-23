using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysVotaciones.EN
{
    public class Student
    {
        public string? StudentCode { get; set; }
        public int CareerYearId {  get; set; }
        public Year? oCareerYear { get; set; }
        public int CareerId { get; set; }
        public Career? oCareer { get; set; }
        public string? Password { get; set; }
    }
}
