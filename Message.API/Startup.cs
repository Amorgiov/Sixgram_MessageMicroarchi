using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using Message.Core.Options;
using Message.Core.Profiles;
using Message.Core.Services.Chat;
using Message.Core.Services.File;
using Message.Core.Services.Hubs;
using Message.Core.Services.Message;
using Message.Core.Services.Token;
using Message.Core.Services.User;
using Message.Database.Context;
using Message.Database.Repository.Chat;
using Message.Database.Repository.Member;
using Message.Database.Repository.Message;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Message.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureAuthentication(services);
            ConfigureSwagger(services);

            // Configure Repositories & Services
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.AddScoped<IFileStorageHttpService, FileStorageHttpService>();
            services.AddScoped<IMessageService, MessageService>();

            
            //Configure DbContext
            var connection = Configuration.GetConnectionString("Default");
            services.AddDbContext<ApplicationContext>(_ => _.UseNpgsql(connection,  
                x => x.MigrationsAssembly("Message.Database")));
            
            services.AddSignalR();
            services.AddHttpContextAccessor();
            
            var mapperConf = new MapperConfiguration(_ =>
            {
                _.AddProfile<AppProfile>();
            });

            var mapper = mapperConf.CreateMapper();
            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton(mapper);
            
            services.Configure<BaseAdresses>(Configuration.GetSection(BaseAdresses.BaseAddress));
            var addressesOptions = Configuration.GetSection(BaseAdresses.BaseAddress).Get<BaseAdresses>();
            services.AddSingleton(addressesOptions);
            
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            
            //Configure HttpClient
            services.AddHttpClient("auth",
                p => { p.BaseAddress = new Uri("http://localhost:5176"); });
            services.AddHttpClient("file_storage",
                c => { c.BaseAddress = new Uri("http://localhost:5000"); });
            
            services.AddControllers();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Message.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>  
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/hub");
            });
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    BearerFormat = "Bearer {authToken}",
                    Description = "JSON Web Token to access resources. Example: Bearer {token}",
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme, Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }
        
        private void ConfigureAuthentication(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Configuration["AppOptions:SecretKey"]);
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuer = false,
                        RequireExpirationTime = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                    x.SaveToken = true;
                });
        }
    }
}