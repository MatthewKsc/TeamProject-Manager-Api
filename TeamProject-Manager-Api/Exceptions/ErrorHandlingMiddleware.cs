using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.Exceptions {
    public class ErrorHandlingMiddleware : IMiddleware {
        
        private readonly ILogger<ErrorHandlingMiddleware> logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next) {

            try {
                await next.Invoke(context);
            }
            catch(BadRequestException badrequest) {
                logger.LogWarning(badrequest, badrequest.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badrequest.Message);
            }
            catch(NotFoundException notfound) {
                logger.LogWarning(notfound, notfound.Message);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notfound.Message);
            }
            catch(Exception exception) {
                logger.LogError(exception, exception.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"Server internal error");
            }
        }
    }
}
