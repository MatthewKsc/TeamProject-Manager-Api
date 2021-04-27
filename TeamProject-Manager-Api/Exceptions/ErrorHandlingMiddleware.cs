using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.Exceptions
{
    public class ErrorHandlingMiddleware : IMiddleware {
       public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
            
       }
    }
}
