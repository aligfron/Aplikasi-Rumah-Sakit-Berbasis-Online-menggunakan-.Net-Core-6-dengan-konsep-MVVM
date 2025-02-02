﻿using HealthCare340B.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.ViewModel
{
    public class VMMDoctorEducation
    {
        public long Id { get; set; }
        public long? DoctorId { get; set; }
        public long? EducationLevelId { get; set; }
        public string? InstitutionName { get; set; }
        public string? Major { get; set; }
        public string? StartYear { get; set; }
        public string? EndYear { get; set; }
        public bool? IsLastEducation { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public VMMDoctorEducation() { }

        public VMMDoctorEducation(MDoctorEducation mDoctorEducation) 
        {
            Id = mDoctorEducation.Id;
            DoctorId = mDoctorEducation.DoctorId;
            EducationLevelId = mDoctorEducation?.EducationLevelId;
            InstitutionName = mDoctorEducation?.InstitutionName;
            Major = mDoctorEducation?.Major;
            StartYear = mDoctorEducation?.StartYear;
            EndYear = mDoctorEducation?.EndYear;
            IsLastEducation = mDoctorEducation?.IsLastEducation;
            CreatedBy = mDoctorEducation.CreatedBy;
            CreatedOn = mDoctorEducation.CreatedOn;
            ModifiedBy = mDoctorEducation.ModifiedBy;
            ModifiedOn = mDoctorEducation.ModifiedOn;
            DeletedBy = mDoctorEducation.DeletedBy;
            DeletedOn = mDoctorEducation.DeletedOn;
            IsDelete = mDoctorEducation.IsDelete;
        }
    }
}
