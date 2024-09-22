using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class MUser
    {
        public MUser()
        {
            InverseCreatedByNavigation = new HashSet<MUser>();
            InverseDeletedByNavigation = new HashSet<MUser>();
            InverseModifiedByNavigation = new HashSet<MUser>();
            MAdminCreatedByNavigations = new HashSet<MAdmin>();
            MAdminDeletedByNavigations = new HashSet<MAdmin>();
            MAdminModifiedByNavigations = new HashSet<MAdmin>();
            MBankCreatedByNavigations = new HashSet<MBank>();
            MBankDeletedByNavigations = new HashSet<MBank>();
            MBankModifiedByNavigations = new HashSet<MBank>();
            MBiodataAddressCreatedByNavigations = new HashSet<MBiodataAddress>();
            MBiodataAddressDeletedByNavigations = new HashSet<MBiodataAddress>();
            MBiodataAddressModifiedByNavigations = new HashSet<MBiodataAddress>();
            MBiodataAttachmentCreatedByNavigations = new HashSet<MBiodataAttachment>();
            MBiodataAttachmentDeletedByNavigations = new HashSet<MBiodataAttachment>();
            MBiodataAttachmentModifiedByNavigations = new HashSet<MBiodataAttachment>();
            MBiodatumCreatedByNavigations = new HashSet<MBiodatum>();
            MBiodatumDeletedByNavigations = new HashSet<MBiodatum>();
            MBiodatumModifiedByNavigations = new HashSet<MBiodatum>();
            MBloodGroupCreatedByNavigations = new HashSet<MBloodGroup>();
            MBloodGroupDeletedByNavigations = new HashSet<MBloodGroup>();
            MBloodGroupModifiedByNavigations = new HashSet<MBloodGroup>();
            MCourierCreatedByNavigations = new HashSet<MCourier>();
            MCourierDeletedByNavigations = new HashSet<MCourier>();
            MCourierModifiedByNavigations = new HashSet<MCourier>();
            MCourierTypeCreatedByNavigations = new HashSet<MCourierType>();
            MCourierTypeDeletedByNavigations = new HashSet<MCourierType>();
            MCourierTypeModifiedByNavigations = new HashSet<MCourierType>();
            MCustomerCreatedByNavigations = new HashSet<MCustomer>();
            MCustomerDeletedByNavigations = new HashSet<MCustomer>();
            MCustomerMemberCreatedByNavigations = new HashSet<MCustomerMember>();
            MCustomerMemberDeletedByNavigations = new HashSet<MCustomerMember>();
            MCustomerMemberModifiedByNavigations = new HashSet<MCustomerMember>();
            MCustomerModifiedByNavigations = new HashSet<MCustomer>();
            MCustomerRelationCreatedByNavigations = new HashSet<MCustomerRelation>();
            MCustomerRelationDeletedByNavigations = new HashSet<MCustomerRelation>();
            MCustomerRelationModifiedByNavigations = new HashSet<MCustomerRelation>();
            MDoctorCreatedByNavigations = new HashSet<MDoctor>();
            MDoctorDeletedByNavigations = new HashSet<MDoctor>();
            MDoctorEducationCreatedByNavigations = new HashSet<MDoctorEducation>();
            MDoctorEducationDeletedByNavigations = new HashSet<MDoctorEducation>();
            MDoctorEducationModifiedByNavigations = new HashSet<MDoctorEducation>();
            MDoctorModifiedByNavigations = new HashSet<MDoctor>();
            MEducationLevelCreatedByNavigations = new HashSet<MEducationLevel>();
            MEducationLevelDeletedByNavigations = new HashSet<MEducationLevel>();
            MEducationLevelModifiedByNavigations = new HashSet<MEducationLevel>();
            MLocationCreatedByNavigations = new HashSet<MLocation>();
            MLocationDeletedByNavigations = new HashSet<MLocation>();
            MLocationLevelCreatedByNavigations = new HashSet<MLocationLevel>();
            MLocationLevelDeletedByNavigations = new HashSet<MLocationLevel>();
            MLocationLevelModifiedByNavigations = new HashSet<MLocationLevel>();
            MLocationModifiedByNavigations = new HashSet<MLocation>();
            MMedicalFacilityCategoryCreatedByNavigations = new HashSet<MMedicalFacilityCategory>();
            MMedicalFacilityCategoryDeletedByNavigations = new HashSet<MMedicalFacilityCategory>();
            MMedicalFacilityCategoryModifiedByNavigations = new HashSet<MMedicalFacilityCategory>();
            MMedicalFacilityCreatedByNavigations = new HashSet<MMedicalFacility>();
            MMedicalFacilityDeletedByNavigations = new HashSet<MMedicalFacility>();
            MMedicalFacilityModifiedByNavigations = new HashSet<MMedicalFacility>();
            MMedicalFacilityScheduleCreatedByNavigations = new HashSet<MMedicalFacilitySchedule>();
            MMedicalFacilityScheduleDeletedByNavigations = new HashSet<MMedicalFacilitySchedule>();
            MMedicalFacilityScheduleModifiedByNavigations = new HashSet<MMedicalFacilitySchedule>();
            MMedicalItemCategoryCreatedByNavigations = new HashSet<MMedicalItemCategory>();
            MMedicalItemCategoryDeletedByNavigations = new HashSet<MMedicalItemCategory>();
            MMedicalItemCategoryModifiedByNavigations = new HashSet<MMedicalItemCategory>();
            MMedicalItemCreatedByNavigations = new HashSet<MMedicalItem>();
            MMedicalItemDeletedByNavigations = new HashSet<MMedicalItem>();
            MMedicalItemModifiedByNavigations = new HashSet<MMedicalItem>();
            MMedicalItemSegmentationCreatedByNavigations = new HashSet<MMedicalItemSegmentation>();
            MMedicalItemSegmentationDeletedByNavigations = new HashSet<MMedicalItemSegmentation>();
            MMedicalItemSegmentationModifiedByNavigations = new HashSet<MMedicalItemSegmentation>();
            MMenuCreatedByNavigations = new HashSet<MMenu>();
            MMenuDeletedByNavigations = new HashSet<MMenu>();
            MMenuModifiedByNavigations = new HashSet<MMenu>();
            MMenuRoleCreatedByNavigations = new HashSet<MMenuRole>();
            MMenuRoleDeletedByNavigations = new HashSet<MMenuRole>();
            MMenuRoleModifiedByNavigations = new HashSet<MMenuRole>();
            MPaymentMethodCreatedByNavigations = new HashSet<MPaymentMethod>();
            MPaymentMethodDeletedByNavigations = new HashSet<MPaymentMethod>();
            MPaymentMethodModifiedByNavigations = new HashSet<MPaymentMethod>();
            MRoleCreatedByNavigations = new HashSet<MRole>();
            MRoleDeletedByNavigations = new HashSet<MRole>();
            MRoleModifiedByNavigations = new HashSet<MRole>();
            MServiceUnitCreatedByNavigations = new HashSet<MServiceUnit>();
            MServiceUnitDeletedByNavigations = new HashSet<MServiceUnit>();
            MServiceUnitModifiedByNavigations = new HashSet<MServiceUnit>();
            MSpecializationCreatedByNavigations = new HashSet<MSpecialization>();
            MSpecializationDeletedByNavigations = new HashSet<MSpecialization>();
            MSpecializationModifiedByNavigations = new HashSet<MSpecialization>();
            MWalletDefaultNominalCreatedByNavigations = new HashSet<MWalletDefaultNominal>();
            MWalletDefaultNominalDeletedByNavigations = new HashSet<MWalletDefaultNominal>();
            MWalletDefaultNominalModifiedByNavigations = new HashSet<MWalletDefaultNominal>();
            TAppointmentCancellationCreatedByNavigations = new HashSet<TAppointmentCancellation>();
            TAppointmentCancellationDeletedByNavigations = new HashSet<TAppointmentCancellation>();
            TAppointmentCancellationModifiedByNavigations = new HashSet<TAppointmentCancellation>();
            TAppointmentCreatedByNavigations = new HashSet<TAppointment>();
            TAppointmentDeletedByNavigations = new HashSet<TAppointment>();
            TAppointmentDoneCreatedByNavigations = new HashSet<TAppointmentDone>();
            TAppointmentDoneDeletedByNavigations = new HashSet<TAppointmentDone>();
            TAppointmentDoneModifiedByNavigations = new HashSet<TAppointmentDone>();
            TAppointmentModifiedByNavigations = new HashSet<TAppointment>();
            TAppointmentRescheduleHistoryCreatedByNavigations = new HashSet<TAppointmentRescheduleHistory>();
            TAppointmentRescheduleHistoryDeletedByNavigations = new HashSet<TAppointmentRescheduleHistory>();
            TAppointmentRescheduleHistoryModifiedByNavigations = new HashSet<TAppointmentRescheduleHistory>();
            TCourierDiscountCreatedByNavigations = new HashSet<TCourierDiscount>();
            TCourierDiscountDeletedByNavigations = new HashSet<TCourierDiscount>();
            TCourierDiscountModifiedByNavigations = new HashSet<TCourierDiscount>();
            TCurrentDoctorSpecializationCreatedByNavigations = new HashSet<TCurrentDoctorSpecialization>();
            TCurrentDoctorSpecializationDeletedByNavigations = new HashSet<TCurrentDoctorSpecialization>();
            TCurrentDoctorSpecializationModifiedByNavigations = new HashSet<TCurrentDoctorSpecialization>();
            TCustomerChatCreatedByNavigations = new HashSet<TCustomerChat>();
            TCustomerChatDeletedByNavigations = new HashSet<TCustomerChat>();
            TCustomerChatHistoryCreatedByNavigations = new HashSet<TCustomerChatHistory>();
            TCustomerChatHistoryDeletedByNavigations = new HashSet<TCustomerChatHistory>();
            TCustomerChatHistoryModifiedByNavigations = new HashSet<TCustomerChatHistory>();
            TCustomerChatModifiedByNavigations = new HashSet<TCustomerChat>();
            TCustomerCustomNominalCreatedByNavigations = new HashSet<TCustomerCustomNominal>();
            TCustomerCustomNominalDeletedByNavigations = new HashSet<TCustomerCustomNominal>();
            TCustomerCustomNominalModifiedByNavigations = new HashSet<TCustomerCustomNominal>();
            TCustomerRegisteredCardCreatedByNavigations = new HashSet<TCustomerRegisteredCard>();
            TCustomerRegisteredCardDeletedByNavigations = new HashSet<TCustomerRegisteredCard>();
            TCustomerRegisteredCardModifiedByNavigations = new HashSet<TCustomerRegisteredCard>();
            TCustomerVaCreatedByNavigations = new HashSet<TCustomerVa>();
            TCustomerVaDeletedByNavigations = new HashSet<TCustomerVa>();
            TCustomerVaHistoryCreatedByNavigations = new HashSet<TCustomerVaHistory>();
            TCustomerVaHistoryDeletedByNavigations = new HashSet<TCustomerVaHistory>();
            TCustomerVaHistoryModifiedByNavigations = new HashSet<TCustomerVaHistory>();
            TCustomerVaModifiedByNavigations = new HashSet<TCustomerVa>();
            TCustomerWalletCreatedByNavigations = new HashSet<TCustomerWallet>();
            TCustomerWalletDeletedByNavigations = new HashSet<TCustomerWallet>();
            TCustomerWalletModifiedByNavigations = new HashSet<TCustomerWallet>();
            TCustomerWalletTopUpCreatedByNavigations = new HashSet<TCustomerWalletTopUp>();
            TCustomerWalletTopUpDeletedByNavigations = new HashSet<TCustomerWalletTopUp>();
            TCustomerWalletTopUpModifiedByNavigations = new HashSet<TCustomerWalletTopUp>();
            TCustomerWalletWithdrawCreatedByNavigations = new HashSet<TCustomerWalletWithdraw>();
            TCustomerWalletWithdrawDeletedByNavigations = new HashSet<TCustomerWalletWithdraw>();
            TCustomerWalletWithdrawModifiedByNavigations = new HashSet<TCustomerWalletWithdraw>();
            TDoctorOfficeCreatedByNavigations = new HashSet<TDoctorOffice>();
            TDoctorOfficeDeletedByNavigations = new HashSet<TDoctorOffice>();
            TDoctorOfficeModifiedByNavigations = new HashSet<TDoctorOffice>();
            TDoctorOfficeScheduleCreatedByNavigations = new HashSet<TDoctorOfficeSchedule>();
            TDoctorOfficeScheduleDeletedByNavigations = new HashSet<TDoctorOfficeSchedule>();
            TDoctorOfficeScheduleModifiedByNavigations = new HashSet<TDoctorOfficeSchedule>();
            TDoctorOfficeTreatmentCreatedByNavigations = new HashSet<TDoctorOfficeTreatment>();
            TDoctorOfficeTreatmentDeletedByNavigations = new HashSet<TDoctorOfficeTreatment>();
            TDoctorOfficeTreatmentModifiedByNavigations = new HashSet<TDoctorOfficeTreatment>();
            TDoctorOfficeTreatmentPriceCreatedByNavigations = new HashSet<TDoctorOfficeTreatmentPrice>();
            TDoctorOfficeTreatmentPriceDeletedByNavigations = new HashSet<TDoctorOfficeTreatmentPrice>();
            TDoctorOfficeTreatmentPriceModifiedByNavigations = new HashSet<TDoctorOfficeTreatmentPrice>();
            TDoctorTreatmentCreatedByNavigations = new HashSet<TDoctorTreatment>();
            TDoctorTreatmentDeletedByNavigations = new HashSet<TDoctorTreatment>();
            TDoctorTreatmentModifiedByNavigations = new HashSet<TDoctorTreatment>();
            TMedicalItemPurchaseCreatedByNavigations = new HashSet<TMedicalItemPurchase>();
            TMedicalItemPurchaseDeletedByNavigations = new HashSet<TMedicalItemPurchase>();
            TMedicalItemPurchaseDetailCreatedByNavigations = new HashSet<TMedicalItemPurchaseDetail>();
            TMedicalItemPurchaseDetailDeletedByNavigations = new HashSet<TMedicalItemPurchaseDetail>();
            TMedicalItemPurchaseDetailModifiedByNavigations = new HashSet<TMedicalItemPurchaseDetail>();
            TMedicalItemPurchaseModifiedByNavigations = new HashSet<TMedicalItemPurchase>();
            TPrescriptionCreatedByNavigations = new HashSet<TPrescription>();
            TPrescriptionDeletedByNavigations = new HashSet<TPrescription>();
            TPrescriptionModifiedByNavigations = new HashSet<TPrescription>();
            TResetPasswordCreatedByNavigations = new HashSet<TResetPassword>();
            TResetPasswordDeletedByNavigations = new HashSet<TResetPassword>();
            TResetPasswordModifiedByNavigations = new HashSet<TResetPassword>();
            TTokenCreatedByNavigations = new HashSet<TToken>();
            TTokenDeletedByNavigations = new HashSet<TToken>();
            TTokenModifiedByNavigations = new HashSet<TToken>();
            TTokenUsers = new HashSet<TToken>();
            TTreatmentDiscountCreatedByNavigations = new HashSet<TTreatmentDiscount>();
            TTreatmentDiscountDeletedByNavigations = new HashSet<TTreatmentDiscount>();
            TTreatmentDiscountModifiedByNavigations = new HashSet<TTreatmentDiscount>();
        }

        public long Id { get; set; }
        public long? BiodataId { get; set; }
        public long? RoleId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? LoginAttempt { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime? LastLogin { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public virtual MBiodatum? Biodata { get; set; }
        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
        public virtual MRole? Role { get; set; }
        public virtual ICollection<MUser> InverseCreatedByNavigation { get; set; }
        public virtual ICollection<MUser> InverseDeletedByNavigation { get; set; }
        public virtual ICollection<MUser> InverseModifiedByNavigation { get; set; }
        public virtual ICollection<MAdmin> MAdminCreatedByNavigations { get; set; }
        public virtual ICollection<MAdmin> MAdminDeletedByNavigations { get; set; }
        public virtual ICollection<MAdmin> MAdminModifiedByNavigations { get; set; }
        public virtual ICollection<MBank> MBankCreatedByNavigations { get; set; }
        public virtual ICollection<MBank> MBankDeletedByNavigations { get; set; }
        public virtual ICollection<MBank> MBankModifiedByNavigations { get; set; }
        public virtual ICollection<MBiodataAddress> MBiodataAddressCreatedByNavigations { get; set; }
        public virtual ICollection<MBiodataAddress> MBiodataAddressDeletedByNavigations { get; set; }
        public virtual ICollection<MBiodataAddress> MBiodataAddressModifiedByNavigations { get; set; }
        public virtual ICollection<MBiodataAttachment> MBiodataAttachmentCreatedByNavigations { get; set; }
        public virtual ICollection<MBiodataAttachment> MBiodataAttachmentDeletedByNavigations { get; set; }
        public virtual ICollection<MBiodataAttachment> MBiodataAttachmentModifiedByNavigations { get; set; }
        public virtual ICollection<MBiodatum> MBiodatumCreatedByNavigations { get; set; }
        public virtual ICollection<MBiodatum> MBiodatumDeletedByNavigations { get; set; }
        public virtual ICollection<MBiodatum> MBiodatumModifiedByNavigations { get; set; }
        public virtual ICollection<MBloodGroup> MBloodGroupCreatedByNavigations { get; set; }
        public virtual ICollection<MBloodGroup> MBloodGroupDeletedByNavigations { get; set; }
        public virtual ICollection<MBloodGroup> MBloodGroupModifiedByNavigations { get; set; }
        public virtual ICollection<MCourier> MCourierCreatedByNavigations { get; set; }
        public virtual ICollection<MCourier> MCourierDeletedByNavigations { get; set; }
        public virtual ICollection<MCourier> MCourierModifiedByNavigations { get; set; }
        public virtual ICollection<MCourierType> MCourierTypeCreatedByNavigations { get; set; }
        public virtual ICollection<MCourierType> MCourierTypeDeletedByNavigations { get; set; }
        public virtual ICollection<MCourierType> MCourierTypeModifiedByNavigations { get; set; }
        public virtual ICollection<MCustomer> MCustomerCreatedByNavigations { get; set; }
        public virtual ICollection<MCustomer> MCustomerDeletedByNavigations { get; set; }
        public virtual ICollection<MCustomerMember> MCustomerMemberCreatedByNavigations { get; set; }
        public virtual ICollection<MCustomerMember> MCustomerMemberDeletedByNavigations { get; set; }
        public virtual ICollection<MCustomerMember> MCustomerMemberModifiedByNavigations { get; set; }
        public virtual ICollection<MCustomer> MCustomerModifiedByNavigations { get; set; }
        public virtual ICollection<MCustomerRelation> MCustomerRelationCreatedByNavigations { get; set; }
        public virtual ICollection<MCustomerRelation> MCustomerRelationDeletedByNavigations { get; set; }
        public virtual ICollection<MCustomerRelation> MCustomerRelationModifiedByNavigations { get; set; }
        public virtual ICollection<MDoctor> MDoctorCreatedByNavigations { get; set; }
        public virtual ICollection<MDoctor> MDoctorDeletedByNavigations { get; set; }
        public virtual ICollection<MDoctorEducation> MDoctorEducationCreatedByNavigations { get; set; }
        public virtual ICollection<MDoctorEducation> MDoctorEducationDeletedByNavigations { get; set; }
        public virtual ICollection<MDoctorEducation> MDoctorEducationModifiedByNavigations { get; set; }
        public virtual ICollection<MDoctor> MDoctorModifiedByNavigations { get; set; }
        public virtual ICollection<MEducationLevel> MEducationLevelCreatedByNavigations { get; set; }
        public virtual ICollection<MEducationLevel> MEducationLevelDeletedByNavigations { get; set; }
        public virtual ICollection<MEducationLevel> MEducationLevelModifiedByNavigations { get; set; }
        public virtual ICollection<MLocation> MLocationCreatedByNavigations { get; set; }
        public virtual ICollection<MLocation> MLocationDeletedByNavigations { get; set; }
        public virtual ICollection<MLocationLevel> MLocationLevelCreatedByNavigations { get; set; }
        public virtual ICollection<MLocationLevel> MLocationLevelDeletedByNavigations { get; set; }
        public virtual ICollection<MLocationLevel> MLocationLevelModifiedByNavigations { get; set; }
        public virtual ICollection<MLocation> MLocationModifiedByNavigations { get; set; }
        public virtual ICollection<MMedicalFacilityCategory> MMedicalFacilityCategoryCreatedByNavigations { get; set; }
        public virtual ICollection<MMedicalFacilityCategory> MMedicalFacilityCategoryDeletedByNavigations { get; set; }
        public virtual ICollection<MMedicalFacilityCategory> MMedicalFacilityCategoryModifiedByNavigations { get; set; }
        public virtual ICollection<MMedicalFacility> MMedicalFacilityCreatedByNavigations { get; set; }
        public virtual ICollection<MMedicalFacility> MMedicalFacilityDeletedByNavigations { get; set; }
        public virtual ICollection<MMedicalFacility> MMedicalFacilityModifiedByNavigations { get; set; }
        public virtual ICollection<MMedicalFacilitySchedule> MMedicalFacilityScheduleCreatedByNavigations { get; set; }
        public virtual ICollection<MMedicalFacilitySchedule> MMedicalFacilityScheduleDeletedByNavigations { get; set; }
        public virtual ICollection<MMedicalFacilitySchedule> MMedicalFacilityScheduleModifiedByNavigations { get; set; }
        public virtual ICollection<MMedicalItemCategory> MMedicalItemCategoryCreatedByNavigations { get; set; }
        public virtual ICollection<MMedicalItemCategory> MMedicalItemCategoryDeletedByNavigations { get; set; }
        public virtual ICollection<MMedicalItemCategory> MMedicalItemCategoryModifiedByNavigations { get; set; }
        public virtual ICollection<MMedicalItem> MMedicalItemCreatedByNavigations { get; set; }
        public virtual ICollection<MMedicalItem> MMedicalItemDeletedByNavigations { get; set; }
        public virtual ICollection<MMedicalItem> MMedicalItemModifiedByNavigations { get; set; }
        public virtual ICollection<MMedicalItemSegmentation> MMedicalItemSegmentationCreatedByNavigations { get; set; }
        public virtual ICollection<MMedicalItemSegmentation> MMedicalItemSegmentationDeletedByNavigations { get; set; }
        public virtual ICollection<MMedicalItemSegmentation> MMedicalItemSegmentationModifiedByNavigations { get; set; }
        public virtual ICollection<MMenu> MMenuCreatedByNavigations { get; set; }
        public virtual ICollection<MMenu> MMenuDeletedByNavigations { get; set; }
        public virtual ICollection<MMenu> MMenuModifiedByNavigations { get; set; }
        public virtual ICollection<MMenuRole> MMenuRoleCreatedByNavigations { get; set; }
        public virtual ICollection<MMenuRole> MMenuRoleDeletedByNavigations { get; set; }
        public virtual ICollection<MMenuRole> MMenuRoleModifiedByNavigations { get; set; }
        public virtual ICollection<MPaymentMethod> MPaymentMethodCreatedByNavigations { get; set; }
        public virtual ICollection<MPaymentMethod> MPaymentMethodDeletedByNavigations { get; set; }
        public virtual ICollection<MPaymentMethod> MPaymentMethodModifiedByNavigations { get; set; }
        public virtual ICollection<MRole> MRoleCreatedByNavigations { get; set; }
        public virtual ICollection<MRole> MRoleDeletedByNavigations { get; set; }
        public virtual ICollection<MRole> MRoleModifiedByNavigations { get; set; }
        public virtual ICollection<MServiceUnit> MServiceUnitCreatedByNavigations { get; set; }
        public virtual ICollection<MServiceUnit> MServiceUnitDeletedByNavigations { get; set; }
        public virtual ICollection<MServiceUnit> MServiceUnitModifiedByNavigations { get; set; }
        public virtual ICollection<MSpecialization> MSpecializationCreatedByNavigations { get; set; }
        public virtual ICollection<MSpecialization> MSpecializationDeletedByNavigations { get; set; }
        public virtual ICollection<MSpecialization> MSpecializationModifiedByNavigations { get; set; }
        public virtual ICollection<MWalletDefaultNominal> MWalletDefaultNominalCreatedByNavigations { get; set; }
        public virtual ICollection<MWalletDefaultNominal> MWalletDefaultNominalDeletedByNavigations { get; set; }
        public virtual ICollection<MWalletDefaultNominal> MWalletDefaultNominalModifiedByNavigations { get; set; }
        public virtual ICollection<TAppointmentCancellation> TAppointmentCancellationCreatedByNavigations { get; set; }
        public virtual ICollection<TAppointmentCancellation> TAppointmentCancellationDeletedByNavigations { get; set; }
        public virtual ICollection<TAppointmentCancellation> TAppointmentCancellationModifiedByNavigations { get; set; }
        public virtual ICollection<TAppointment> TAppointmentCreatedByNavigations { get; set; }
        public virtual ICollection<TAppointment> TAppointmentDeletedByNavigations { get; set; }
        public virtual ICollection<TAppointmentDone> TAppointmentDoneCreatedByNavigations { get; set; }
        public virtual ICollection<TAppointmentDone> TAppointmentDoneDeletedByNavigations { get; set; }
        public virtual ICollection<TAppointmentDone> TAppointmentDoneModifiedByNavigations { get; set; }
        public virtual ICollection<TAppointment> TAppointmentModifiedByNavigations { get; set; }
        public virtual ICollection<TAppointmentRescheduleHistory> TAppointmentRescheduleHistoryCreatedByNavigations { get; set; }
        public virtual ICollection<TAppointmentRescheduleHistory> TAppointmentRescheduleHistoryDeletedByNavigations { get; set; }
        public virtual ICollection<TAppointmentRescheduleHistory> TAppointmentRescheduleHistoryModifiedByNavigations { get; set; }
        public virtual ICollection<TCourierDiscount> TCourierDiscountCreatedByNavigations { get; set; }
        public virtual ICollection<TCourierDiscount> TCourierDiscountDeletedByNavigations { get; set; }
        public virtual ICollection<TCourierDiscount> TCourierDiscountModifiedByNavigations { get; set; }
        public virtual ICollection<TCurrentDoctorSpecialization> TCurrentDoctorSpecializationCreatedByNavigations { get; set; }
        public virtual ICollection<TCurrentDoctorSpecialization> TCurrentDoctorSpecializationDeletedByNavigations { get; set; }
        public virtual ICollection<TCurrentDoctorSpecialization> TCurrentDoctorSpecializationModifiedByNavigations { get; set; }
        public virtual ICollection<TCustomerChat> TCustomerChatCreatedByNavigations { get; set; }
        public virtual ICollection<TCustomerChat> TCustomerChatDeletedByNavigations { get; set; }
        public virtual ICollection<TCustomerChatHistory> TCustomerChatHistoryCreatedByNavigations { get; set; }
        public virtual ICollection<TCustomerChatHistory> TCustomerChatHistoryDeletedByNavigations { get; set; }
        public virtual ICollection<TCustomerChatHistory> TCustomerChatHistoryModifiedByNavigations { get; set; }
        public virtual ICollection<TCustomerChat> TCustomerChatModifiedByNavigations { get; set; }
        public virtual ICollection<TCustomerCustomNominal> TCustomerCustomNominalCreatedByNavigations { get; set; }
        public virtual ICollection<TCustomerCustomNominal> TCustomerCustomNominalDeletedByNavigations { get; set; }
        public virtual ICollection<TCustomerCustomNominal> TCustomerCustomNominalModifiedByNavigations { get; set; }
        public virtual ICollection<TCustomerRegisteredCard> TCustomerRegisteredCardCreatedByNavigations { get; set; }
        public virtual ICollection<TCustomerRegisteredCard> TCustomerRegisteredCardDeletedByNavigations { get; set; }
        public virtual ICollection<TCustomerRegisteredCard> TCustomerRegisteredCardModifiedByNavigations { get; set; }
        public virtual ICollection<TCustomerVa> TCustomerVaCreatedByNavigations { get; set; }
        public virtual ICollection<TCustomerVa> TCustomerVaDeletedByNavigations { get; set; }
        public virtual ICollection<TCustomerVaHistory> TCustomerVaHistoryCreatedByNavigations { get; set; }
        public virtual ICollection<TCustomerVaHistory> TCustomerVaHistoryDeletedByNavigations { get; set; }
        public virtual ICollection<TCustomerVaHistory> TCustomerVaHistoryModifiedByNavigations { get; set; }
        public virtual ICollection<TCustomerVa> TCustomerVaModifiedByNavigations { get; set; }
        public virtual ICollection<TCustomerWallet> TCustomerWalletCreatedByNavigations { get; set; }
        public virtual ICollection<TCustomerWallet> TCustomerWalletDeletedByNavigations { get; set; }
        public virtual ICollection<TCustomerWallet> TCustomerWalletModifiedByNavigations { get; set; }
        public virtual ICollection<TCustomerWalletTopUp> TCustomerWalletTopUpCreatedByNavigations { get; set; }
        public virtual ICollection<TCustomerWalletTopUp> TCustomerWalletTopUpDeletedByNavigations { get; set; }
        public virtual ICollection<TCustomerWalletTopUp> TCustomerWalletTopUpModifiedByNavigations { get; set; }
        public virtual ICollection<TCustomerWalletWithdraw> TCustomerWalletWithdrawCreatedByNavigations { get; set; }
        public virtual ICollection<TCustomerWalletWithdraw> TCustomerWalletWithdrawDeletedByNavigations { get; set; }
        public virtual ICollection<TCustomerWalletWithdraw> TCustomerWalletWithdrawModifiedByNavigations { get; set; }
        public virtual ICollection<TDoctorOffice> TDoctorOfficeCreatedByNavigations { get; set; }
        public virtual ICollection<TDoctorOffice> TDoctorOfficeDeletedByNavigations { get; set; }
        public virtual ICollection<TDoctorOffice> TDoctorOfficeModifiedByNavigations { get; set; }
        public virtual ICollection<TDoctorOfficeSchedule> TDoctorOfficeScheduleCreatedByNavigations { get; set; }
        public virtual ICollection<TDoctorOfficeSchedule> TDoctorOfficeScheduleDeletedByNavigations { get; set; }
        public virtual ICollection<TDoctorOfficeSchedule> TDoctorOfficeScheduleModifiedByNavigations { get; set; }
        public virtual ICollection<TDoctorOfficeTreatment> TDoctorOfficeTreatmentCreatedByNavigations { get; set; }
        public virtual ICollection<TDoctorOfficeTreatment> TDoctorOfficeTreatmentDeletedByNavigations { get; set; }
        public virtual ICollection<TDoctorOfficeTreatment> TDoctorOfficeTreatmentModifiedByNavigations { get; set; }
        public virtual ICollection<TDoctorOfficeTreatmentPrice> TDoctorOfficeTreatmentPriceCreatedByNavigations { get; set; }
        public virtual ICollection<TDoctorOfficeTreatmentPrice> TDoctorOfficeTreatmentPriceDeletedByNavigations { get; set; }
        public virtual ICollection<TDoctorOfficeTreatmentPrice> TDoctorOfficeTreatmentPriceModifiedByNavigations { get; set; }
        public virtual ICollection<TDoctorTreatment> TDoctorTreatmentCreatedByNavigations { get; set; }
        public virtual ICollection<TDoctorTreatment> TDoctorTreatmentDeletedByNavigations { get; set; }
        public virtual ICollection<TDoctorTreatment> TDoctorTreatmentModifiedByNavigations { get; set; }
        public virtual ICollection<TMedicalItemPurchase> TMedicalItemPurchaseCreatedByNavigations { get; set; }
        public virtual ICollection<TMedicalItemPurchase> TMedicalItemPurchaseDeletedByNavigations { get; set; }
        public virtual ICollection<TMedicalItemPurchaseDetail> TMedicalItemPurchaseDetailCreatedByNavigations { get; set; }
        public virtual ICollection<TMedicalItemPurchaseDetail> TMedicalItemPurchaseDetailDeletedByNavigations { get; set; }
        public virtual ICollection<TMedicalItemPurchaseDetail> TMedicalItemPurchaseDetailModifiedByNavigations { get; set; }
        public virtual ICollection<TMedicalItemPurchase> TMedicalItemPurchaseModifiedByNavigations { get; set; }
        public virtual ICollection<TPrescription> TPrescriptionCreatedByNavigations { get; set; }
        public virtual ICollection<TPrescription> TPrescriptionDeletedByNavigations { get; set; }
        public virtual ICollection<TPrescription> TPrescriptionModifiedByNavigations { get; set; }
        public virtual ICollection<TResetPassword> TResetPasswordCreatedByNavigations { get; set; }
        public virtual ICollection<TResetPassword> TResetPasswordDeletedByNavigations { get; set; }
        public virtual ICollection<TResetPassword> TResetPasswordModifiedByNavigations { get; set; }
        public virtual ICollection<TToken> TTokenCreatedByNavigations { get; set; }
        public virtual ICollection<TToken> TTokenDeletedByNavigations { get; set; }
        public virtual ICollection<TToken> TTokenModifiedByNavigations { get; set; }
        public virtual ICollection<TToken> TTokenUsers { get; set; }
        public virtual ICollection<TTreatmentDiscount> TTreatmentDiscountCreatedByNavigations { get; set; }
        public virtual ICollection<TTreatmentDiscount> TTreatmentDiscountDeletedByNavigations { get; set; }
        public virtual ICollection<TTreatmentDiscount> TTreatmentDiscountModifiedByNavigations { get; set; }
    }
}
