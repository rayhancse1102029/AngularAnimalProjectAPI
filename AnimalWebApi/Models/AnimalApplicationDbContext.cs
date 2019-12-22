using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AnimalWebApi.Models
{
    public partial class AnimalApplicationDbContext : DbContext
    {

        public AnimalApplicationDbContext(DbContextOptions<AnimalApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Animal> Animal { get; set; }

    }
}
