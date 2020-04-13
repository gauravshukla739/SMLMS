using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SMLMS.Data.Entity;
using SMLMS.Data.Interfaces;
using SMLMS.Helper.AppSetting;
using SMLMS.Model.Core;
using SMLMS.Services;
using SMLMS.Services.interfaces;
using SMLMS.Services.services;


namespace SMLMS.REST
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

      //  public readonly string AllowAllOriginsPolicy= "test123";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer( Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<SmtpDetails>(Configuration.GetSection("SmtpDetails"));
            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));
        
            services.AddDefaultIdentity<ApplicationUser>().AddRoles<Role>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // ===== Add Jwt Authentication ========
            // JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            //services
            //    .AddAuthentication(options =>
            //    {
            //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //    })
            //    .AddJwtBearer(cfg =>
            //    {
            //        cfg.RequireHttpsMetadata = false;
            //        cfg.SaveToken = true;
            //        cfg.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidIssuer = Configuration["JwtIssuer"],
            //            ValidAudience = Configuration["JwtIssuer"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
            //           // ClockSkew = TimeSpan.Zero // remove delay of token when expire
            //        };
            //    });
            var key = Encoding.ASCII.GetBytes(Configuration["JwtKey"]);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
            {
                option.RequireHttpsMetadata = false;
                option.SaveToken = true;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IUnitOfWork, DapperUnitOfWork>(provider => new DapperUnitOfWork(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ILeaveService, LeaveService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            


           // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                               opt.TokenLifespan = TimeSpan.FromHours(2));
            services.ConfigureSwaggerGen(c =>
            {
                c.SwaggerDoc("v3", new OpenApiInfo
                {
                    Title = "API",
                    Version = "v3"
                });
            });
        }
    


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder =>
               builder
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowAnyOrigin()
           );
            app.UseHttpsRedirection();
     
             app.UseRouting();
            //app.UseCors();
            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
