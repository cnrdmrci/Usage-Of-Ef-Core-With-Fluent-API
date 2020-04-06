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
    public class UpdateExample
    {
        public static void Run()
        {
            changeTrackerDirtyExample();
            changeTrackerNiceExample();

            oneToOneExample();
            oneToManyExample();
            manyToManyExample();
            updateOneField();
        }

        private static void changeTrackerDirtyExample()
        {
            using (var context = new WorkerContext())
            {
                //id'si 4 olan bir degerimiz olsun. Dısarıdanda alınabilir.
                Worker worker = context.Worker.FirstOrDefault(x => x.WorkerId == 4);
                context.Entry(worker).State = EntityState.Detached; // Disaridan gelmis gibi yaptık.
                context.Entry(worker).State = EntityState.Modified;
                //Durumunu modified yaparsak bu durumda tüm kolonlar güncellemeye gidecektir. 
                //Bu da bize fazladan maliyet oluşturacaktır.
                context.SaveChanges();

                ContactInfo contactInfo = new ContactInfo() { ContactInfoId = 11, Phone = "558", WorkerId = worker.WorkerId };
                context.ContactInfo.Update(contactInfo);

                context.SaveChanges();
            }
        }

        private static void changeTrackerNiceExample()
        {
            using (var context = new WorkerContext())
            {
                //id'si 4 olan bir degerimiz olsun. Dısarıdanda alınabilir.
                Worker worker = context.Worker.FirstOrDefault(x => x.WorkerId == 4);
                context.Entry(worker).State = EntityState.Detached; // Disaridan gelmis gibi yaptık.
                context.Worker.Attach(worker); //Durumu : Unchanged
                worker.FirstName = "Yeni isim"; //Durumu : Modified 
                //dikkat edilmesi gereken sadece FirstName kolonu güncellenecek. Diğer kolonlar update sorgusunda olmayacak.
                context.SaveChanges();

                ContactInfo contactInfo = new ContactInfo() { ContactInfoId = 11, Phone = "558", WorkerId = worker.WorkerId };
                context.ContactInfo.Update(contactInfo);

                context.SaveChanges();
            }
        }

        private static void oneToOneExample()
        {
            using (var context = new WorkerContext())
            {
                Worker worker = new Worker() { WorkerId = 4, FirstName = "Updated Name" };
                context.Worker.Update(worker);
                context.SaveChanges();

                ContactInfo contactInfo = new ContactInfo() {ContactInfoId = 11,Phone = "558", WorkerId = worker.WorkerId };
                context.ContactInfo.Update(contactInfo);

                context.SaveChanges();
            }
        }

        private static void oneToManyExample()
        {
            using (var context = new CustomerContext())
            {
                Customer customer = new Customer() {CustomerId = 3,Name = "Updated Mehmet" };
                context.Customer.Update(customer);
                context.SaveChanges();

                Order order = new Order() {OrderId = 3,Customer = customer, OrderName = "Updated item1" };
                Order order2 = new Order() { OrderId = 4, Customer = customer, OrderName = "Updated item2" };
                context.Order.Update(order);
                context.Order.Update(order2);

                context.SaveChanges();
            }
        }

        private static void manyToManyExample()
        {
            using (var context = new StudentTeacherContext())
            {
                Student student = new Student() {StudentId = 5,FirstName = "Updated Caner" };
                context.Student.Update(student);

                Teacher teacher = new Teacher() { TeacherId = 5,Name = "Updated Mehmet", Deparment = "Math" };
                context.Teacher.Update(teacher);

                context.SaveChanges();
            }
        }

        private static void updateOneField()
        {
            using (var context = new WorkerContext())
            {
                Worker worker = new Worker() { WorkerId = 4, LastName = "Updated Last Name" };
                context.Worker.Attach(worker);
                context.Entry(worker).Property(x => x.LastName).IsModified = true;

                context.SaveChanges();
            }
        }
    }
}
