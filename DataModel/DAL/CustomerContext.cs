using DataModel.BE;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstCoreDemo.DAL
{
    public class CustomerContext: DbContext
    {
        public DbSet<Customer> Customers {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial catalog=CustomerDB; Integrated security=True", b => b.MigrationsAssembly("DataModel"));
        }
    }

 
}
