using FavoDeMel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FavoDeMel.Repository.Common
{
    public class FavoDeMelDbContext : DbContext, IFavoDeMelDbContext
    {
        public FavoDeMelDbContext(DbContextOptions<FavoDeMelDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var currentAssembly = typeof(FavoDeMelDbContext).Assembly;
            var efMappingTypes = currentAssembly.GetTypes().Where(t =>
                t.FullName != null && t.FullName.StartsWith("FavoDeMel.Repository.") && t.FullName.EndsWith("Mapping"));

            foreach (var map in efMappingTypes.Select(Activator.CreateInstance))
            {
                builder.ApplyConfiguration((dynamic)map);
            }
        }       
    }
}