using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.ViewModel
{
    public class VMResponse<T>
    {
        public HttpStatusCode statusCode { get; set; }
        public string? message { get; set; }
        public T? data { get; set; }

        public VMResponse()
        {
            statusCode = HttpStatusCode.InternalServerError;
            message = string.Empty; // sama dengan ""
            data = default(T); // empty object
        }
    }
}
