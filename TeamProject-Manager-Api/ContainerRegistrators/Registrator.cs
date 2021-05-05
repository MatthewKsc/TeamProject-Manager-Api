using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.ContainerRegistrators
{
    public interface Registrator
    {
        void RegisterService(IServiceCollection services, IConfiguration Configuration);
    }
}
