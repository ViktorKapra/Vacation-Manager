using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entity
{
   public class Team: BaseEntity
    {
        public string Name { get; set; }
        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<User> Developers { get; set; }
        public virtual User TeamLeader { get; set; }
        public int? TeamLeaderId { get; set; }
    }
}
