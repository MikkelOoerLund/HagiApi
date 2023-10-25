


using HagiApi.Configuration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


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

            serviceCollection.AddControllers()
               .AddNewtonsoftJson(options =>
               {
                   var serializerSettings = options.SerializerSettings;
                   serializerSettings.ContractResolver = new DefaultContractResolver();
                   serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
               });


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