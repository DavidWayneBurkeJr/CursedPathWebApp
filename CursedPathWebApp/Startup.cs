using CursedPathWebApp.Data;
using CursedPathWebApp.Hubs;
using CursedPathWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Sockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Newtonsoft.Json;

public class Startup
{
    public Startup(IHostingEnvironment env)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

        if (env.IsDevelopment())
        {
            // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
            builder.AddUserSecrets("aspnet-CursedPathWebApp-0f2df5e4-57d2-4982-9b44-70bbf369d2c0");
        }

        builder.AddEnvironmentVariables();
        Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Add framework services.
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("UserDatabase")));

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


        var settings = new JsonSerializerSettings();
        settings.ContractResolver = new SignalRContractResolver();

        var serializer = JsonSerializer.Create(settings);

        services.Add(new ServiceDescriptor(typeof(JsonSerializer),
                                        provider => serializer,
ServiceLifetime.Transient));


        services.AddMvc();

        // Add application services.
        services.AddTransient<IEmailSender, AuthMessageSender>();
        services.AddTransient<ISmsSender, AuthMessageSender>();
        services.AddSignalR(options => options.Hubs.EnableDetailedErrors = true);



    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
        loggerFactory.AddConsole(Configuration.GetSection("Logging"));
        loggerFactory.AddDebug();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
            app.UseBrowserLink();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();

        app.UseIdentity();

        //  Add external authentication middleware below.To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
        app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: "default",
                template: "{controller=ApplicationRole}/{action=Index}/{id?}");
        });
        app.UseSignalR();
        app.UseWebSockets();
    }
}

