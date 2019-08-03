using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final4.Models;

namespace Final4.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AcademicItem> academicItems { get; set; }
        
        public DbSet<Project> projects { get; set; }

        public DbSet<ResearchPublication> researchPublications { get; set; }

        public DbSet<HonorsAndAwards> honorsAndAwards { get; set; }

        public DbSet<Internship> internships { get; set; }

        public DbSet<Job> jobs { get; set; }

        public DbSet<Volunteer> volunteers { get; set; }

        public DbSet<FilesMetadata> filesMetadatas { get; set; }

        public DbSet<Comment> comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
