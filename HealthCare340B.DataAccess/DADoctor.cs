using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.DataAccess
{
    public class DADoctor
    {
        private readonly HealthCare340BContext db;

        public DADoctor(HealthCare340BContext _db)
        {
            db = _db;
        }

        //public VMResponse<List<VMMDoctor>?> GetByName(string? filter)
        //{

        //}
    }
}
