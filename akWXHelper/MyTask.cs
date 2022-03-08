using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using akWXHelper.EF.WebApplication1.Models;
using akWXHelper.Models;
using Microsoft.EntityFrameworkCore;
using SQ.Base;

namespace akWXHelper
{
    public class MyTask
    {
        public static MyTask Singleton = new MyTask();

        public MyTask()
        {
            http = new AiKuaiHttp(ConfigurationHelper.GetAppSetting("username"), ConfigurationHelper.GetAppSetting("passwd"), ConfigurationHelper.GetAppSetting("passH"), ConfigurationHelper.GetAppSetting("akurl"));

            string connecttext = SQ.Base.FileHelp.GetMyConfPath() + "db";

            System.IO.Directory.CreateDirectory(connecttext);
            connecttext = "Data Source=" + connecttext + "/ak.db";
            var optionsBuilder = new DbContextOptionsBuilder<SqlContext>();
            optionsBuilder.UseSqlite(connecttext);
            context = new SqlContext(optionsBuilder.Options);
            context.Database.EnsureCreated();

        }

        public static string ToMbps(double val)
        {
            return $"{(val / 131072).ToString("0.00")}Mbps";
        }
        public static string GetHSize(long blen)
        {
            if (blen < 0x400)
            {
                return blen + " B";
            }
            var dblen = Convert.ToDouble(blen);
            if (blen < 0x100000)
            {
                return Math.Round(dblen / 1024, 2) + " KB";
            }
            if (blen < 0x40000000)
            {
                return Math.Round(dblen / 0x100000, 2) + " MB";
            }
            if (blen < 0x10000000000)
            {
                return Math.Round(dblen / 0x40000000, 2) + " GB";
            }
            if (blen < 0x4000000000000)
            {
                return Math.Round(dblen / 0x10000000000, 2) + " TB";
            }
            if (blen < 0x1000000000000000)
            {
                return Math.Round(dblen / 0x4000000000000, 2) + " PB";
            }
            return "Max";

        }
        public bool Start()
        {
#if DEBUG
            return true;
#endif

            if (http.Login())
            {
                SQ.Base.ThreadWhile<object> th = new ThreadWhile<object>();
                th.SleepMs = 1000;
                th.Start(run, null, "MyTask");
                return true;
            }
            return false;
        }
        public AiKuaiHttp http;
        public SqlContext context;

        int hour = -1;
        void run(object tag, CancellationToken cancellationToken)
        {
            var dtnow = DateTime.Now;
            if (dtnow.Hour != hour || (dtnow.Hour == 23 && dtnow.Minute == 59 && dtnow.Second >= 55))
            {
                var ips = http.monitor_lanip();
                context.AddRange(ips.data);
                context.SaveChanges();
                hour = dtnow.Hour;
            }
        }
    }
}
