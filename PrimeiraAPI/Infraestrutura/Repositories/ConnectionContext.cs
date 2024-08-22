using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Infraestrutura.Repositores
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

            => optionsBuilder.UseNpgsql(
                "Server=localhost;" +
                "Port=5432; Database =employee_sample;" +
                "User Id=postgres;" +
                "Password=123456;");


    }
}
