using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Transaction.Data;
using Microsoft.OpenApi.Models;
using Transaction.Business.Services;
using Transaction.Business.Services.Impl;
using Transaction.Data.Repositories.Interfaces;
using Transaction.Data.Repositories.Impl;
using Serilog;

namespace Transaction.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<TContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString(nameof(TContext)),
                b => b.MigrationsAssembly(typeof(TContext).Assembly.FullName)));            
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabase(services);

            services.AddSingleton(Log.Logger);

            // configure unitof work
            services.AddScoped<IUnitOfWork, Data.Repositories.Infrastructure.UnitOfWork>();
            // configure repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            //configure services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IPersonService, PersonService>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Okon-Kufre Transaction API V1",
                    Version = "v1"
                });
            });
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Okon-Kufre Transaction API V1");
                c.RoutePrefix = string.Empty;
            });


        }
    }
}
