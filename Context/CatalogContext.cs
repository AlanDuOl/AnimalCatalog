using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M = AnimalCatalogSqLite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AnimalCatalogSqLite.Context
{
    public class CatalogContext : IdentityDbContext<IdentityUser>
    {
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        {
        }

        public DbSet<M.Animal> Animals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string[] roleNames = { "Admin", "Manager", "User" };

            foreach (string roleName in roleNames)
            {
                builder.Entity<IdentityRole>().HasData(new IdentityRole
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                });
            }

            builder.Entity<M.Animal>().HasData(new M.Animal
            {
                Id = 1,
                Name = "Abc",
                Genus = "Feline",
                Specie = "Catus"
            });
        }
    }
}
