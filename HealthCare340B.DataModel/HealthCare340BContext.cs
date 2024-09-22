using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HealthCare340B.DataModel
{
    public partial class HealthCare340BContext : DbContext
    {
        public HealthCare340BContext()
        {
        }

        public HealthCare340BContext(DbContextOptions<HealthCare340BContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MAdmin> MAdmins { get; set; } = null!;
        public virtual DbSet<MBank> MBanks { get; set; } = null!;
        public virtual DbSet<MBiodataAddress> MBiodataAddresses { get; set; } = null!;
        public virtual DbSet<MBiodataAttachment> MBiodataAttachments { get; set; } = null!;
        public virtual DbSet<MBiodatum> MBiodata { get; set; } = null!;
        public virtual DbSet<MBloodGroup> MBloodGroups { get; set; } = null!;
        public virtual DbSet<MCourier> MCouriers { get; set; } = null!;
        public virtual DbSet<MCourierType> MCourierTypes { get; set; } = null!;
        public virtual DbSet<MCustomer> MCustomers { get; set; } = null!;
        public virtual DbSet<MCustomerMember> MCustomerMembers { get; set; } = null!;
        public virtual DbSet<MCustomerRelation> MCustomerRelations { get; set; } = null!;
        public virtual DbSet<MDoctor> MDoctors { get; set; } = null!;
        public virtual DbSet<MDoctorEducation> MDoctorEducations { get; set; } = null!;
        public virtual DbSet<MEducationLevel> MEducationLevels { get; set; } = null!;
        public virtual DbSet<MLocation> MLocations { get; set; } = null!;
        public virtual DbSet<MLocationLevel> MLocationLevels { get; set; } = null!;
        public virtual DbSet<MMedicalFacility> MMedicalFacilities { get; set; } = null!;
        public virtual DbSet<MMedicalFacilityCategory> MMedicalFacilityCategories { get; set; } = null!;
        public virtual DbSet<MMedicalFacilitySchedule> MMedicalFacilitySchedules { get; set; } = null!;
        public virtual DbSet<MMedicalItem> MMedicalItems { get; set; } = null!;
        public virtual DbSet<MMedicalItemCategory> MMedicalItemCategories { get; set; } = null!;
        public virtual DbSet<MMedicalItemSegmentation> MMedicalItemSegmentations { get; set; } = null!;
        public virtual DbSet<MMenu> MMenus { get; set; } = null!;
        public virtual DbSet<MMenuRole> MMenuRoles { get; set; } = null!;
        public virtual DbSet<MPaymentMethod> MPaymentMethods { get; set; } = null!;
        public virtual DbSet<MRole> MRoles { get; set; } = null!;
        public virtual DbSet<MServiceUnit> MServiceUnits { get; set; } = null!;
        public virtual DbSet<MSpecialization> MSpecializations { get; set; } = null!;
        public virtual DbSet<MUser> MUsers { get; set; } = null!;
        public virtual DbSet<MWalletDefaultNominal> MWalletDefaultNominals { get; set; } = null!;
        public virtual DbSet<TAppointment> TAppointments { get; set; } = null!;
        public virtual DbSet<TAppointmentCancellation> TAppointmentCancellations { get; set; } = null!;
        public virtual DbSet<TAppointmentDone> TAppointmentDones { get; set; } = null!;
        public virtual DbSet<TAppointmentRescheduleHistory> TAppointmentRescheduleHistories { get; set; } = null!;
        public virtual DbSet<TCourierDiscount> TCourierDiscounts { get; set; } = null!;
        public virtual DbSet<TCurrentDoctorSpecialization> TCurrentDoctorSpecializations { get; set; } = null!;
        public virtual DbSet<TCustomerChat> TCustomerChats { get; set; } = null!;
        public virtual DbSet<TCustomerChatHistory> TCustomerChatHistories { get; set; } = null!;
        public virtual DbSet<TCustomerCustomNominal> TCustomerCustomNominals { get; set; } = null!;
        public virtual DbSet<TCustomerRegisteredCard> TCustomerRegisteredCards { get; set; } = null!;
        public virtual DbSet<TCustomerVa> TCustomerVas { get; set; } = null!;
        public virtual DbSet<TCustomerVaHistory> TCustomerVaHistories { get; set; } = null!;
        public virtual DbSet<TCustomerWallet> TCustomerWallets { get; set; } = null!;
        public virtual DbSet<TCustomerWalletTopUp> TCustomerWalletTopUps { get; set; } = null!;
        public virtual DbSet<TCustomerWalletWithdraw> TCustomerWalletWithdraws { get; set; } = null!;
        public virtual DbSet<TDoctorOffice> TDoctorOffices { get; set; } = null!;
        public virtual DbSet<TDoctorOfficeSchedule> TDoctorOfficeSchedules { get; set; } = null!;
        public virtual DbSet<TDoctorOfficeTreatment> TDoctorOfficeTreatments { get; set; } = null!;
        public virtual DbSet<TDoctorOfficeTreatmentPrice> TDoctorOfficeTreatmentPrices { get; set; } = null!;
        public virtual DbSet<TDoctorTreatment> TDoctorTreatments { get; set; } = null!;
        public virtual DbSet<TMedicalItemPurchase> TMedicalItemPurchases { get; set; } = null!;
        public virtual DbSet<TMedicalItemPurchaseDetail> TMedicalItemPurchaseDetails { get; set; } = null!;
        public virtual DbSet<TPrescription> TPrescriptions { get; set; } = null!;
        public virtual DbSet<TResetPassword> TResetPasswords { get; set; } = null!;
        public virtual DbSet<TToken> TTokens { get; set; } = null!;
        public virtual DbSet<TTreatmentDiscount> TTreatmentDiscounts { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=HealthCare340B;user id=sa;password=P@ssw0rd;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MAdmin>(entity =>
            {
                entity.ToTable("m_admin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BiodataId).HasColumnName("biodata_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.HasOne(d => d.Biodata)
                    .WithMany(p => p.MAdmins)
                    .HasForeignKey(d => d.BiodataId)
                    .HasConstraintName("FK_m_admin_biodata");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MAdminCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_admin_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MAdminDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_admin_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MAdminModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_admin_modified_by");
            });

            modelBuilder.Entity<MBank>(entity =>
            {
                entity.ToTable("m_bank");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.VaCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("va_code");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MBankCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_bank_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MBankDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_bank_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MBankModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_bank_modified_by");
            });

            modelBuilder.Entity<MBiodataAddress>(entity =>
            {
                entity.ToTable("m_biodata_address");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnType("text")
                    .HasColumnName("address");

                entity.Property(e => e.BiodataId).HasColumnName("biodata_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.Label)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("label");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("postal_code");

                entity.Property(e => e.Recipient)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("recipient");

                entity.Property(e => e.RecipientPhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("recipient_phone_number");

                entity.HasOne(d => d.Biodata)
                    .WithMany(p => p.MBiodataAddresses)
                    .HasForeignKey(d => d.BiodataId)
                    .HasConstraintName("FK_m_biodata_address_biodata");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MBiodataAddressCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_biodata_address_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MBiodataAddressDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_biodata_address_deleted_by");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.MBiodataAddresses)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_m_biodata_address_location");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MBiodataAddressModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_biodata_address_modified_by");
            });

            modelBuilder.Entity<MBiodataAttachment>(entity =>
            {
                entity.ToTable("m_biodata_attachment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BiodataId).HasColumnName("biodata_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.File).HasColumnName("file");

                entity.Property(e => e.FileName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("file_name");

                entity.Property(e => e.FilePath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("file_path");

                entity.Property(e => e.FileSize).HasColumnName("file_size");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.HasOne(d => d.Biodata)
                    .WithMany(p => p.MBiodataAttachments)
                    .HasForeignKey(d => d.BiodataId)
                    .HasConstraintName("FK_m_biodata_attachment_biodata");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MBiodataAttachmentCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_biodata_attachment_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MBiodataAttachmentDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_biodata_attachment_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MBiodataAttachmentModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_biodata_attachment_modified_by");
            });

            modelBuilder.Entity<MBiodatum>(entity =>
            {
                entity.ToTable("m_biodata");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("fullname");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("image_path");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("mobile_phone");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MBiodatumCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_biodata_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MBiodatumDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_biodata_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MBiodatumModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_biodata_modified_by");
            });

            modelBuilder.Entity<MBloodGroup>(entity =>
            {
                entity.ToTable("m_blood_group");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.Descrtiption)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descrtiption");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MBloodGroupCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_blood_group_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MBloodGroupDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_blood_group_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MBloodGroupModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_blood_group_modified_by");
            });

            modelBuilder.Entity<MCourier>(entity =>
            {
                entity.ToTable("m_courier");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MCourierCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_courier_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MCourierDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_courier_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MCourierModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_courier_modified_by");
            });

            modelBuilder.Entity<MCourierType>(entity =>
            {
                entity.ToTable("m_courier_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourierId).HasColumnName("courier_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.Courier)
                    .WithMany(p => p.MCourierTypes)
                    .HasForeignKey(d => d.CourierId)
                    .HasConstraintName("FK_m_courier_type_courier");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MCourierTypeCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_courier_type_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MCourierTypeDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_courier_type_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MCourierTypeModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_courier_type_modified_by");
            });

            modelBuilder.Entity<MCustomer>(entity =>
            {
                entity.ToTable("m_customer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BiodataId).HasColumnName("biodata_id");

                entity.Property(e => e.BloodGroupId).HasColumnName("blood_group_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.Height)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("height");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.RhesusType)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("rhesus_type");

                entity.Property(e => e.Weight)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("weight");

                entity.HasOne(d => d.Biodata)
                    .WithMany(p => p.MCustomers)
                    .HasForeignKey(d => d.BiodataId)
                    .HasConstraintName("FK_m_customer_biodata");

                entity.HasOne(d => d.BloodGroup)
                    .WithMany(p => p.MCustomers)
                    .HasForeignKey(d => d.BloodGroupId)
                    .HasConstraintName("FK_m_customer_blood_group");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MCustomerCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_customer_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MCustomerDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_customer_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MCustomerModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_customer_modified_by");
            });

            modelBuilder.Entity<MCustomerMember>(entity =>
            {
                entity.ToTable("m_customer_member");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.CustomerRelationId).HasColumnName("customer_relation_id");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.ParentBiodataId).HasColumnName("parent_biodata_id");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MCustomerMemberCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_customer_member_created_by");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.MCustomerMembers)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_m_customer_member_customer");

                entity.HasOne(d => d.CustomerRelation)
                    .WithMany(p => p.MCustomerMembers)
                    .HasForeignKey(d => d.CustomerRelationId)
                    .HasConstraintName("FK_m_customer_member_relation");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MCustomerMemberDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_customer_member_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MCustomerMemberModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_customer_member_modified_by");

                entity.HasOne(d => d.ParentBiodata)
                    .WithMany(p => p.MCustomerMembers)
                    .HasForeignKey(d => d.ParentBiodataId)
                    .HasConstraintName("FK_m_customer_member_parent_biodata");
            });

            modelBuilder.Entity<MCustomerRelation>(entity =>
            {
                entity.ToTable("m_customer_relation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MCustomerRelationCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_customer_relation_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MCustomerRelationDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_customer_relation_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MCustomerRelationModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_customer_relation_modified_by");
            });

            modelBuilder.Entity<MDoctor>(entity =>
            {
                entity.ToTable("m_doctor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BiodataId).HasColumnName("biodata_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Str)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("str");

                entity.HasOne(d => d.Biodata)
                    .WithMany(p => p.MDoctors)
                    .HasForeignKey(d => d.BiodataId)
                    .HasConstraintName("FK_m_doctor_biodata");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MDoctorCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_doctor_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MDoctorDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_doctor_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MDoctorModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_doctor_modified_by");
            });

            modelBuilder.Entity<MDoctorEducation>(entity =>
            {
                entity.ToTable("m_doctor_education");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.EducationLevelId).HasColumnName("education_level_id");

                entity.Property(e => e.EndYear)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("end_year");

                entity.Property(e => e.InstitutionName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("institution_name");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.IsLastEducation)
                    .HasColumnName("is_last_education")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Major)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("major");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.StartYear)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("start_year");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MDoctorEducationCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_doctor_education_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MDoctorEducationDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_doctor_education_deleted_by");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.MDoctorEducations)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_m_doctor_education_doctor");

                entity.HasOne(d => d.EducationLevel)
                    .WithMany(p => p.MDoctorEducations)
                    .HasForeignKey(d => d.EducationLevelId)
                    .HasConstraintName("FK_m_doctor_education_education_level");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MDoctorEducationModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_doctor_education_modified_by");
            });

            modelBuilder.Entity<MEducationLevel>(entity =>
            {
                entity.ToTable("m_education_level");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MEducationLevelCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_education_level_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MEducationLevelDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_education_level_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MEducationLevelModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_education_level_modified_by");
            });

            modelBuilder.Entity<MLocation>(entity =>
            {
                entity.ToTable("m_location");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.LocationLevelId).HasColumnName("location_level_id");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MLocationCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_location_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MLocationDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_location_deleted_by");

                entity.HasOne(d => d.LocationLevel)
                    .WithMany(p => p.MLocations)
                    .HasForeignKey(d => d.LocationLevelId)
                    .HasConstraintName("FK_m_location_level");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MLocationModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_location_modified_by");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_m_location_parent");
            });

            modelBuilder.Entity<MLocationLevel>(entity =>
            {
                entity.ToTable("m_location_level");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Abbreviation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("abbreviation");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MLocationLevelCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_location_level_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MLocationLevelDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_location_level_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MLocationLevelModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_location_level_modified_by");
            });

            modelBuilder.Entity<MMedicalFacility>(entity =>
            {
                entity.ToTable("m_medical_facility");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fax)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("fax");

                entity.Property(e => e.FullAddress)
                    .HasColumnType("text")
                    .HasColumnName("full_address");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.MedicalFacilityCategoryId).HasColumnName("medical_facility_category_id");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.PhoneCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone_code");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MMedicalFacilityCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_medical_facility_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MMedicalFacilityDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_medical_facility_deleted_by");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.MMedicalFacilities)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_m_medical_facility_location");

                entity.HasOne(d => d.MedicalFacilityCategory)
                    .WithMany(p => p.MMedicalFacilities)
                    .HasForeignKey(d => d.MedicalFacilityCategoryId)
                    .HasConstraintName("FK_m_medical_facility_category");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MMedicalFacilityModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_medical_facility_modified_by");
            });

            modelBuilder.Entity<MMedicalFacilityCategory>(entity =>
            {
                entity.ToTable("m_medical_facility_category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MMedicalFacilityCategoryCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_medical_facility_category_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MMedicalFacilityCategoryDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_medical_facility_category_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MMedicalFacilityCategoryModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_medical_facility_category_modified_by");
            });

            modelBuilder.Entity<MMedicalFacilitySchedule>(entity =>
            {
                entity.ToTable("m_medical_facility_schedule");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.Day)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("day");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.MedicalFacilityId).HasColumnName("medical_facility_id");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.TimeScheduleEnd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("time_schedule_end");

                entity.Property(e => e.TimeScheduleStart)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("time_schedule_start");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MMedicalFacilityScheduleCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_medical_facility_schedule_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MMedicalFacilityScheduleDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_medical_facility_schedule_deleted_by");

                entity.HasOne(d => d.MedicalFacility)
                    .WithMany(p => p.MMedicalFacilitySchedules)
                    .HasForeignKey(d => d.MedicalFacilityId)
                    .HasConstraintName("FK_m_medical_facility_schedule_facility");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MMedicalFacilityScheduleModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_medical_facility_schedule_modified_by");
            });

            modelBuilder.Entity<MMedicalItem>(entity =>
            {
                entity.ToTable("m_medical_item");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Caution)
                    .HasColumnType("text")
                    .HasColumnName("caution");

                entity.Property(e => e.Composition)
                    .HasColumnType("text")
                    .HasColumnName("composition");

                entity.Property(e => e.Contraindication)
                    .HasColumnType("text")
                    .HasColumnName("contraindication");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.Directions)
                    .HasColumnType("text")
                    .HasColumnName("directions");

                entity.Property(e => e.Dosage)
                    .HasColumnType("text")
                    .HasColumnName("dosage");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("image_path");

                entity.Property(e => e.Indication)
                    .HasColumnType("text")
                    .HasColumnName("indication");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("manufacturer");

                entity.Property(e => e.MedicalItemCategoryId).HasColumnName("medical_item_category_id");

                entity.Property(e => e.MedicalItemSegmentationId).HasColumnName("medical_item_segmentation_id");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Packaging)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("packaging");

                entity.Property(e => e.PriceMax).HasColumnName("price_max");

                entity.Property(e => e.PriceMin).HasColumnName("price_min");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MMedicalItemCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_medical_item_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MMedicalItemDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_medical_item_deleted_by");

                entity.HasOne(d => d.MedicalItemCategory)
                    .WithMany(p => p.MMedicalItems)
                    .HasForeignKey(d => d.MedicalItemCategoryId)
                    .HasConstraintName("FK_m_medical_item_category");

                entity.HasOne(d => d.MedicalItemSegmentation)
                    .WithMany(p => p.MMedicalItems)
                    .HasForeignKey(d => d.MedicalItemSegmentationId)
                    .HasConstraintName("FK_m_medical_item_segmentation");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MMedicalItemModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_medical_item_modified_by");
            });

            modelBuilder.Entity<MMedicalItemCategory>(entity =>
            {
                entity.ToTable("m_medical_item_category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MMedicalItemCategoryCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_medical_item_category_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MMedicalItemCategoryDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_medical_item_category_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MMedicalItemCategoryModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_medical_item_category_modified_by");
            });

            modelBuilder.Entity<MMedicalItemSegmentation>(entity =>
            {
                entity.ToTable("m_medical_item_segmentation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MMedicalItemSegmentationCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_medical_item_segmentation_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MMedicalItemSegmentationDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_medical_item_segmentation_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MMedicalItemSegmentationModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_medical_item_segmentation_modified_by");
            });

            modelBuilder.Entity<MMenu>(entity =>
            {
                entity.ToTable("m_menu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BigIcon)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("big_icon");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.SmallIcon)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("small_icon");

                entity.Property(e => e.Url)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("url");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MMenuCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_menu_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MMenuDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_menu_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MMenuModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_menu_modified_by");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_m_menu_parent");
            });

            modelBuilder.Entity<MMenuRole>(entity =>
            {
                entity.ToTable("m_menu_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MMenuRoleCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_menu_role_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MMenuRoleDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_menu_role_deleted_by");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MMenuRoles)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_m_menu_role_menu");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MMenuRoleModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_menu_role_modified_by");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.MMenuRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_m_menu_role_role");
            });

            modelBuilder.Entity<MPaymentMethod>(entity =>
            {
                entity.ToTable("m_payment_method");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MPaymentMethodCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_payment_method_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MPaymentMethodDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_payment_method_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MPaymentMethodModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_payment_method_modified_by");
            });

            modelBuilder.Entity<MRole>(entity =>
            {
                entity.ToTable("m_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MRoleCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_role_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MRoleDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_role_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MRoleModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_role_modified_by");
            });

            modelBuilder.Entity<MServiceUnit>(entity =>
            {
                entity.ToTable("m_service_unit");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MServiceUnitCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_service_unit_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MServiceUnitDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_service_unit_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MServiceUnitModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_service_unit_modified_by");
            });

            modelBuilder.Entity<MSpecialization>(entity =>
            {
                entity.ToTable("m_specialization");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MSpecializationCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_specialization_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MSpecializationDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_specialization_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MSpecializationModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_specialization_modified_by");
            });

            modelBuilder.Entity<MUser>(entity =>
            {
                entity.ToTable("m_user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BiodataId).HasColumnName("biodata_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.IsLocked).HasColumnName("is_locked");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("datetime")
                    .HasColumnName("last_login");

                entity.Property(e => e.LoginAttempt).HasColumnName("login_attempt");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Biodata)
                    .WithMany(p => p.MUsers)
                    .HasForeignKey(d => d.BiodataId)
                    .HasConstraintName("FK_m_user_biodata");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.InverseCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_user_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.InverseDeletedByNavigation)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_user_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.InverseModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_user_modified_by");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.MUsers)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_m_user_role");
            });

            modelBuilder.Entity<MWalletDefaultNominal>(entity =>
            {
                entity.ToTable("m_wallet_default_nominal");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Nominal).HasColumnName("nominal");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MWalletDefaultNominalCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_wallet_default_nominal_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MWalletDefaultNominalDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_m_wallet_default_nominal_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MWalletDefaultNominalModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_m_wallet_default_nominal_modified_by");
            });

            modelBuilder.Entity<TAppointment>(entity =>
            {
                entity.ToTable("t_appointment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppointmentDate)
                    .HasColumnType("date")
                    .HasColumnName("appointment_date");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.DoctorOfficeId).HasColumnName("doctor_office_id");

                entity.Property(e => e.DoctorOfficeScheduleId).HasColumnName("doctor_office_schedule_id");

                entity.Property(e => e.DoctorOfficeTreatmentId).HasColumnName("doctor_office_treatment_id");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TAppointmentCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_appointment_created_by");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TAppointments)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_t_appointment_customer");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TAppointmentDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_appointment_deleted_by");

                entity.HasOne(d => d.DoctorOffice)
                    .WithMany(p => p.TAppointments)
                    .HasForeignKey(d => d.DoctorOfficeId)
                    .HasConstraintName("FK_t_appointment_doctor_office");

                entity.HasOne(d => d.DoctorOfficeSchedule)
                    .WithMany(p => p.TAppointments)
                    .HasForeignKey(d => d.DoctorOfficeScheduleId)
                    .HasConstraintName("FK_t_appointment_doctor_office_schedule");

                entity.HasOne(d => d.DoctorOfficeTreatment)
                    .WithMany(p => p.TAppointments)
                    .HasForeignKey(d => d.DoctorOfficeTreatmentId)
                    .HasConstraintName("FK_t_appointment_doctor_office_treatment");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TAppointmentModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_appointment_modified_by");
            });

            modelBuilder.Entity<TAppointmentCancellation>(entity =>
            {
                entity.ToTable("t_appointment_cancellation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.TAppointmentCancellations)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK_t_appointment_cancellation_appointment");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TAppointmentCancellationCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_appointment_cancellation_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TAppointmentCancellationDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_appointment_cancellation_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TAppointmentCancellationModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_appointment_cancellation_modified_by");
            });

            modelBuilder.Entity<TAppointmentDone>(entity =>
            {
                entity.ToTable("t_appointment_done");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.Diagnosis)
                    .IsUnicode(false)
                    .HasColumnName("diagnosis");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.TAppointmentDones)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK_t_appointment_done_appointment");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TAppointmentDoneCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_appointment_done_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TAppointmentDoneDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_appointment_done_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TAppointmentDoneModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_appointment_done_modified_by");
            });

            modelBuilder.Entity<TAppointmentRescheduleHistory>(entity =>
            {
                entity.ToTable("t_appointment_reschedule_history");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppointmentDate)
                    .HasColumnType("date")
                    .HasColumnName("appointment_date");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.DoctorOfficeScheduleId).HasColumnName("doctor_office_schedule_id");

                entity.Property(e => e.DoctorOfficeTreatmentId).HasColumnName("doctor_office_treatment_id");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.TAppointmentRescheduleHistories)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK_t_appointment_reschedule_history_appointment");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TAppointmentRescheduleHistoryCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_appointment_reschedule_history_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TAppointmentRescheduleHistoryDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_appointment_reschedule_history_deleted_by");

                entity.HasOne(d => d.DoctorOfficeSchedule)
                    .WithMany(p => p.TAppointmentRescheduleHistories)
                    .HasForeignKey(d => d.DoctorOfficeScheduleId)
                    .HasConstraintName("FK_t_appointment_reschedule_history_doctor_schedule");

                entity.HasOne(d => d.DoctorOfficeTreatment)
                    .WithMany(p => p.TAppointmentRescheduleHistories)
                    .HasForeignKey(d => d.DoctorOfficeTreatmentId)
                    .HasConstraintName("FK_t_appointment_reschedule_history_doctor_treatment");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TAppointmentRescheduleHistoryModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_appointment_reschedule_history_modified_by");
            });

            modelBuilder.Entity<TCourierDiscount>(entity =>
            {
                entity.ToTable("t_courier_discount");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourierTypeId).HasColumnName("courier_type_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("value");

                entity.HasOne(d => d.CourierType)
                    .WithMany(p => p.TCourierDiscounts)
                    .HasForeignKey(d => d.CourierTypeId)
                    .HasConstraintName("FK_t_courier_discount_courier_type");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TCourierDiscountCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_courier_discount_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TCourierDiscountDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_courier_discount_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TCourierDiscountModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_courier_discount_modified_by");
            });

            modelBuilder.Entity<TCurrentDoctorSpecialization>(entity =>
            {
                entity.ToTable("t_current_doctor_specialization");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.SpecializationId).HasColumnName("specialization_id");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TCurrentDoctorSpecializationCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_current_doctor_specialization_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TCurrentDoctorSpecializationDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_current_doctor_specialization_deleted_by");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.TCurrentDoctorSpecializations)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_t_current_doctor_specialization_doctor");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TCurrentDoctorSpecializationModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_current_doctor_specialization_modified_by");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.TCurrentDoctorSpecializations)
                    .HasForeignKey(d => d.SpecializationId)
                    .HasConstraintName("FK_t_current_doctor_specialization_specialization");
            });

            modelBuilder.Entity<TCustomerChat>(entity =>
            {
                entity.ToTable("t_customer_chat");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TCustomerChatCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_customer_chat_created_by");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TCustomerChats)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_t_customer_chat_customer");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TCustomerChatDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_customer_chat_deleted_by");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.TCustomerChats)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_t_customer_chat_doctor");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TCustomerChatModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_customer_chat_modified_by");
            });

            modelBuilder.Entity<TCustomerChatHistory>(entity =>
            {
                entity.ToTable("t_customer_chat_history");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChatContent)
                    .HasColumnType("text")
                    .HasColumnName("chat_content");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.CustomerChatId).HasColumnName("customer_chat_id");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TCustomerChatHistoryCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_customer_chat_history_created_by");

                entity.HasOne(d => d.CustomerChat)
                    .WithMany(p => p.TCustomerChatHistories)
                    .HasForeignKey(d => d.CustomerChatId)
                    .HasConstraintName("FK_t_customer_chat_history_chat");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TCustomerChatHistoryDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_customer_chat_history_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TCustomerChatHistoryModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_customer_chat_history_modified_by");
            });

            modelBuilder.Entity<TCustomerCustomNominal>(entity =>
            {
                entity.ToTable("t_customer_custom_nominal");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Nominal).HasColumnName("nominal");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TCustomerCustomNominalCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_customer_custom_nominal_created_by");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TCustomerCustomNominals)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_t_customer_custom_nominal_customer");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TCustomerCustomNominalDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_customer_custom_nominal_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TCustomerCustomNominalModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_customer_custom_nominal_modified_by");
            });

            modelBuilder.Entity<TCustomerRegisteredCard>(entity =>
            {
                entity.ToTable("t_customer_registered_card");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CardNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("card_number");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Cvv)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("cvv");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.ValidityPeriod)
                    .HasColumnType("date")
                    .HasColumnName("validity_period");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TCustomerRegisteredCardCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_customer_registered_card_created_by");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TCustomerRegisteredCards)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_t_customer_registered_card_customer");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TCustomerRegisteredCardDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_customer_registered_card_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TCustomerRegisteredCardModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_customer_registered_card_modified_by");
            });

            modelBuilder.Entity<TCustomerVa>(entity =>
            {
                entity.ToTable("t_customer_va");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.VaNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("va_number");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TCustomerVaCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_customer_va_created_by");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TCustomerVas)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_t_customer_va_customer");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TCustomerVaDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_customer_va_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TCustomerVaModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_customer_va_modified_by");
            });

            modelBuilder.Entity<TCustomerVaHistory>(entity =>
            {
                entity.ToTable("t_customer_va_history");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.CustomerVaId).HasColumnName("customer_va_id");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.ExpiredOn)
                    .HasColumnType("datetime")
                    .HasColumnName("expired_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TCustomerVaHistoryCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_customer_va_history_created_by");

                entity.HasOne(d => d.CustomerVa)
                    .WithMany(p => p.TCustomerVaHistories)
                    .HasForeignKey(d => d.CustomerVaId)
                    .HasConstraintName("FK_t_customer_va_history_va");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TCustomerVaHistoryDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_customer_va_history_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TCustomerVaHistoryModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_customer_va_history_modified_by");
            });

            modelBuilder.Entity<TCustomerWallet>(entity =>
            {
                entity.ToTable("t_customer_wallet");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("balance");

                entity.Property(e => e.Barcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("barcode");

                entity.Property(e => e.BlockEnds)
                    .HasColumnType("datetime")
                    .HasColumnName("block_ends");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsBlocked).HasColumnName("is_blocked");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Pin)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("pin");

                entity.Property(e => e.PinAttempt)
                    .HasColumnName("pin_attempt")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Points)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("points");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TCustomerWalletCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_customer_wallet_created_by");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TCustomerWallets)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_t_customer_wallet_customer");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TCustomerWalletDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_customer_wallet_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TCustomerWalletModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_customer_wallet_modified_by");
            });

            modelBuilder.Entity<TCustomerWalletTopUp>(entity =>
            {
                entity.ToTable("t_customer_wallet_top_up");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.CustomerWalletId).HasColumnName("customer_wallet_id");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TCustomerWalletTopUpCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_customer_wallet_top_up_created_by");

                entity.HasOne(d => d.CustomerWallet)
                    .WithMany(p => p.TCustomerWalletTopUps)
                    .HasForeignKey(d => d.CustomerWalletId)
                    .HasConstraintName("FK_t_customer_wallet_top_up_wallet");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TCustomerWalletTopUpDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_customer_wallet_top_up_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TCustomerWalletTopUpModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_customer_wallet_top_up_modified_by");
            });

            modelBuilder.Entity<TCustomerWalletWithdraw>(entity =>
            {
                entity.ToTable("t_customer_wallet_withdraw");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("account_name");

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("account_number");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.BankName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("bank_name");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Otp).HasColumnName("otp");

                entity.Property(e => e.WalletDefaultNominalId).HasColumnName("wallet_default_nominal_id");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TCustomerWalletWithdrawCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_customer_wallet_withdraw_created_by");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TCustomerWalletWithdraws)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_t_customer_wallet_withdraw_customer");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TCustomerWalletWithdrawDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_customer_wallet_withdraw_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TCustomerWalletWithdrawModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_customer_wallet_withdraw_modified_by");

                entity.HasOne(d => d.WalletDefaultNominal)
                    .WithMany(p => p.TCustomerWalletWithdraws)
                    .HasForeignKey(d => d.WalletDefaultNominalId)
                    .HasConstraintName("FK_t_customer_wallet_withdraw_wallet_nominal");
            });

            modelBuilder.Entity<TDoctorOffice>(entity =>
            {
                entity.ToTable("t_doctor_office");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.MedicalFacilityId).HasColumnName("medical_facility_id");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.ServiceUnitId).HasColumnName("service_unit_id");

                entity.Property(e => e.Specialization)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("specialization");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TDoctorOfficeCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_doctor_office_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TDoctorOfficeDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_doctor_office_deleted_by");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.TDoctorOffices)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_t_doctor_office_doctor");

                entity.HasOne(d => d.MedicalFacility)
                    .WithMany(p => p.TDoctorOffices)
                    .HasForeignKey(d => d.MedicalFacilityId)
                    .HasConstraintName("FK_t_doctor_office_medical_facility");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TDoctorOfficeModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_doctor_office_modified_by");

                entity.HasOne(d => d.ServiceUnit)
                    .WithMany(p => p.TDoctorOffices)
                    .HasForeignKey(d => d.ServiceUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_doctor_office_service_unit");
            });

            modelBuilder.Entity<TDoctorOfficeSchedule>(entity =>
            {
                entity.ToTable("t_doctor_office_schedule");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.MedicalFacilityScheduleId).HasColumnName("medical_facility_schedule_id");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Slot).HasColumnName("slot");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TDoctorOfficeScheduleCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_doctor_office_schedule_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TDoctorOfficeScheduleDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_doctor_office_schedule_deleted_by");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.TDoctorOfficeSchedules)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_t_doctor_office_schedule_doctor");

                entity.HasOne(d => d.MedicalFacilitySchedule)
                    .WithMany(p => p.TDoctorOfficeSchedules)
                    .HasForeignKey(d => d.MedicalFacilityScheduleId)
                    .HasConstraintName("FK_t_doctor_office_schedule_medical_facility_schedule");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TDoctorOfficeScheduleModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_doctor_office_schedule_modified_by");
            });

            modelBuilder.Entity<TDoctorOfficeTreatment>(entity =>
            {
                entity.ToTable("t_doctor_office_treatment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.DoctorOfficeId).HasColumnName("doctor_office_id");

                entity.Property(e => e.DoctorTreatmentId).HasColumnName("doctor_treatment_id");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TDoctorOfficeTreatmentCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_doctor_office_treatment_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TDoctorOfficeTreatmentDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_doctor_office_treatment_deleted_by");

                entity.HasOne(d => d.DoctorOffice)
                    .WithMany(p => p.TDoctorOfficeTreatments)
                    .HasForeignKey(d => d.DoctorOfficeId)
                    .HasConstraintName("FK_t_doctor_office_treatment_doctor_office");

                entity.HasOne(d => d.DoctorTreatment)
                    .WithMany(p => p.TDoctorOfficeTreatments)
                    .HasForeignKey(d => d.DoctorTreatmentId)
                    .HasConstraintName("FK_t_doctor_office_treatment_treatment");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TDoctorOfficeTreatmentModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_doctor_office_treatment_modified_by");
            });

            modelBuilder.Entity<TDoctorOfficeTreatmentPrice>(entity =>
            {
                entity.ToTable("t_doctor_office_treatment_price");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.DoctorOfficeTreatmentId).HasColumnName("doctor_office_treatment_id");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.PriceStartFrom)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price_start_from");

                entity.Property(e => e.PriceUntilFrom)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price_until_from");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TDoctorOfficeTreatmentPriceCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_doctor_office_treatment_price_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TDoctorOfficeTreatmentPriceDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_doctor_office_treatment_price_deleted_by");

                entity.HasOne(d => d.DoctorOfficeTreatment)
                    .WithMany(p => p.TDoctorOfficeTreatmentPrices)
                    .HasForeignKey(d => d.DoctorOfficeTreatmentId)
                    .HasConstraintName("FK_t_doctor_office_treatment_price_treatment");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TDoctorOfficeTreatmentPriceModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_doctor_office_treatment_price_modified_by");
            });

            modelBuilder.Entity<TDoctorTreatment>(entity =>
            {
                entity.ToTable("t_doctor_treatment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TDoctorTreatmentCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_doctor_treatment_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TDoctorTreatmentDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_doctor_treatment_deleted_by");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.TDoctorTreatments)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_t_doctor_treatment_doctor");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TDoctorTreatmentModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_doctor_treatment_modified_by");
            });

            modelBuilder.Entity<TMedicalItemPurchase>(entity =>
            {
                entity.ToTable("t_medical_item_purchase");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TMedicalItemPurchaseCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_medical_item_purchase_created_by");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TMedicalItemPurchases)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_t_medical_item_purchase_customer");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TMedicalItemPurchaseDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_medical_item_purchase_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TMedicalItemPurchaseModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_medical_item_purchase_modified_by");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.TMedicalItemPurchases)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("FK_t_medical_item_purchase_payment_method");
            });

            modelBuilder.Entity<TMedicalItemPurchaseDetail>(entity =>
            {
                entity.ToTable("t_medical_item_purchase_detail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourierId).HasColumnName("courier_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.MedicalFacilityId).HasColumnName("medical_facility_id");

                entity.Property(e => e.MedicalItemId).HasColumnName("medical_item_id");

                entity.Property(e => e.MedicalItemPurchaseId).HasColumnName("medical_item_purchase_id");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.SubTotal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("sub_total");

                entity.HasOne(d => d.Courier)
                    .WithMany(p => p.TMedicalItemPurchaseDetails)
                    .HasForeignKey(d => d.CourierId)
                    .HasConstraintName("FK_t_medical_item_purchase_detail_courier");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TMedicalItemPurchaseDetailCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_medical_item_purchase_detail_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TMedicalItemPurchaseDetailDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_medical_item_purchase_detail_deleted_by");

                entity.HasOne(d => d.MedicalFacility)
                    .WithMany(p => p.TMedicalItemPurchaseDetails)
                    .HasForeignKey(d => d.MedicalFacilityId)
                    .HasConstraintName("FK_t_medical_item_purchase_detail_facility");

                entity.HasOne(d => d.MedicalItem)
                    .WithMany(p => p.TMedicalItemPurchaseDetails)
                    .HasForeignKey(d => d.MedicalItemId)
                    .HasConstraintName("FK_t_medical_item_purchase_detail_item");

                entity.HasOne(d => d.MedicalItemPurchase)
                    .WithMany(p => p.TMedicalItemPurchaseDetails)
                    .HasForeignKey(d => d.MedicalItemPurchaseId)
                    .HasConstraintName("FK_t_medical_item_purchase_detail_purchase");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TMedicalItemPurchaseDetailModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_medical_item_purchase_detail_modified_by");
            });

            modelBuilder.Entity<TPrescription>(entity =>
            {
                entity.ToTable("t_prescription");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.Directions)
                    .HasColumnType("text")
                    .HasColumnName("directions");

                entity.Property(e => e.Dosage)
                    .HasColumnType("text")
                    .HasColumnName("dosage");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.MedicalItemId).HasColumnName("medical_item_id");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Notes)
                    .HasColumnType("text")
                    .HasColumnName("notes");

                entity.Property(e => e.PrintAttempt).HasColumnName("print_attempt");

                entity.Property(e => e.PrintedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("printed_on");

                entity.Property(e => e.Time)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("time");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.TPrescriptions)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK_t_prescription_appointment");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TPrescriptionCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_prescription_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TPrescriptionDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_prescription_deleted_by");

                entity.HasOne(d => d.MedicalItem)
                    .WithMany(p => p.TPrescriptions)
                    .HasForeignKey(d => d.MedicalItemId)
                    .HasConstraintName("FK_t_prescription_medical_item");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TPrescriptionModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_prescription_modified_by");
            });

            modelBuilder.Entity<TResetPassword>(entity =>
            {
                entity.ToTable("t_reset_password");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.NewPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("new_password");

                entity.Property(e => e.OldPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("old_password");

                entity.Property(e => e.ResetFor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("reset_for");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TResetPasswordCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_reset_password_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TResetPasswordDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_reset_password_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TResetPasswordModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_reset_password_modified_by");
            });

            modelBuilder.Entity<TToken>(entity =>
            {
                entity.ToTable("t_token");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.ExpiredOn)
                    .HasColumnType("datetime")
                    .HasColumnName("expired_on");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.IsExpired).HasColumnName("is_expired");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Token)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.UsedFor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("used_for");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TTokenCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_token_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TTokenDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_token_deleted_by");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TTokenModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_token_modified_by");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TTokenUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_t_token_user");
            });

            modelBuilder.Entity<TTreatmentDiscount>(entity =>
            {
                entity.ToTable("t_treatment_discount");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_on");

                entity.Property(e => e.DoctorOfficeTreatmentPriceId).HasColumnName("doctor_office_treatment_price_id");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("value");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TTreatmentDiscountCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_treatment_discount_created_by");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TTreatmentDiscountDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_t_treatment_discount_deleted_by");

                entity.HasOne(d => d.DoctorOfficeTreatmentPrice)
                    .WithMany(p => p.TTreatmentDiscounts)
                    .HasForeignKey(d => d.DoctorOfficeTreatmentPriceId)
                    .HasConstraintName("FK_t_treatment_discount_treatment_price");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TTreatmentDiscountModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_t_treatment_discount_modified_by");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
