
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using akWXHelper.Models;
using Microsoft.EntityFrameworkCore;

namespace akWXHelper.EF
{
    namespace WebApplication1.Models
    {
        public class SqlContext : DbContext
        {
            public SqlContext(DbContextOptions<SqlContext> Options) : base(Options)
            {
            }
            public DbSet<MonitorLanip> MonitorLanip { get; set; }

        }

    }
}
