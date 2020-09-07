using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramBot.Bot;
using TelegramBotClient.Abstractions;

namespace TelegramBot
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
            services.AddControllers().AddNewtonsoftJson();
            services.AddTransient<BotSettings>();
            services.AddSingleton<MyTelegramBotClient>();
            services.AddSingleton<LibTelegramBotClient>();
            services.AddSingleton<ITelegramBotClient>(serviceProvider =>
            {
                switch(serviceProvider.GetRequiredService<BotSettings>().BotType)
                {
                    case "MyBot":
                        return serviceProvider.GetService<MyTelegramBotClient>();
                    case "ThirdPartyBot":
                        return serviceProvider.GetService<LibTelegramBotClient>();
                    default:
                        throw new KeyNotFoundException(message: "Not found key for ITelegramBotService");
                };
            });
            services.AddTransient<Invoker>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ITelegramBotClient client)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.Use((context, next) =>
            {
                context.Request.EnableBuffering();
                return next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "/api/{controller=Message}/{action=Update}");
            }); 
        }
    }
}
