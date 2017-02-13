using System;
using System.Collections.Generic;
using System.Linq;
using Hangfire;
using Microsoft.Owin;
using Owin;
using WebAppHangFireMVC.Controllers.Slack;

[assembly: OwinStartup(typeof(WebAppHangFireMVC.Startup))]

namespace WebAppHangFireMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("connectionString");

            // Job 1
            BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));

            // Job 2
            RecurringJob.AddOrUpdate("jobMinuto", () => Console.Write("Easy! Executado a cada minuto"), Cron.Minutely);

            // Job 3
            string cronExp = "0/1 * * * *";
            RecurringJob.AddOrUpdate("jobRWM", () => Console.WriteLine("meu teste rwn"), cronExp);

            // Job 4 - Slack
            var mensagem = new Mensagem();
            RecurringJob.AddOrUpdate("jobMinutoSlack", () => Mensagem.stpEnvia_Mensagem_Slack("hangfire","Esta mensagem foi disparada pelo Hangfire!!! ") , Cron.Minutely);

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
