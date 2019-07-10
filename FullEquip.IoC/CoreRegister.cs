using Microsoft.Extensions.DependencyInjection;
using FullEquip.Core.Interfaces.Services;
using FullEquip.Core.Services;

namespace FullEquip.IoC
{
    public static class CoreRegister
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICourseService, CourseService>();        
        }           
    }
}
