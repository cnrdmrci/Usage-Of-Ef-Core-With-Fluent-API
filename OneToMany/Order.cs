using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreExamples.OneToMany
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}