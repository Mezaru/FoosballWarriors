using FoosballApi.Context;
using FoosballApi.Hubs;
using FoosballApi.Interface;
using FoosballApi.Repository;
using FoosballApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace FoosballApi
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
            services.AddDbContext<FoosballContext>(opt => { opt.UseSqlServer(Configuration["ConnectionStrings:FoosballApiContext"]); }, ServiceLifetime.Singleton);
            services.AddControllers();
            services.AddTransient<IDataRepository, DataRepository>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<ISignalService, SignalService>();

            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", bulder =>
                {
                    bulder.AllowAnyMethod();
                    bulder.AllowAnyHeader();
                    bulder.AllowCredentials();
                    bulder.WithOrigins("http://localhost:4200");
                });
            });
            
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
                RequestPath = new PathString("/Images")
            });

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<PlayerHub>("/playerHub");
            });
        }
    }
}
