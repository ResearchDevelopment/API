using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShadiWebApplication.DAL;
using ShadiWebApplication.Logger;


namespace ShadiWebApplication
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login/";
                });

            services.AddScoped<ILogger, NLogger>((x) => { return new NLogger("ShadiWebApplication", Guid.NewGuid()); });
            services.AddScoped<IVocabularyRepository>((x) =>
            { return CreateShadiAppRepository(); });

#if DEBUG
            services.AddMvc().AddRazorRuntimeCompilation();
#else
            services.AddMvc();
#endif

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("94.101.140.210", "*", "http://localhost:4200/");
                                                         
                                  });
            });
        }

        private IVocabularyRepository CreateShadiAppRepository()
        {
            string shadiCnn = GetConnectionString("ShadiApp");
            return new VocabularyRepository(shadiCnn);
        }

        private string GetConnectionString(string name)
        {
            string cnn = Configuration.GetConnectionString(name);
            if (string.IsNullOrWhiteSpace(cnn))
                throw new Exception($"ConnectionStrings.{name} is null");

            return cnn;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/Home/Error");


            if (env.IsDevelopment())
            {

                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
