using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class TMedicalItemPurchaseDetail
    {
        public long Id { get; set; }
        public long? MedicalItemPurchaseId { get; set; }
        public long? MedicalItemId { get; set; }
        public int? Qty { get; set; }
        public long? MedicalFacilityId { get; set; }
        public long? CourierId { get; set; }
        public decimal? SubTotal { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public virtual MCourier? Courier { get; set; }
        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual MMedicalFacility? MedicalFacility { get; set; }
        public virtual MMedicalItem? MedicalItem { get; set; }
        public virtual TMedicalItemPurchase? MedicalItemPurchase { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
    }
}
