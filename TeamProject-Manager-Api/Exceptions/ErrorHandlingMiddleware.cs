using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.Exceptions {
    public class ErrorHandlingMiddleware : IMiddleware {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next) {

            try {
                await next.Invoke(context);
            }
            catch(BadRequestException badrequest) {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badrequest.Message);
            }
            catch(NotFoundException notfound) {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notfound.Message);
            }
            catch(Exception exception) {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"Server internal error : {exception.Message}");
            }
        }
    }
}
