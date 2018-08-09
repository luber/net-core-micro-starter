﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Service
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddIdentityServer()
                    .AddDeveloperSigningCredential() // TODO: Replace before production!
                    // TODO: Before production replace all this with Persistent Store
                    .AddTestUsers(InMemoryConfiguration.GetTestUsers()) // clients are allowed to use this Auth server
                    .AddInMemoryIdentityResources(InMemoryConfiguration.GetIdentityResources())
                    .AddInMemoryApiResources(InMemoryConfiguration.GetApiResources()) //which APIs are allowed to use this Auth server
                    .AddInMemoryClients(InMemoryConfiguration.GetClients());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();

            // open http://localhost:5000/.well-known/openid-configuration
        }
    }
}
