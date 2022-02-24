using Eventures.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Eventures.Models;

namespace Eventures.Data
{
    public class ApplicationDbContext : IdentityDbContext<EventuresUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Eventure> Events { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
