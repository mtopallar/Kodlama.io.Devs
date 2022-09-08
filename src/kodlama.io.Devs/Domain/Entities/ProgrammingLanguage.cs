using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProgrammingLanguage:Entity
    {
        public string Name { get; set; }
        public virtual ICollection<SubTechnology> SubTechnologies { get; set; }

        public ProgrammingLanguage()
        {
            //migration için ef ihtiyaç duyabiliyor. onun için parametresiz ctor oluşturuyoruz.
        }

        public ProgrammingLanguage(int id,string name) : this()
        {
            Id = id;
            Name = name;
        }
    }
}
