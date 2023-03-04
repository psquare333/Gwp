using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Gwp.Data;
using Gwp.Services;

namespace Gwp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<GwpContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("GwpContext") ?? throw new InvalidOperationException("Connection string 'GwpContext' not found.")));


            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<IGwpServices, GwpServices>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.WebHost.ConfigureKestrel(serverOptions => { 
                serverOptions.Limits.MaxConcurrentConnections = 100;
                serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
                serverOptions.Limits.MaxRequestBodySize = 52428800;

            });




            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GwpContext>();
                dbContext.initiateData();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}