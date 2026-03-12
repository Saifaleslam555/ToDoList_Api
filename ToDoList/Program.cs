using ToDoList.Middlewares;
using ToDoList.Extensions;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region test
            //// Add services to the container.

            //builder.Services.AddControllers();
            //// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();

            //builder.Services.AddDbContext<ToDoDBcontext>(options =>

            // options.UseSqlServer(builder.Configuration.GetConnectionString("cs"))

            //);


            ////Defualt imge
            //builder.Services.Configure<DefaultImgUrl>(
            //    builder.Configuration.GetSection("DefaultImg"));

            ////CloudinarySettings 
            //builder.Services.Configure<CloudinarySettings>(builder.
            //    Configuration.GetSection("CloudinarySettings"));

            ////Identity
            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            //.AddEntityFrameworkStores<ToDoDBcontext>();

            ////scopse
            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
            //builder.Services.AddScoped<IAuthService, AuthService>();

            ////unit of work scope 
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            ////auto mapper
            //builder.Services.AddAutoMapper(cfg => { },typeof(Program));

            ////fluentvalidations
            //builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);


            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            #endregion

            builder.Services.ApplicationService(builder.Configuration);

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
