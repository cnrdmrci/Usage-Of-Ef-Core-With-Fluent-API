using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreExamples.ManyToMany
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Deparment { get; set; }

        public IList<StudentTeacher> StudentTeacher { get; set; }
    }
}
