

using Microsoft.EntityFrameworkCore;

namespace AccountOwnerServer.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
            });
        }
       

public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config["mysqlconnection:connectionString"];

        services.AddDbContext<API.Model.UserDbContext>(o => o.UseMySql(connectionString,
            MySqlServerVersion.LatestSupportedServerVersion));
    }
        
}
}