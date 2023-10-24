


using HagiApi.Configuration;
using Microsoft.Extensions.Configuration;

namespace HagiApi
{
    class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var serviceCollection = builder.Services;
            var configuration = builder.Configuration;

            var connectionStringContainer = new ConnectionStringContainer();
            configuration.Bind("ConnectionStringContainer", connectionStringContainer);

            serviceCollection.AddSingleton(connectionStringContainer);
            serviceCollection.AddAutoMapper(typeof(UserMapper));
            



            serviceCollection.AddDbContext<UserContext>();
            serviceCollection.AddTransient<UserRepository>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {

            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}