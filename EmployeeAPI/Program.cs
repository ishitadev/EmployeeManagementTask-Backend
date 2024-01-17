using Employee.Data.Contaxt;
using Employee.Data.Core;
using Employee.Data.Core.Interface;
using Employee.Data.Interface;
using Employee.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DataContext>(options =>
            {
                //builder.cofiguration and not just configuration
                //options.UseInMemoryDatabase("EmployeeDb");
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            }, ServiceLifetime.Transient);
            
            // Add services to the container.
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            builder.Services.AddControllers();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            //builder.core
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAll");
            app.MapControllers();

            app.Run();
        }
    }
}
