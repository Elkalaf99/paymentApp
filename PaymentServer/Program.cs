using Microsoft.EntityFrameworkCore;
using PaymentProject.Data;
using PaymentProject.Services;

namespace PaymentProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            
            // Add OpenAPI/Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Payment API",
                    Version = "v1",
                    Description = "A simple payment processing API"
                });
            });

            // Add DbContext
            builder.Services.AddDbContext<PaymentDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add Application Services
            builder.Services.AddScoped<IPaymentDetailService, PaymentDetailService>();

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment API V1");
                    c.RoutePrefix = "swagger";
                });
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
