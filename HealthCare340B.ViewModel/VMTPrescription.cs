using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.ViewModel
{
    public class VMTPrescription
    {
        public long Id { get; set; }
        public long? AppointmentId { get; set; }
        public long? MedicalItemId { get; set; }

        public string? MedicalItemName { get; set; }

        public string? Dosage { get; set; }
        public string? Directions { get; set; }
        public string? Time { get; set; }
        public string? Notes { get; set; }
        public DateTime? PrintedOn { get; set; }
        public int? PrintAttempt { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
    }
}
