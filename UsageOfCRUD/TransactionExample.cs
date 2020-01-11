using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCoreExamples.OneToOne;
using Microsoft.EntityFrameworkCore.Storage;

namespace EntityFrameworkCoreExamples.UsageOfCRUD
{
    public class TransactionExample
    {
        public static void Run()
        {
            transctionExample();
        }

        private static void transctionExample()
        {
            using (var context = new WorkerContext())
            {
                using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Worker.Add(new Worker {FirstName = "Transaction", LastName = "lastName"});
                        context.SaveChanges();
                        
                        // throw exectiopn to test roll back transaction
                        //throw new Exception();

                        context.Worker.Add(new Worker {FirstName = "Transaction", LastName = "lastName2"});
                        context.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception)   
                    {
                        transaction.Rollback();
                        Console.WriteLine("Error occurred.");
                    }

                }
            }
        }
    }
}
