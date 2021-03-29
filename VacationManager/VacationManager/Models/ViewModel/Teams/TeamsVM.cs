using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity;

namespace VacationManager.Models.ViewModel.Teams
{
    public class TeamsVM
    {
        public string Name { get; set; }
        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<User> Developers { get; set; }
        public virtual User TeamLeader { get; set; }
        public int? TeamLeaderId { get; set; }
    }
}
