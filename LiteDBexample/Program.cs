using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace LiteDBexample
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var db = new LiteDatabase(@"MyBasa.db"))
            {
                var col = db.GetCollection<Company>("companies");

                var microsoft = new Company { Name = "Microsoft" };
                microsoft.Users = new List<User> { new User { Name = "Bill Gates" } };
                col.Insert(microsoft);

                microsoft.Name = "Microsoft Inc.";
                col.Update(microsoft);

                var google = new Company { Name = "Gogle" };
                google.Users = new List<User> { new User { Name = "Larry Page" } };
                col.Insert(google);

                var result = col.FindAll();
                foreach (Company c in result)
                {
                    Console.WriteLine(c.Name);

                    foreach (User u in c.Users)
                    {
                        Console.WriteLine(u.Name);
                    }
                    Console.WriteLine();
                }

                col.EnsureIndex(x => x.Name);

               
                col.DeleteMany(x => x.Name.Equals("Google"));
                //Console.WriteLine("После удаления Google");

                result = col.FindAll();
                foreach(Company c in result)
                {
                    Console.WriteLine(c.Name);
                    foreach (User u in c.Users)
                    {
                        Console.WriteLine(u.Name);
                    }
                    Console.WriteLine();
                }

                Console.ReadKey();

            }
        }
    }
}
