using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace akWXHelper.Models
{
    public class StatDayLanip
    {
        public StatDayLanip(string Key, List<MonitorLanip> monitorLanips, bool akdaily)
        {
            this.Key = Key;
            this.Data = monitorLanips;
            this.akdaily = akdaily;
        }
        public string Key { get; set; }

        public List<MonitorLanip> Data { get; protected set; }

        bool akdaily;
        public bool GetCHBytes(int start, int end, out long bytesRec, out long bytesUp)
        {
            bytesRec = 0;
            bytesUp = 0;
            MonitorLanip last = null;
            foreach (var item in Data)
            {
                if (item.GetTime.Hour < start)
                {
                    continue;
                }
                if (item.GetTime.Hour > end)
                {
                    break;
                }
                if (last == null)
                {
                    last = item;
                    if (akdaily && start == 0 && item.GetTime.TimeOfDay.TotalSeconds > 5)
                    {
                        bytesRec += item.total_down;
                        bytesUp += item.total_up;
                    }
                }
                else
                {
                    if (item.total_up >= last.total_up && item.total_down >= last.total_down)
                    {
                        bytesRec += item.total_down - last.total_down;
                        bytesUp += item.total_up - last.total_up;
                    }
                    else
                    {
                        bytesRec += item.total_down;
                        bytesUp += item.total_up;
                    }
                    last = item;
                }
            }

            return true;
        }

    }


}
