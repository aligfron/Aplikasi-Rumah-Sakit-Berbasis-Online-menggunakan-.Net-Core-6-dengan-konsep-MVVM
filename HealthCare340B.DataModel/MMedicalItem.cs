using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class MMedicalItem
    {
        public MMedicalItem()
        {
            TMedicalItemPurchaseDetails = new HashSet<TMedicalItemPurchaseDetail>();
            TPrescriptions = new HashSet<TPrescription>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public long? MedicalItemCategoryId { get; set; }
        public string? Composition { get; set; }
        public long? MedicalItemSegmentationId { get; set; }
        public string? Manufacturer { get; set; }
        public string? Indication { get; set; }
        public string? Dosage { get; set; }
        public string? Directions { get; set; }
        public string? Contraindication { get; set; }
        public string? Caution { get; set; }
        public string? Packaging { get; set; }
        public long? PriceMax { get; set; }
        public long? PriceMin { get; set; }
        public byte[]? Image { get; set; }
        public string? ImagePath { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual MMedicalItemCategory? MedicalItemCategory { get; set; }
        public virtual MMedicalItemSegmentation? MedicalItemSegmentation { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
        public virtual ICollection<TMedicalItemPurchaseDetail> TMedicalItemPurchaseDetails { get; set; }
        public virtual ICollection<TPrescription> TPrescriptions { get; set; }
    }
}
