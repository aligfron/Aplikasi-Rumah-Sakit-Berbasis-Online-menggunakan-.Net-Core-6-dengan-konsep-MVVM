using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class TDoctorOfficeTreatmentPrice
    {
        public TDoctorOfficeTreatmentPrice()
        {
            TTreatmentDiscounts = new HashSet<TTreatmentDiscount>();
        }

        public long Id { get; set; }
        public long? DoctorOfficeTreatmentId { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceStartFrom { get; set; }
        public decimal? PriceUntilFrom { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual TDoctorOfficeTreatment? DoctorOfficeTreatment { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
        public virtual ICollection<TTreatmentDiscount> TTreatmentDiscounts { get; set; }
    }
}
