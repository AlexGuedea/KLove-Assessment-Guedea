using KLove.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLove.Data
{
    public class VersesContext : DbContext
    {

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder
           .Entity<VersesData>(builder =>
           {
               builder.HasNoKey();
               builder.ToTable("VersesData");
           });
        }

        public VersesContext(DbContextOptions<VersesContext> options)
            : base(options)
        {

        }
        public DbSet<VersesData> GetVerses { get; set; }
        public DbSet<FavoriteVerses> GetFavoriteVerses { get; set; }


    }
}
