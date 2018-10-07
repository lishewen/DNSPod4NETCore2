namespace DNSPod4NETCore2
{
    public class DnsPodConfiguration
    {
        /// <summary>
        /// API主机域名
        /// </summary>
        public string Host { get; set; } = "https://dnsapi.cn/";
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 用户帐号
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 返回的数据格式，可选，默认为xml，建议用json
        /// </summary>
        public string Format { get; set; } = "json";
        /// <summary>
        /// 返回的错误语言，可选，默认为en，建议用cn
        /// </summary>
        public string Lang { get; set; } = "cn";
        /// <summary>
        /// 没有数据时是否返回错误，可选，默认为yes，建议用no
        /// </summary>
        public string ErrorOnEmpty { get; set; } = "no";
        public string UserAgent { get; set; } = "DNSPodForNETCore2/V1.0 (me@lishewen.com)";
    }
}
