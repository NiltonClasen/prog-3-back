using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;

namespace BackEnd.Models
{
    public class BackContext : DbContext
    {
        public BackContext(DbContextOptions<BackContext> options)
            : base(options)
        {
        }

        public DbSet<Obra> Obras { get; set; }
        public DbSet<Instituicao> Instituicao { get; set; }

    }
}
