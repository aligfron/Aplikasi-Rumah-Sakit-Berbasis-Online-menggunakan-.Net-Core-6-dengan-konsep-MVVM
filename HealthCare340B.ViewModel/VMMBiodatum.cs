using HealthCare340B.DataModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.ViewModel
{
    public class VMMBiodatum
    {
        public long Id { get; set; }
        public string? Fullname { get; set; }
        public string? MobilePhone { get; set; }
        public byte[]? Image { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? ImageFile { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
        public VMMBiodatum() { }
        public VMMBiodatum(MBiodatum mBiodatum) {
            Id = mBiodatum.Id;
            Fullname = mBiodatum?.Fullname;
            MobilePhone = mBiodatum?.MobilePhone;
            Image = mBiodatum?.Image;
            ImagePath = mBiodatum?.ImagePath;
            CreatedBy = mBiodatum!.CreatedBy;
            CreatedOn = mBiodatum!.CreatedOn;
            ModifiedBy = mBiodatum!.ModifiedBy;
            ModifiedOn = mBiodatum.ModifiedOn;
            DeletedBy = mBiodatum.DeletedBy;
            DeletedOn = mBiodatum.DeletedOn;
            IsDelete = mBiodatum.IsDelete;


        }
    }
}
