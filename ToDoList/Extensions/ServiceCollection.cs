using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Configurations;
using ToDoList.Data;
using ToDoList.Helpers;
using ToDoList.Repository.General_Repository;
using ToDoList.Repository.Photo_Repository;
using ToDoList.Repository.UnitOfWork;
using ToDoList.Server.Interfaces;
using ToDoList.Service.Auth_Service;

namespace ToDoList.Extensions
{
    internal static class ServiceCollection 
    {
        public static IServiceCollection ApplicationService(this IServiceCollection services,IConfiguration Configuration) 
        {

            services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddOpenApi();

            services.AddDbContext<ToDoDBcontext>(options =>

             options.UseSqlServer(Configuration.GetConnectionString("cs"))

            );

            //Defualt imge
            services.Configure<DefaultImgUrl>(
                Configuration.GetSection("DefaultImg"));

            //CloudinarySettings 
            services.Configure<CloudinarySettings>(
                Configuration.GetSection("CloudinarySettings"));

            //Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ToDoDBcontext>();

            //scopse
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IAuthService, AuthService>();

            //unit of work scope 
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //auto mapper
            services.AddAutoMapper(cfg => { }, typeof(Program));

            //fluentvalidations
            services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);

            //swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
