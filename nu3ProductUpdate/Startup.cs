using LiteDB;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using nu3ProductUpdate.Data;
using nu3ProductUpdate.Data.Classes;
using nu3ProductUpdate.Data.Interfaces;
using nu3ProductUpdate.Data.Services;

namespace nu3ProductUpdate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            BsonMapper.Global.Entity<Models.Inventory>().Id(item => item.Handle);
            BsonMapper.Global.Entity<Models.Product>().Id(item => item.Id);

            services.Configure<LiteDbOptions>(Configuration.GetSection("LiteDbOptions"));

            services.AddSingleton<IDbContext, DBContext>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IFilesService, FilesService>();
            services.AddTransient<IInventoryService, InventoryService>();

            services.AddRouting();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })

            .AddCookie(options =>
            {
                options.LoginPath = "/";
                options.LogoutPath = "/";
            })

           .AddGitHub(options =>
           {
               options.ClientId = Configuration["GithubOauth:ClientId"];
               options.ClientSecret = Configuration["GithubOauth:ClientSecret"];
               options.Scope.Add("user:email");
           });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}