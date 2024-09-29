using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.ViewModel
{
    public class VMAppointmentSchedule
    {
        public long MedFacScheduleId { get; set; }
        public long DoctorOfficeId { get; set; }
        public long DoctorOfficeScheduleID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Day { get; set; }
        public string? TimeScheduleStart { get; set; }
        public string? TimeScheduleEnd { get; set; }
    }
}
