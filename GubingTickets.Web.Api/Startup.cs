using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GubingTickets.DataAccessLayer.Implementations;
using GubingTickets.DataAccessLayer.Interfaces;
using GubingTickets.Web.ApplicationLayer.BusinessLogic.Implementations;
using GubingTickets.Web.ApplicationLayer.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GubingTickets.Utilities.Cache;
using GubingTickets.DataAccessLayer.Utils.ConnectionFactory;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace GubingTickets.Web.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<ITicketRequestsDataLayer, TicketRequestsDataLayer>();
            services.AddSingleton<ITicketRequestsLayer, TicketRequestsLayer>();
            services.AddSingleton<ICachingLayer, CachingLayer>();
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseStaticFiles();

            app.UseCors(ConfigureCors);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Gubing Tickets API");
            });
        }

        private void ConfigureCors(CorsPolicyBuilder corsPolicy)
        {
            corsPolicy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
    }
}
