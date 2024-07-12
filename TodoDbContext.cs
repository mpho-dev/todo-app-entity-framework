using Microsoft.EntityFrameworkCore;
using TodoEFConsole.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoEFConsole
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext() : base() 
        {
        }
        //OnConfiguring() method is used to select and configure the data source
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Configure the context here
            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var configSection = configBuilder.GetSection("ConnectionStrings");
            var connectionString = configSection["SQLServerConnection"] ?? null;
            optionsBuilder.UseSqlServer(connectionString);
            //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information); //Logging SQL queries for this context
            //base.OnConfiguring(optionsBuilder);
        }

        //OnModelCreating() method is used to configure the model using ModelBuilder Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configure the model here
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<Todo> TodoItems { get; set; }
    }
}
