using API_PCHY.Models.QLTN.QLTN_KYSO;
using API_PCHY.Services.SMART_CA;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace APIPCHY_PhanQuyen
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
            // C?u h?nh Swagger cho API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIPCHY", Version = "v1" });
            });

            // C?u h?nh CORS (Cho ph�p t?t c? c�c origin, header v� method)
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            // ��ng k? c�c d?ch v? c?a ?ng d?ng
            services.AddControllers();

            // ��ng k? c�c d?ch v? Transient
            services.AddTransient<SmartCA769>();
            services.AddTransient<QLTN_KYSO_Manager>();

            // C?u h?nh JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, // Ki?m tra Issuer
                        ValidateAudience = true, // Ki?m tra Audience
                        ValidateLifetime = true, // Ki?m tra th?i gian s?ng c?a token
                        ValidateIssuerSigningKey = true, // Ki?m tra kh�a k?
                        ValidIssuer = Configuration["JwtSettings:Issuer"], // �?a ch? Issuer
                        ValidAudience = Configuration["JwtSettings:Audience"], // �?a ch? Audience
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:Secret"])) // Kh�a b� m?t
                    };
                });

            // ��ng k? c�c d?ch v? kh�c n?u c?n
            //services.AddScoped<IFileService, FileService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIPCHY v1"));
            }

            // C?u h?nh c�c middleware
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            // S? d?ng CORS
            app.UseCors("CorsPolicy");

            // S? d?ng Authentication v� Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // C?u h?nh c�c endpoints API
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
