
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Helpers;
using ToDoList.Repository.General_Repository;
using ToDoList.Repository.Photo_Repository;
using ToDoList.Repository.Profile_Repository;
using ToDoList.Repository.ToDoTasks;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using ToDoList.Server.Interfaces;
using ToDoList.Service.Auth_Service;
using ToDoList.Repository.UnitOfWork;
using ToDoList.Middlewares;
using FluentValidation;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<ToDoDBcontext>(options =>

             options.UseSqlServer(builder.Configuration.GetConnectionString("cs"))

            );


            //CloudinarySettings 
            builder.Services.Configure<CloudinarySettings>(builder.
                Configuration.GetSection("CloudinarySettings"));

            //Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ToDoDBcontext>();



            //scopse
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            //unit of work scope 
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //auto mapper
            builder.Services.AddAutoMapper(cfg => { },typeof(Program));

            //fluentvalidations
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();


            //if (app.Environment.IsDevelopment())
            //{
            //    app.MapOpenApi();
            //}

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
