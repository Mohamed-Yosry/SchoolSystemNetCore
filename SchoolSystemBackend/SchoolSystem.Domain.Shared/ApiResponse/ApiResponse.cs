using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Domain.Shared.ApiResponse
{
    public class ApiResponse<TEntity> where TEntity : class
    {
        public string Message { get; set; }
        public TEntity Data { get; set; }
        public int StatusCode { get; set; }
        public string Exception { get; set; }
    }
}
