using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entity.TimeOffs
{
   public class SickTimeOff: BaseTimeOff
    {
        public string PathAttachment { get; set; }
        [NotMapped]
        public override bool IsHalfDay { get; set; }
    }
}
