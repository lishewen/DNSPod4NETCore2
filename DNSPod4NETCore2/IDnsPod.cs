using System;
using System.Collections.Generic;
using System.Text;

namespace DNSPod4NETCore2
{
    interface IDnsPod
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="paramObject"></param>
        /// <returns></returns>
        int Create(object paramObject);

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="paramObject"></param>
        /// <returns></returns>
        dynamic List(object paramObject);

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="paramObject"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool Remove(object paramObject);

        dynamic Info(object paramObject);
    }
}
