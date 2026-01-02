using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositores.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure
{
    public static class DependecyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            AddDbContext_MySqlServer(services);
            AddRepositores(services);
        }
        private static void AddDbContext_MySqlServer(IServiceCollection services)
        {
            var connectionString = "Server=127.0.0.1;Port=3306;Database=meulivrodereceitas;Uid=root;Pwd=;SslMode=None;";

            var serverVersion = new MySqlServerVersion(new Version(8, 2, 12));

            services.AddDbContext<MyRecipeBookDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseMySql(connectionString, serverVersion);
            });
        }

        private static void AddRepositores(IServiceCollection services)
        {
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}
