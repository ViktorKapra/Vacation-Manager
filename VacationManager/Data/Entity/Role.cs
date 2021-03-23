using System;
using System.Collections.Generic;
using System.Text;
using Data;

namespace Data.Entity
{
    public class Role:BaseEntity
    {
        public virtual ICollection<User> Users {get;set;}

        public virtual RoleEnum Name { get; set; } 
    }
}
