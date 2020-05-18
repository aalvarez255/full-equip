using Microsoft.Extensions.DependencyInjection;
using FullEquip.Infrastructure.DataAccess;
using FullEquip.Infrastructure.DataAccess.Repositories;
using FullEquip.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FullEquip.IoC
{
    public static class InfrastructureRegister
    {
        public static void RegisterDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseSqlServer(connectionString));
        }

        public static void RegisterRepository(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseStudentRepository, CourseStudentRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
        }           
    }
}
