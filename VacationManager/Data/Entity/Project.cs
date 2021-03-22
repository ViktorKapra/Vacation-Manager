using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Data.Entity
{
   public class Project: BaseEntity
    {
        public string Name { get; set; }
        [MaxLengthAttribute]
        public string Description { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
