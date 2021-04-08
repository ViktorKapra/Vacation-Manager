using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity.TimeOffs;

namespace VacationManager.Models.ViewModel.TimeOffs
{
    public class AllRequestsVM
    {
        public List<PaidTimeOff> PaidTimeOffs { get; set; }

        public List<UnpaidTimeOff> UnpaidTimeOffs { get; set; }

        public List<SickTimeOff> SickTimeOffs { get; set; }
    }
}
