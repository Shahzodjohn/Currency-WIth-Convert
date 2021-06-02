using Currency.Models;
using Currency.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ValCurs> Valcures { get; set; }
        public DbSet<Valute> Valutes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().ToTable("Users");
        }
    }
}
