using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudioUp.Models;
using StudioUp.Repo;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;
using StudioUp.Repo.Repository;
using System.Text;
using NLog;
using NLog.Web;
using StudioUp.DTO;
using StudioUp.Repo.Repository;
using StudioUp.Repo;

namespace StudioUp.API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            // Early init of NLog to allow startup and exception logging, before host is built
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                // Configure services
                ConfigureServices(builder);
                var app = builder.Build();
                // Configure middleware
                ConfigureMiddleware(app);
                app.Run();
            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }


        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers();

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            // Configuration
            builder.Configuration.AddJsonFile("appsettings.json", optional: false);

            // Database context
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("StudioUp")));

            // Repositories
            builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();
            builder.Services.AddScoped<IContentTypeRepository, ContentTypeRepository>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddScoped<IRepository<TrainingTypeDTO>, TrainingTypeRepository>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // áãåâîä äæå, äîéîåù ùì ICustomerSubscriptionRepository äåà CustomerSubscriptionRepository
            builder.Services.AddScoped<ICustomerSubscriptionRepository, CustomerSubscriptionRepository>();
            builder.Services.AddScoped<IContactRepository, ContactRepository>();
            builder.Services.AddScoped<IHMORepository, HMORepository>();
            builder.Services.AddScoped<IAvailableTrainingRepository, AvailableTrainingRepository>();
            builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
            builder.Services.AddScoped<ICustomerTypeRepository, CustomerTypeRepository>();
            builder.Services.AddScoped<IRepository<SubscriptionTypeDTO>, SubscriptionTypeRepository>();
            builder.Services.AddScoped<IRepository<PaymentOptionDTO>, PaymentOptionRepository>();
            builder.Services.AddScoped<IRepository<TrainingTypeDTO>, TrainingTypeRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            builder.Services.AddScoped<EmailService>();
            builder.Services.AddScoped<IContentSectionRepository, ContentSectionRepository>();

            builder.Services.AddScoped<IInternalHomeLinksRepository,InternalHomeLinksRepository>();
            builder.Services.AddScoped<ITrainingCustomerTypesRepository, TrainingCustomerTypeRepository>();
            builder.Services.AddScoped<ICustomerHMOSRepository, CustomerHMOSRepository>();
            builder.Services.AddScoped<ILeumitCommimentsRepository, LeumitCommimentRepository>();
            builder.Services.AddScoped<ILeumitCommimentTypesRepository, LeumitCommimentTypesRepository>();
            builder.Services.AddScoped<IFileUploadRepository, FileUploadRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            builder.Services.AddScoped<ITrainingCustomerRepository, TrainingCustomerRepository>();
            //builder.Services.AddScoped<ITrainingCustomerTypeRepository, TrainingCustomerTypeRepository>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();


            // AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));
           // builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Add JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });

            // Add Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Scheme = "Bearer",
                //    BearerFormat = "JWT",
                //    In = ParameterLocation.Header,
                //    Name = "Authorization",
                //    Description = "Bearer Authentication with JWT Token",
                //    Type = SecuritySchemeType.Http
                //});
                //options.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Id = "Bearer",
                //                Type = ReferenceType.SecurityScheme
                //            }
                //        },
                //        new List<string>()
                //    }
                //});
            });

            builder.Services.AddLogging();
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowOrigin");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=ContentTypeController}/{action=Index}/{id?}");
        }
    }
}
