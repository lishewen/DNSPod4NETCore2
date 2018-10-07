using System;

namespace DNSPod4NETCore2
{
    public class DnsPodRecord : IDnsPod
    {
        private readonly IDnsPodClient client;
        public DnsPodRecord(IDnsPodClient client)
        {
            this.client = client;
        }
        #region 增加

        /// <summary>
        /// 创建记录,默认记录类型为A
        /// </summary>
        /// <param name="domainId">域名ID</param>
        /// <param name="subDomain">二级域名名称</param>
        /// <param name="recordValue">记录值</param>
        /// <returns>记录ID</returns>
        public int Create(int domainId, string subDomain, string recordValue)
        {
            return Create(domainId, subDomain, recordValue, "A", "默认");
        }

        /// <summary>
        /// 创建记录,默认记录类型为A
        /// </summary>
        /// <param name="domainId">域名ID</param>
        /// <param name="subDomain">二级域名名称</param>
        /// <param name="recordValue">记录值</param>
        /// <param name="recordType">记录类型，通过API记录类型获得，大写英文，比如：A</param>
        /// <param name="recordLine">记录线路，通过API记录线路获得，中文，比如：默认</param>
        /// <returns>记录ID</returns>
        public int Create(int domainId, string subDomain, string recordValue, string recordType, string recordLine)
        {
            var recordId = 0;
            object p = new
            {
                domain_id = domainId,
                sub_domain = subDomain,
                record_type = recordType,
                record_line = recordLine,
                value = recordValue
            };
            recordId = Create(p);
            return recordId;
        }

        /// <summary>
        /// 创建记录,默认记录类型为A
        /// </summary>
        /// <param name="domainId">域名ID</param>
        /// <param name="subDomain">二级域名名称</param>
        /// <param name="recordValue">记录值</param>
        /// <param name="recordType">记录类型，通过API记录类型获得，大写英文，比如：A</param>
        /// <param name="recordLineId">记录线路ID，通过API记录线路获得，中文，比如：默认</param>
        /// <returns>记录ID</returns>
        public int Create(string domainName, string subDomain, string recordValue, string recordType = "A", string recordLineId = "0")
        {
            var recordId = 0;
            object p = new
            {
                domain = domainName,
                sub_domain = subDomain,
                record_type = recordType,
                record_line_id = recordLineId,
                value = recordValue
            };
            recordId = Create(p);
            return recordId;
        }

        /// <summary>
        /// 创建记录
        /// domain_id 域名ID, 必选
        /// sub_domain 主机记录, 如 www, 默认@，可选
        /// record_type 记录类型，通过API记录类型获得，大写英文，比如：A, 必选
        /// record_line 记录线路，通过API记录线路获得，中文，比如：默认, 必选
        /// value 记录值, 如 IP:200.200.200.200, CNAME: cname.dnspod.com., MX: mail.dnspod.com., 必选
        /// mx {1-20} MX优先级, 当记录类型是 MX 时有效，范围1-20, MX记录必选
        /// ttl {1-604800} TTL，范围1-604800，不同等级域名最小值不同, 可选
        /// </summary>
        /// <param name="paramObject"></param>
        /// <returns></returns>
        public int Create(object paramObject)
        {
            dynamic result = client.PostApiRequest("Record.Create", paramObject);
            return Convert.ToInt32(result.status.code) == 1 ? Convert.ToInt32(result.record.id) : -1;
        }

        #endregion

        #region 记录列表

        /// <summary>
        /// 记录列表
        /// </summary>
        /// <param name="domainId">域名ID</param>
        /// <returns></returns>
        public dynamic List(int domainId)
        {
            return List(new { domain_id = domainId });
        }

        public dynamic List(object paramObject)
        {
            return client.PostApiRequest("Record.List", paramObject);
        }

        #endregion

        #region 修改记录

        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="domainName">域名</param>
        /// <param name="recordId">记录ID</param>
        /// <param name="value">记录值</param>
        /// <param name="subDomain">主机记录（二级域名）</param>
        /// <param name="recordType">记录类型（默认为“A”）</param>
        /// <param name="recordLineId">记录线路（默认为“默认”）</param>
        /// <returns>操作是否成功</returns>
        public bool Modify(string domainName, int recordId, string value, string subDomain, string recordType = "A", string recordLineId = "0")
        {
            object p = new
            {
                domain = domainName,
                record_id = recordId,
                value,
                sub_domain = subDomain,
                record_type = recordType,
                record_line_id = recordLineId,
            };
            return Modify(p);

        }

        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="paramObject"></param>
        /// <returns>操作是否成功</returns>
        public bool Modify(object paramObject)
        {
            dynamic result = client.PostApiRequest("Record.Modify", paramObject);
            return Convert.ToInt32(result.status.code) == 1;
        }

        #endregion

        #region 删除记录

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="paramObject"></param>
        /// <returns></returns>
        public bool Remove(object paramObject)
        {
            dynamic result = client.PostApiRequest("Record.Remove", paramObject);
            return Convert.ToInt32(result.status.code) == 1;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="domainId">域名ID</param>
        /// <param name="recordId">记录ID</param>
        /// <returns></returns>
        public bool Remove(int domainId, int recordId)
        {
            return Remove(new { domain_id = domainId, record_id = recordId });
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="domainName">域名</param>
        /// <param name="recordId">记录ID</param>
        /// <returns></returns>
        public bool Remove(string domainName, int recordId)
        {
            return Remove(new { domain = domainName, record_id = recordId });
        }

        #endregion

        #region DDNS

        private bool Ddns(object paramObject)
        {
            dynamic result = client.PostApiRequest("Record.Ddns", paramObject);
            return Convert.ToInt32(result.status.code) == 1;
        }

        public bool Ddns(int domainId, int recordId, string subDomain, string value)
        {
            return Ddns(domainId, recordId, subDomain, "默认", value);
        }

        public bool Ddns(int domainId, int recordId, string subDomain, string recordLine, string value)
        {
            return Ddns(new
            {
                domain_id = domainId,
                record_id = recordId,
                sub_domain = subDomain,
                record_line = recordLine,
                value
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="recordId"></param>
        /// <param name="value"></param>
        /// <param name="record_line"></param>
        /// <param name="subDomain"></param>
        /// <returns></returns>
        public bool Ddns(string domainName, int recordId, string value, string subDomain = "", string recordLine = "默认")
        {
            if (!string.IsNullOrWhiteSpace(subDomain))
            {
                return Ddns(new
                {
                    domain = domainName,
                    record_id = recordId,
                    sub_domain = subDomain,
                    record_line = recordLine,
                    value
                });
            }
            else
            {
                return Ddns(new
                {
                    domain = domainName,
                    record_id = recordId,
                    record_line = recordLine,
                    value
                });
            }
        }


        #endregion

        #region 设置记录备注

        public bool Remark(int domainId, int recordId, string subDomainremark)
        {
            dynamic result = client.PostApiRequest("Record.Remark", new { });
            return Convert.ToInt32(result.status.code) == 1;
        }

        #endregion

        #region 获取记录信息

        public dynamic Info(int domainId, int recordId)
        {
            return Info(new
            {
                domain_id = domainId,
                record_id = recordId
            });
        }

        /// <summary>
        /// 获取记录信息
        /// </summary>
        /// <param name="paramObject"></param>
        /// <returns></returns>
        public dynamic Info(object paramObject)
        {
            return client.PostApiRequest("Record.Info", paramObject);
        }
        #endregion

    }
}
