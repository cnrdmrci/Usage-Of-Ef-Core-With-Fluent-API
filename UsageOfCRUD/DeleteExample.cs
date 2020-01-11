using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFrameworkCoreExamples.ManyToMany;
using EntityFrameworkCoreExamples.OneToMany;
using EntityFrameworkCoreExamples.OneToOne;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreExamples.UsageExamples
{
    public class DeleteExample
    {
        public static void Run()
        {
            oneToOneExample();
            oneToManyExample();
            manyToManyExample();
            removeWithWhereExample();
        }

        private static void oneToOneExample()
        {
            using (var context = new WorkerContext())
            {
                //Delete all dependencies
                Worker worker = new Worker() { WorkerId = 4, FirstName = "Updated Name" };
                context.Worker.Remove(worker);
                context.SaveChanges();
            }
        }

        private static void oneToManyExample()
        {
            using (var context = new CustomerContext())
            {
                //Delete all dependencies
                Customer customer = new Customer() { CustomerId = 3, Name = "Updated Mehmet" };
                context.Customer.Remove(customer);
                context.SaveChanges();
            }
        }

        private static void manyToManyExample()
        {
            using (var context = new StudentTeacherContext())
            {
                //Delete Student and StudentTeacher where studentId = 5
                Student student = new Student() { StudentId = 5 };
                context.Student.Remove(student);

                context.SaveChanges();
            }
        }

        private static void removeWithWhereExample()
        {
            using (var context = new CustomerContext())
            {
                //Delete all dependencies
                context.Customer.RemoveRange(context.Customer.Where(x => x.Name == "Mehmet"));
                //context.Database.ExecuteSqlCommand("Delete from Customer where Name = 'Mehmet' "); 
                //context.Customer.FromSqlRaw()
                context.SaveChanges();
            }
        }
    }
}
