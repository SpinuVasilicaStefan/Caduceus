using DiseasesDataFront.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiseasesDataFront.Data;

namespace DiseasesDataFront.Data
{
    public class MvcSimptomContext : DbContext
    {
        public MvcSimptomContext(DbContextOptions<MvcSimptomContext> options) : base(options)
        {
        }

        public DbSet<Simptom> Simptom { get; set; }
    }
}
