using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace akWXHelper.Models
{
    public class MonitorLanip
    {
        public MonitorLanip()
        {
            GetTime = DateTime.Now;
        }
        /// <summary>
        /// 获取时间
        /// </summary>
        [Key, Column(Order = 0)]
        public DateTime GetTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string apname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Key, Column(Order = 1)]
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bssid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long ip_addr_int { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string downrate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ppptype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string client_device { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string uprate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mac { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int reject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int webid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long total_up { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string signal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ac_gid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string apmac { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long connect_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ssid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hostname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string client_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long upload { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string uptime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long total_down { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int auth_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dtalk_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string comment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long timestamp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ip_addr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long download { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string frequencies { get; set; }
    }


}
