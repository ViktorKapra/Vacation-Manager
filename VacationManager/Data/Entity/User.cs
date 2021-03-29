using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Data.Entity.TimeOffs;

namespace Data.Entity
{
   public class User : BaseEntity
    {

        [MaxLength(100), Required]
        public string UserName { get; set; }
        [MaxLength(100), Required]
        public string Password { get; set; }
        [MaxLength(100), Required]
        public string FirstName { get; set; }

        [MaxLength(100), Required]
        public string LastName { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Team> LedTeams { get; set; }

        public virtual Team Team { get; set; }
        public int? TeamId { get; set; }
        public virtual ICollection<PaidTimeOff> PaidTimeOffRequests { get; set; }

        public virtual ICollection<UnpaidTimeOff> UnpaidTimeOffRequests { get; set; }

        public virtual ICollection<SickTimeOff> SickTimeOffRequests { get; set; }
    }
}
