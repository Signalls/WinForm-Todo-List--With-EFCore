using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMe.Model;
using System.Data.SqlClient;
using System.Configuration;
using Task = TaskMe.Model.Task;

namespace TaskMe
{
     class tubmDBContext : DbContext
    {
     public DbSet<Status> Statuses { get; set; }
     public DbSet<Task> Tasks { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            
            optionsBuilder.UseSqlServer("data source =DECAGON\\MSSQLSERVER02; initial catalog =TaskMaster; Integrated security = True; TrustServerCertificate=True"); 
        }

        

    }
}
