using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Agree.Allow.Infrastructure.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Agree.Allow.Infrastructure.IoC;
using Agree.Allow.Infrastructure.Data;
using Agree.Allow.Domain.Security;
using Agree.Allow.Presentation.ViewModels;
using Agree.Allow.Presentation.Mappings;
using Microsoft.Net.Http.Headers;

namespace Agree.Allow.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddPolicy("DefaultCorsPolicy", builder =>
                    builder.WithMethods("GET", "POST", "PATCH", "PUT", "DELETE", "OPTIONS")
                        .WithHeaders(
                            HeaderNames.Accept,
                            HeaderNames.ContentType,
                            HeaderNames.Authorization)
                        .AllowCredentials()
                        .SetIsOriginAllowed(origin =>
                        {
                            if (string.IsNullOrWhiteSpace(origin)) return false;
                            if (origin.ToLower().StartsWith("http://localhost")) return true;
                            return false;
                        })
                )
            );

            services
                .AddDefaultIdentity<ApplicationUser>(NativeContainerBootStrapper.ConfigureIdentity)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper(opt =>
            {
                opt.AddProfile<EntityToViewModelMap>();
            });

            NativeContainerBootStrapper.ConfigureServices(services, Configuration);
            NativeContainerBootStrapper.ConfigureAuthentication(services, Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Agree.Allow", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agree.Allow v1"));
            }

            // app.UseHttpsRedirection();

            app.UseCors("DefaultCorsPolicy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
