using DS = NetAzure_DemoApi.Dal.Services;
using NetAzure_DemoApi.Bll.Services;
using System.Data.Common;
using System.Data.SqlClient;
using NetAzure_DemoApi.CQRS.Commands;
using Tools.CQRS.Commands;
using Tools.CQRS.Queries;
using NetAzure_DemoApi.CQRS.Queries;
using NetAzure_DemoApi.CQRS.Entities;

namespace NetAzure_DemoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddTransient<DbConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("Database")));
            builder.Services.AddTransient<DS.ToDoService>();
            builder.Services.AddTransient<ToDoService>();
            builder.Services.AddTransient<ICommandHandler<RegisterCommand>, RegisterCommandHandler>();
            builder.Services.AddTransient<IQueryHandler<LoginQuery, User>, LoginQueryHandler>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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