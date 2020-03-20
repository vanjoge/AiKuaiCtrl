using System.Collections.Generic;

namespace AiKuaiCtrl.Models
{
    public class RDataList<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<T> data { get; set; }
    }
}
