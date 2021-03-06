﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EntityFrameworkCoreExamples.ManyToMany;
using EntityFrameworkCoreExamples.OneToMany;
using EntityFrameworkCoreExamples.OneToOne;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreExamples.UsageExamples
{
    public class SelectExample
    {
        public static void Run()
        {
            normalSelect();
            oneToOneSelect();
            oneToManySelect();
            manyToManySelect();
            noTrackingSelect();
            rawSqlSelect();
            selectGetDataTable();

            lazyLoadingSelect();
            eagerLoadingSelect();
            explicitLoadingSelect();
        }

        private static void lazyLoadingSelect()
        {
            //lazy loading nesnenin alt nesnelerinin ilk sorguda getirilmemesidir.
            //Daha sonra gereken yerde çağırıldığında, veritabanından sorgulanarak getirilir.
            //Default olarak lazy loading ef core kütüphanesine dahil değildir.
            //Sebebi, bilmeyen kişilerin yanlışlıkla lazy loading kullanmasının istenmemesi.
            //Lazy loading kullanmak için gerekli eklenti: Microsoft.EntityFrameworkCore.Proxies
            using (var context = new WorkerContext())
            {
                List<Worker> worker = context.Worker.ToList(); //Contact info dolmayacak.
                ContactInfo into = worker.First().ContactInfo; //Veritabanına sorgu atılacak.
            }
        }

        private static void eagerLoadingSelect()
        {
            //eager loading nesnenin alt nesneleriyle beraber getirilmesidir.
            //Getirilmesi istenen alt sınıfların include methoduyla dahil edilmesi gerekli.
            using (var context = new WorkerContext())
            {
                context.ChangeTracker.LazyLoadingEnabled = false; //Lazy loading devredışı bırakıyoruz.
                List<Worker> worker = context.Worker.ToList(); //Contact info dolu olacak.
                ContactInfo into = worker.First().ContactInfo; //null gelecek.
                //List<Worker> worker = context.Worker.Include(x => x.ContactInfo).ThenInclude(x=>x...).ToList();
            }
        }

        private static void explicitLoadingSelect()
        {
            //explicit loading nesnenin alt nesenlerinin bilinçli olarak getirilmesidir.
            //Getirilmesi istenen alt nesneyi load methoduyla çağırmamız gerekli.
            //Tekil sınıfın getirilmesinde Reference kullanılır.
            //Çoğul sınıfın getirilmesinde Collection kullanılır.
            using (var context = new WorkerContext())
            {
                context.ChangeTracker.LazyLoadingEnabled = false; //Lazy loading devredışı bırakıyoruz.
                List<Worker> worker = context.Worker.ToList(); //Contact info boş olacak.

                //İlk elemanın contact info değeri boş ise veritabanından sorgulayıp getirecek.
                if (!context.Entry(worker.First()).Reference(x => x.ContactInfo).IsLoaded)
                    context.Entry(worker.First()).Reference(x => x.ContactInfo).Load();

                ContactInfo into = worker.First().ContactInfo; //dolu olacak.
            }
        }

        private static void normalSelect()
        {
            using (var context = new WorkerContext())
            {
                List<Worker> worker = context.Worker.ToList();
            }
        }

        private static void oneToOneSelect()
        {
            using (var context = new WorkerContext())
            {
                List<Worker> worker = context.Worker.Include(x=>x.ContactInfo).ToList();
                List<Worker> worker2 = context.Worker.Include(x=>x.ContactInfo).Where(x=>x.ContactInfo.Phone == "555").ToList();
            }
        }

        private static void oneToManySelect()
        {
            using (var context = new CustomerContext())
            {
                //Currently, in EF core you cannot filter or limit the Included related entities in any way.
                List<Customer> customer = context.Customer.Include(x=>x.Order).ToList();
                List<Customer> customer2 = context.Customer.Where(x => x.Name == "Mehmet").ToList();
                List<Order> order = context.Order.Where(x => x.OrderName == "item1").ToList();

                var customer3 = context.Customer.Select(x=>new
                {
                    Name = x.Name,
                    Order = x.Order.Where(y => y.OrderName == "item1").ToList()
                }).ToList();
            }
        }

        private static void manyToManySelect()
        {
            using (var context = new StudentTeacherContext())
            {
                List<Student> student = context.Student.ToList();
                List<Teacher> teacher = context.Teacher.ToList();

                List<StudentTeacher> studentTeacher = context.StudentTeacher.ToList();
                
                List<StudentTeacher> studentTeacher2 = context.StudentTeacher
                    .Include(x=>x.Student)
                    .Include(x=>x.Teacher)
                    .Where(x=>x.TeacherId == 6).ToList();
                
                List<StudentTeacher> studentTeacher3 = context.StudentTeacher
                    .Include(x=>x.Student)
                    .Include(x=>x.Teacher)
                    .Where(x=>x.Teacher.Deparment == "fizik").ToList();

            }
        }

        private static void noTrackingSelect()
        {
            using (var context = new WorkerContext())
            {
                List<Worker> worker = context.Worker.AsNoTracking().ToList();
                worker[0].FirstName = "new Name";

                //Not update because the object not tracking. The fast way to read information from database.
                context.SaveChanges();
            }
        }

        private static void rawSqlSelect()
        {
            using (var context = new WorkerContext())
            {
                string parameter = "Caner";
                var user = new SqlParameter("user", "johndoe");

                List<Worker> worker = context.Worker.FromSqlRaw("select * from Worker").ToList();
                List<Worker> worker2 = context.Worker.FromSqlRaw("select * from Worker").AsNoTracking().ToList();
                List<Worker> worker3 = context.Worker.FromSqlRaw("select * from Worker where FirstName = {0}",parameter).ToList();
                List<Worker> worker4 = context.Worker.FromSqlRaw("select * from Worker where FirstName = {0}",parameter).OrderBy(x=>x.FirstName).ToList();
                List<Worker> worker5 = context.Worker.FromSqlRaw($"select * from Worker where FirstName = {parameter}").ToList();
                List<Worker> worker6 = context.Worker.FromSqlRaw("EXECUTE dbo.newStoredProsedure").ToList();
                List<Worker> worker7 = context.Worker.FromSqlRaw("EXECUTE dbo.newStoredProsedure @user",user).ToList();

            }
        }

        private static void selectGetDataTable()
        {
            using (var context = new WorkerContext())
            {
                string query = context.Worker.Where(x => x.FirstName == "Caner").ToString();
                query = "select * from worker";

                DataTable dataTable = context.DataTable(query);
                foreach (DataRow dr in dataTable.Rows)
                {
                    Console.Write(dr["FirstName"].ToString());
                }
            }
        }
    }
}
