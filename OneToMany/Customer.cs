using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreExamples.OneToMany
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public IList<Order> Order { get; set; }
    }
}