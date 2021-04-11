using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Data;

namespace Data.Entity
{
    public class Role:BaseEntity
    {
        public virtual ICollection<User> Users {get;set;}

        [Required, MaxLength(30)]
        public virtual string Name { get; set; } 
    }
}
