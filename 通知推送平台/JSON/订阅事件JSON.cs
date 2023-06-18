using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 通知推送客户端.JSON
{
    public class Events_JSON
    {
        public class Events_Return
        {
            /// <summary>
            /// 订阅事件表
            /// </summary>
            public List<string>? Events { get; set; }
        }
    }
}
