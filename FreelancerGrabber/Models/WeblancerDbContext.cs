using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreelancerGrabber.Models
{
    class WeblancerDbContext : DbContext
    {

        private const string connectionString = @"Data Source=localhost,1433;Initial Catalog=WeblancerDB;User ID=sa;Password=myPass123;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<DevtoCandidates> DevtoCandidates { get; set; }

        public DbSet<WeblancerCandidates> WeblancerCandidates { get; set; }
    }
}
