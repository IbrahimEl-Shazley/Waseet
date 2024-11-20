using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Globalization;
using System.Text;
using Wasit.Context;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;
using Wasit.Core.Models;
using Wasit.Core.Models.DTO;
using Wasit.Integration.Services.Abstraction;
using Wasit.Integration.Services.Implementation;
using Wasit.Repositories.Implementations;
using Wasit.Repositories.Implementations.Daily;
using Wasit.Repositories.Implementations.Entertainment;
using Wasit.Repositories.Implementations.Rent;
using Wasit.Repositories.Implementations.Sale;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.Interfaces.Daily;
using Wasit.Repositories.Interfaces.Entertainment;
using Wasit.Repositories.Interfaces.Rent;
using Wasit.Repositories.Interfaces.Sale;
using Wasit.Repositories.UnitOfWork;
using Wasit.Service.Implementation.General;
using Wasit.Service.Interfaces.General;
using Wasit.Services;
using Wasit.Services.Implementation;
using Wasit.Services.Implementation.General;
using Wasit.Services.Implementations.General;
using Wasit.Services.Implementations.Generic.ConsumerEstates;
using Wasit.Services.Implementations.Generic.ConsumerRequests;
using Wasit.Services.Implementations.Generic.MyEstates;
using Wasit.Services.Implementations.Generic.PropertiesManagement;
using Wasit.Services.Implementations.Generic.Shared;
using Wasit.Services.Interfaces;
using Wasit.Services.Interfaces.General;
using Wasit.Services.Interfaces.Generic.ConsumerEstates;
using Wasit.Services.Interfaces.Generic.ConsumerRequests;
using Wasit.Services.Interfaces.Generic.MyEstates;
using Wasit.Services.Interfaces.Generic.PropertiesManagement;
using Wasit.Services.Interfaces.Generic.Shared;
using Wasit.Services.MapperConfig;

namespace Wasit.Helpers
{
    public static class ConfigureServices
    {
        public static void AddDbContextServices(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("MsSqlConnectionString")));
        }


        public static void AddSingletonServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        }

        public static void AddLocalizationServices(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                GetLocalizationOptions();
            });
        }


        public static RequestLocalizationOptions GetLocalizationOptions()
        {
            var supportedCultures = new[] { new CultureInfo("ar"), new CultureInfo("en") };
            supportedCultures[0].DateTimeFormat = supportedCultures[1].DateTimeFormat;
            supportedCultures[0].NumberFormat = supportedCultures[1].NumberFormat;

            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(supportedCultures[0]),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>()
                {
                    // Order is important, its in which order they will be evaluated
                    new AcceptLanguageHeaderRequestCultureProvider(),
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider(),
                }
            };

            return localizationOptions;
        }


        public static void ScanAllServices(this IServiceCollection services)
        {
            services.Scan
            (
                scan => scan
                    .FromAssemblyOf<ServicesAssembly>()
                    .AddClasses
                    (
                        classes => classes.Where
                        (type =>
                            (type.IsInterface || type.IsClass) && (type.Name.EndsWith("Service") || type.Name.EndsWith("Services"))
                        )
                    )
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
            );
        }

        public static void TimeOutServices(this IServiceCollection services, IWebHostEnvironment Environment)
        {
            services.AddDataProtection()
                              .SetApplicationName($"my-app-{Environment.EnvironmentName}")
                              .PersistKeysToFileSystem(new DirectoryInfo($@"{Environment.ContentRootPath}\keys"));

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(300);
            });


        }

        public static void AddDefaultIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationDbUser, IdentityRole>(options =>
            {
                // Default Password settings.
                options.User.RequireUniqueEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            }).AddDefaultUI()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }
        public static void AddJwtServices(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddAuthentication(options =>
            {
                //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {

                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = Configuration["Jwt:Site"],
                    ValidIssuer = Configuration["Jwt:Site"],

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SigningKey"]))
                };
            });

        }


        public static void AddScopedServices(this IServiceCollection services)
        {
            //services.AddScoped<IHelper, Helper>();
            //services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IUserRepository, UserRepository>();
            //  services.AddScoped<IValidator<UserRegisterDTO>, UserRegisterValidator>();

            // BASE DI
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IBaseRepository<Entity>, BaseRepository<Entity>>();
            services.AddScoped<IBaseService, BaseService>();
            services.AddScoped<IBaseService<Entity, DTO, DTO, DTO>, BaseService<Entity, DTO, DTO, DTO>>();
        }


        public static void AddTransientServices(this IServiceCollection services)
        {
            // GENERAL SERVICES DI
            services.AddTransient<ICurrentUserService, CurrentUserService>();

            #region Repositories DI

            #region Sale Section    
            services.AddTransient<ISaleEstateRepository, SaleEstateRepository>();
            #endregion

            #region Rent Section    
            services.AddTransient<IRentEstateRepository, RentEstateRepository>();
            #endregion

            #region Entertainment Section
            services.AddTransient<IEntertainmentEstateRepository, EntertainmentEstateRepository>();
            #endregion

            #region DailyRent Section
            services.AddTransient<IDailyRentEstateRepository, DailyRentEstateRepository>();
            #endregion

            #region User Section
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            #endregion

            #endregion

            #region Services DI
            // Notif DI
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<ISMSService, SMSService>();

            //LookUp DI
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IRegionService, RegionService>();

            #region My Estates
            services.AddTransient<IGeneralMySharedEstatesService, GeneralMySharedEstatesService>();

            services.AddTransient<SaleEstateService>();
            services.AddTransient<ISaleEstatesService, SaleEstateService>();

            services.AddTransient<RentEstateService>();
            services.AddTransient<IRentEstatesService, RentEstateService>();

            services.AddTransient<DailyRentEstateService>();
            services.AddTransient<IDailyRentEstatesService, DailyRentEstateService>();

            services.AddTransient<EntertainmentEstatesService>();
            services.AddTransient<IEntertainmentEstatesService, EntertainmentEstatesService>();

            services.AddTransient<Func<CategoryType, IMySharedEstatesService>>(serviceProvider => type =>
            {
                return type switch
                {
                    CategoryType.Sale => serviceProvider.GetService<SaleEstateService>(),
                    CategoryType.Rent => serviceProvider.GetService<RentEstateService>(),
                    CategoryType.DailyRent => serviceProvider.GetService<DailyRentEstateService>(),
                    CategoryType.Entertainment => serviceProvider.GetService<EntertainmentEstatesService>(),
                    _ => throw new InvalidOperationException(),
                };
            });
            #endregion

            

            #region Consumer Estates
            services.AddTransient<ConsumerSaleEstatesService>();
            services.AddTransient<IConsumerSaleEstatesService, ConsumerSaleEstatesService>();

            services.AddTransient<ConsumerRentEstatesService>();
            services.AddTransient<IConsumerRentEstatesService, ConsumerRentEstatesService>();

            services.AddTransient<ConsumerDailyRentEstatesService>();
            services.AddTransient<IConsumerDailyRentEstatesService, ConsumerDailyRentEstatesService>();

            services.AddTransient<ConsumerEntertainmentEstatesService>();
            services.AddTransient<IConsumerEntertainmentEstatesService, ConsumerEntertainmentEstatesService>();

            services.AddTransient<IConsumerFavouriteEstatesService, ConsumerFavouriteEstatesService>();
            services.AddTransient<IConsumerGeneralSharedService, ConsumerGeneralSharedService>();

            services.AddTransient<Func<CategoryType, IConsumerSharedEstatesService>>(serviceProvider => type =>
            {
                return type switch
                {
                    CategoryType.Sale => serviceProvider.GetService<ConsumerSaleEstatesService>(),
                    CategoryType.Rent => serviceProvider.GetService<ConsumerRentEstatesService>(),
                    CategoryType.DailyRent => serviceProvider.GetService<ConsumerDailyRentEstatesService>(),
                    CategoryType.Entertainment => serviceProvider.GetService<ConsumerEntertainmentEstatesService>(),
                    _ => throw new InvalidOperationException(),
                };
            });
            #endregion

            #region Consumer Requests
            services.AddTransient<ConsumerPurchaseRequestsService>();
            services.AddTransient<IConsumerPurchaseRequestsService, ConsumerPurchaseRequestsService>();

            services.AddTransient<ConsumerRentRequestsService>();
            services.AddTransient<IConsumerRentRequestsService, ConsumerRentRequestsService>();

            services.AddTransient<ConsumerEntertainmentRequestsService>();
            services.AddTransient<IConsumerEntertainmentRequestsService, ConsumerEntertainmentRequestsService>();

            services.AddTransient<ConsumerDailyRentRequestsService>();
            services.AddTransient<IConsumerDailyRentRequestsService, ConsumerDailyRentRequestsService>();

            services.AddTransient<Func<CategoryType, IConsumerSharedRequestsService>>(serviceProvider => type =>
            {
                return type switch
                {
                    CategoryType.Sale => serviceProvider.GetService<ConsumerPurchaseRequestsService>(),
                    CategoryType.Rent => serviceProvider.GetService<ConsumerRentRequestsService>(),
                    CategoryType.Entertainment => serviceProvider.GetService<ConsumerEntertainmentRequestsService>(),
                    CategoryType.DailyRent => serviceProvider.GetService<ConsumerDailyRentRequestsService>(),
                    _ => throw new InvalidOperationException(),
                };
            });
            #endregion

            #region PropertiesManagement
            services.AddScoped<IRentManagementService, RentManagementService>();
            services.AddScoped<IMaintainanceMangementService, MaintainanceMangementService>();
            services.AddScoped<IPayApartmentRentService, PayApartmentRentService>();
            #endregion

            #region Shared
            services.AddTransient<INotificationsService, NotificationsService>();
            services.AddTransient<IGeneralService, GeneralService>();
            #endregion

            #endregion

            //Swagger UI Configuration Register
            services.AddTransient<IConfigureOptions<SwaggerUIOptions>, SwaggerUIConfiguration>();
        }


        public static void SetEnvironment(this IServiceCollection services, IWebHostEnvironment environment)
        {
            Hosting.Environment = environment;
        }


        public static void AddCorsServices(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                //options.AddDefaultPolicy(
                //    builder =>
                //    {
                //        builder.WithOrigins("https://localhost:44306/")
                //        .AllowAnyHeader()
                //        .AllowAnyMethod()
                //        .AllowCredentials();
                //    });
                options.AddPolicy("Wasit", o =>
                {
                    o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
        }



        //    public static void AddController(this IServiceCollection services)
        //    {
        //        services.AddControllers();
        //    }


        public static void addfluentvalidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
             .AddFluentValidationClientsideAdapters()
             .AddValidatorsFromAssembly(typeof(ServicesAssembly).Assembly);
        }


        public static void addAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(new Action<IMapperConfigurationExpression>(cfg =>
             {
                 cfg.AddProfile(new MapperProfile(new CurrentUserService(new HttpContextAccessor())));
             }));
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("Auth", new OpenApiInfo { Title = "Auth", Version = "v1" });
                options.SwaggerDoc("MyEstates", new OpenApiInfo { Title = "MyEstates", Version = "v1" });
                options.SwaggerDoc("EstatesServices", new OpenApiInfo { Title = "EstatesServices", Version = "v1" });
                options.SwaggerDoc("ConsumerEstates", new OpenApiInfo { Title = "ConsumerEstates", Version = "v1" });
                options.SwaggerDoc("ConsumerRequests", new OpenApiInfo { Title = "ConsumerRequests", Version = "v1" });
                options.SwaggerDoc("PropertiesManagement", new OpenApiInfo { Title = "PropertiesManagement", Version = "v1" });
                options.SwaggerDoc("Shared", new OpenApiInfo { Title = "Shared", Version = "v1" });
                options.OperationFilter<SwaggerCustomHeaderAttribute>();
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Name = "Authorization",
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Specify the authorization token.",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
                    new string[] {}
                }});

                string xmlPath1 = System.IO.Path.Combine(Environment.CurrentDirectory, "Wasit.Services.xml");
                string xmlPath2 = System.IO.Path.Combine(Environment.CurrentDirectory, "Wasit.xml");

                options.IncludeXmlComments(xmlPath1);
                options.IncludeXmlComments(xmlPath2);


            });
        }


        public class SwaggerUIConfiguration : IConfigureOptions<SwaggerUIOptions>
        {
            public void Configure(SwaggerUIOptions options)
            {
                options.SwaggerEndpoint("/swagger/Auth/swagger.json", "Auth");
                options.SwaggerEndpoint("/swagger/MyEstates/swagger.json", "MyEstates");
                options.SwaggerEndpoint("/swagger/EstatesServices/swagger.json", "EstatesServices");
                options.SwaggerEndpoint("/swagger/ConsumerEstates/swagger.json", "ConsumerEstates");
                options.SwaggerEndpoint("/swagger/ConsumerRequests/swagger.json", "ConsumerRequests");
                options.SwaggerEndpoint("/swagger/PropertiesManagement/swagger.json", "PropertiesManagement");
                options.SwaggerEndpoint("/swagger/Shared/swagger.json", "Shared");
            }
        }
    }
}
