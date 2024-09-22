using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class TToken
    {
        public long Id { get; set; }
        public string? Email { get; set; }
        public long? UserId { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiredOn { get; set; }
        public bool? IsExpired { get; set; }
        public string? UsedFor { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
        public virtual MUser? User { get; set; }
    }
}
