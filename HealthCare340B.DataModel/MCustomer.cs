using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class MCustomer
    {
        public MCustomer()
        {
            MCustomerMembers = new HashSet<MCustomerMember>();
            TAppointments = new HashSet<TAppointment>();
            TCustomerChats = new HashSet<TCustomerChat>();
            TCustomerCustomNominals = new HashSet<TCustomerCustomNominal>();
            TCustomerRegisteredCards = new HashSet<TCustomerRegisteredCard>();
            TCustomerVas = new HashSet<TCustomerVa>();
            TCustomerWalletWithdraws = new HashSet<TCustomerWalletWithdraw>();
            TCustomerWallets = new HashSet<TCustomerWallet>();
            TMedicalItemPurchases = new HashSet<TMedicalItemPurchase>();
        }

        public long Id { get; set; }
        public long? BiodataId { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public long? BloodGroupId { get; set; }
        public string? RhesusType { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public virtual MBiodatum? Biodata { get; set; }
        public virtual MBloodGroup? BloodGroup { get; set; }
        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
        public virtual ICollection<MCustomerMember> MCustomerMembers { get; set; }
        public virtual ICollection<TAppointment> TAppointments { get; set; }
        public virtual ICollection<TCustomerChat> TCustomerChats { get; set; }
        public virtual ICollection<TCustomerCustomNominal> TCustomerCustomNominals { get; set; }
        public virtual ICollection<TCustomerRegisteredCard> TCustomerRegisteredCards { get; set; }
        public virtual ICollection<TCustomerVa> TCustomerVas { get; set; }
        public virtual ICollection<TCustomerWalletWithdraw> TCustomerWalletWithdraws { get; set; }
        public virtual ICollection<TCustomerWallet> TCustomerWallets { get; set; }
        public virtual ICollection<TMedicalItemPurchase> TMedicalItemPurchases { get; set; }
    }
}
