using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCoreExamples.ManyToMany;
using EntityFrameworkCoreExamples.OneToMany;
using EntityFrameworkCoreExamples.OneToOne;

namespace EntityFrameworkCoreExamples.UsageExamples
{
    public class InsertExample
    {
        public static void Run()
        {
            oneToOneExample();
            oneToManyExample();
            manyToManyExample();
        }

        private static void oneToOneExample()
        {
            using (var context = new WorkerContext())
            {
                Worker worker = new Worker() { FirstName = "Caner" };
                context.Worker.Add(worker);
                context.SaveChanges();

                ContactInfo contactInfo = new ContactInfo() { City = "Istanbul", Phone = "555", WorkerId = worker.WorkerId };
                context.ContactInfo.Add(contactInfo);
                context.SaveChanges();
            }
        }

        private static void oneToManyExample()
        {
            using (var context = new CustomerContext())
            {
                Customer customer = new Customer() { Name = "Mehmet" };
                context.Customer.Add(customer);
                context.SaveChanges();

                Order order = new Order() { CustomerId = customer.CustomerId, OrderName = "item1" };
                Order order2 = new Order() { CustomerId = customer.CustomerId, OrderName = "item2" };
                context.Order.Add(order);
                context.Order.Add(order2);

                context.SaveChanges();
            }
        }

        private static void manyToManyExample()
        {
            Student student = new Student() {FirstName = "Caner"};
            Teacher teacher = new Teacher() {Name = "Mehmet", Deparment = "Math"};

            using (var context = new StudentTeacherContext())
            {
                var studentTeacher = new StudentTeacher
                {
                        Student = student, 
                        Teacher = teacher
                };

                context.StudentTeacher.Add(studentTeacher);

                context.SaveChanges();
            }
        }
    }
}
