using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreExamples.OneToOne
{
    public class ContactInfo
    {
        public int ContactInfoId { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public int WorkerId { get; set; }
        public virtual Worker Worker { get; set; }
    }
}