using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace akWXHelper.Models
{
    public class RData<T> : RModel
    {
        /// <summary>
        /// 
        /// </summary>
        public RDataList<T> Data { get; set; }
    }
}
