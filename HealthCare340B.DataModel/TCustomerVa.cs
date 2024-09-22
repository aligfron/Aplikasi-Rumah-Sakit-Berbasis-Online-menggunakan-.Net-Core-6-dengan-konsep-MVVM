using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class TCustomerVa
    {
        public TCustomerVa()
        {
            TCustomerVaHistories = new HashSet<TCustomerVaHistory>();
        }

        public long Id { get; set; }
        public long? CustomerId { get; set; }
        public string? VaNumber { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual MCustomer? Customer { get; set; }
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
        public virtual ICollection<TCustomerVaHistory> TCustomerVaHistories { get; set; }
    }
}
