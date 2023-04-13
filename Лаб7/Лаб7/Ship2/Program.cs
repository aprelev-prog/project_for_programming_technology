﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ship2
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var services = new ServiceCollection();
            ConfigureServices(services);
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {

                Application.Run(serviceProvider.GetRequiredService<FormMapWithSetShips>());
            }
        }
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<FormMapWithSetShips>()
            .AddLogging(option =>
            {
                option.SetMinimumLevel(LogLevel.Information);
                option.AddNLog("nlog.config");
            });
        }
    }
}