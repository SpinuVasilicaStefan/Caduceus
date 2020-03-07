using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DiseasesData
{
    public class DiseasesContext : DbContext
    {
        public DbSet<Disease> Diseases { get; set; }

        public DbSet<Name> Names { get; set; }

        public DbSet<Cause> Causes { get; set; }

        public DiseasesContext()
        {
            Database.EnsureCreated();
        }

        public string GetDiseaseName(Disease disease)
        {
            return this.Names.Where(n => n.DiseaseId == disease.Id).FirstOrDefault().Value;
        }

        public List<List<string>> GetMlData()
        {
            List<List<string>> result = new List<List<string>>();
            int i = 0;
            foreach (Disease disease in this.Diseases)
            {
                List<string> aux = new List<string>();

                var comorbids = this.Causes
                    .Where(c => c.Disease == disease)
                    .Select(c => c.CausedBy)
                    .ToList();

                foreach(Disease comorbid in comorbids)
                    aux.Add(this.GetDiseaseName(comorbid));
                aux.Add(this.GetDiseaseName(disease));

                result.Add(aux);
            }
            return result;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=DiseasesDb;Trusted_Connection=True;MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Disease>()
                .HasMany(m => m.Names)
                .WithOne(m => m.Disease)
                .HasForeignKey(m => m.DiseaseId);
            
            modelBuilder.Entity<Cause>()
                .HasOne(typeof(Disease), "Disease")
                .WithMany()
                .HasForeignKey("DiseaseId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cause>()
                .HasOne(typeof(Disease), "CausedBy")
                .WithMany()
                .HasForeignKey("CausedById")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cause>()
                .HasIndex(p => new { p.DiseaseId, p.CausedById })
                .IsUnique();    
        }
    }
}
