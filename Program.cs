using System;
using EntityFrameworkCoreExamples.UsageExamples;
using EntityFrameworkCoreExamples.UsageOfCRUD;

namespace EntityFrameworkCoreExamples
{
    /*
     * Migration
     * add-migration <migration name>       //Creates a migration by adding a migration snapshot.
     * add-migration <migration name> -Context <context name>
     * remove-migration     //Removes the last migration snapshot.
     * update-database      //Updates the database schema based on the last migration snapshot.
     * update-database <migration name>
     * update-database 0    //Get first migration
     * update-database -Context <context name>
     * scrip-migration      //Get all of tables initialization script
     * This will also add to the __EFMigrationsHistory table in the database.
     * If you want to remove last migration, you need to revert a migration to previous migration.
     *
     * get-dbcontext  //Get all db context
     *
     * More information for get-help entityframework
     *
     */

    //-------------------------------------------------------------------------------------------------------------

    /*
     * There are two ways to configure domain classes in EF Core (same as in EF 6).
     * 1. By using Data Annotation Attributes
     * 2. By using Fluent API
     */

    //-------------------------------------------------------------------------------------------------------------

    //Diyelim yeni oluşturduğum bir tablo var. Local'de yaptığım değişiklik için Migration oluşturdum ve commitledim.
    //Prod ortamda son değişikliklerin migrate olabileceği bir sayfa hazırlanabilir.
    //Bu değişiklik ilgili context'imizi kapsamakta.

    //if (!context.Database.EnsureCreated())
    //    context.Database.Migrate();

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            InsertExample.Run();
            UpdateExample.Run();
            DeleteExample.Run();
            SelectExample.Run();
            TransactionExample.Run();
        }
    }
}
