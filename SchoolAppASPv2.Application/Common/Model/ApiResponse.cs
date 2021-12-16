using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Application.Common.Model
{
    public class ApiResponse<T>
    {
        public ApiResponse(string message = "")
        {
            Message = message;
        }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
    