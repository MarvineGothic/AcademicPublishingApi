using Microsoft.EntityFrameworkCore;

namespace AcademicPublishingApi.Data
{
    public static class AppDbContextExtensions
    {
        public static void AddAppDbContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(connection));
        }
    }
}
