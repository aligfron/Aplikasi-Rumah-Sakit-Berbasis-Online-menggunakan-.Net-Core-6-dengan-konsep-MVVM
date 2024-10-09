using HealthCare340B.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.ViewModel
{
    public class VMMSpecialization
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
        public string? fullname { get; set; }
        public long IdUser { get; set; }

        public VMMSpecialization()
        {
        }
        public VMMSpecialization(MSpecialization specialization)
        {
            Id = specialization.Id;
            Name = specialization.Name;
            CreatedBy = specialization.CreatedBy;
            CreatedOn = specialization.CreatedOn;
            ModifiedBy = specialization.ModifiedBy;
            ModifiedOn = specialization.ModifiedOn;
            DeletedBy = specialization.DeletedBy;
            DeletedOn = specialization.DeletedOn;
            IsDelete = specialization.IsDelete;
        }
        public VMMSpecialization(MSpecialization specialization,MUser user, MBiodatum biodata)
        {
            Id = specialization.Id;
            Name = specialization.Name;
            CreatedBy = specialization.CreatedBy;
            CreatedOn = specialization.CreatedOn;
            ModifiedBy = specialization.ModifiedBy;
            ModifiedOn = specialization.ModifiedOn;
            DeletedBy = specialization.DeletedBy;
            DeletedOn = specialization.DeletedOn;
            IsDelete = specialization.IsDelete;
            fullname = biodata.Fullname;
            IdUser = user.Id;
        }
    }
    
    }
