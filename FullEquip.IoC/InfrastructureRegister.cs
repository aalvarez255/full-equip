using Microsoft.Extensions.DependencyInjection;
using FullEquip.Infrastructure.DataAccess;
using FullEquip.Infrastructure.DataAccess.Repositories;
using FullEquip.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using FullEquip.Core.Interfaces.Repositories.ReadRepositories;
using FullEquip.Infrastructure.DataAccess.Repositories.ReadRepositories;
using FullEquip.Core.Interfaces.Repositories.WriteRepositories;
using FullEquip.Infrastructure.DataAccess.Repositories.WriteRepositories;

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
            services.AddScoped<ICourseReadRepository, CourseReadRepository>();
            services.AddScoped<ICourseWriteRepository, CourseWriteRepository>();
            services.AddScoped<ICourseStudentWriteRepository, CourseStudentWriteRepository>();
        }           
    }
}
