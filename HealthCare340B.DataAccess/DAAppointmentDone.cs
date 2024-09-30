using HealthCare340B.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.DataAccess
{
    public class DAAppointmentDone
    {
        private readonly HealthCare340BContext _db;

        public DAAppointmentDone(HealthCare340BContext db)
        {
            _db = db;
        }


    }
}
