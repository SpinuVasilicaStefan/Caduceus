using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DiseasesData
{
    public class Disease
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        [Obsolete]
        public virtual List<Name> Names { get; private set; }

        [Obsolete]
        public List<string> StringNames
        {
            get => Names?.Select(n => n.Value).ToList();
        }

        [Obsolete]
        public string StringName
        {
            get => this.StringNames.First();
        }

        public Disease()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
