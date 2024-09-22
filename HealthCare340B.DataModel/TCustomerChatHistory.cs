﻿using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class TCustomerChatHistory
    {
        public long Id { get; set; }
        public long? CustomerChatId { get; set; }
        public string? ChatContent { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual TCustomerChat? CustomerChat { get; set; }
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
    }
}
