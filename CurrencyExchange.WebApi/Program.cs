using AutoMapper;
using CurrencyExchange.Application;
using CurrencyExchange.Application.Profiles;
using CurrencyExchange.Infrastructure;
using Microsoft.OpenApi.Models;

namespace CurrencyExchange.WebApi
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApplicationLayer();
            builder.Services.AddInfrastructureLayer(builder.Configuration);

            var mapperConfig = new MapperConfiguration(mc =>
            {

                mc.AddProfile(new GeneralProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:7064")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });


            var app = builder.Build();




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("../swagger/v1/swagger.json", "CurrencyExchange.WebApi");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}