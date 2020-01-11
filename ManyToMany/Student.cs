using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreExamples.ManyToMany
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IList<StudentTeacher> StudentTeacher { get; set; }
    }
}
