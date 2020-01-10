using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreExamples.OneToOne
{
    public class Worker
    {
        public int WorkerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }
    }
}