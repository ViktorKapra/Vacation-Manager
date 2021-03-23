using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Data.Entity.TimeOffs
{
   public class BaseTimeOff: BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual bool IsHalfDay { get; set; }
        public bool IsApproved { get; set; }
        public int RequestorId { get; set; }
        public virtual User Requestor { get; set; }
    }
}
