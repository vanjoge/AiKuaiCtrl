using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SQ.Base;

namespace akWXHelper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (MyTask.Singleton.Start())
            {
                Console.WriteLine("启动成功");
                CreateHostBuilder(args).Build().Run();
            }
            else
            {
                Console.WriteLine("启动失败，程序退出");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(ConfigurationHelper.GetAppSetting("urls"));
                    webBuilder.UseStartup<Startup>();
                });
    }
}
