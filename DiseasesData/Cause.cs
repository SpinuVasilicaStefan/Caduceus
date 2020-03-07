using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DiseasesData
{
    public class Cause
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        [Required]
        public Guid DiseaseId { get; private set; }

        [Required]
        public Guid CausedById { get; private set; }

        [Required]
        public uint Count { get; set; }

        [ForeignKey("DiseaseId")]
        public virtual Disease Disease { get; private set; }

        [ForeignKey("CausedById")]
        public virtual Disease CausedBy { get; private set; }

        public Cause(Guid diseaseId, Guid causedById)
        {
            this.Id = Guid.NewGuid();
            this.DiseaseId = diseaseId;
            this.CausedById = causedById;
            this.Count = 1;
        }
       
        public Cause(Guid diseaseId, Disease causedBy)
            : this(diseaseId, causedBy.Id) { }

        public Cause(Disease disease, Guid causedById)
           : this(disease.Id, causedById) { }

        public Cause(Disease disease, Disease causedBy)
            : this(disease.Id, causedBy.Id) { }
    }
}
