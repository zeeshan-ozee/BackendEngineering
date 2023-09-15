
using Microsoft.EntityFrameworkCore;


namespace WebAPI.Models
{
    public class BankContext : DbContext
    {
        public BankContext() { }


        public BankContext(DbContextOptions<BankContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "AuthorDb");
            //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=DBName;Integrated Security=True");
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
