using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TencentCloud.Common.Profile;
using TencentCloud.Common;
using TencentCloud.Dnspod.V20210323.Models;
using TencentCloud.Dnspod.V20210323;

namespace DNSPod4NETCore2.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DnsPod Cli Tools v1.2");

            var builder = new ConfigurationBuilder();
            builder.AddCommandLine(args, new Dictionary<string, string> {
                {"--secretid","SecretId" },
                {"--secretkey","SecretKey" },
                {"-d","Domain" },
                {"-r","RecordId" },
                {"-v","Value" },
                {"-s","SubDomain" },
                {"--type","RecordType" },
                {"--line","RecordLine" }
            });
            var item = builder.Build().Get<CommandLineArgs>();

            if (item == null)
            {
                Console.WriteLine("请输入参数");
                PrintMenu();
                return;
            }

            Console.WriteLine("正在修改域名记录:" + item.SubDomain + "." + item.Domain);

            try
            {
                // 实例化一个认证对象，入参需要传入腾讯云账户 SecretId 和 SecretKey，此处还需注意密钥对的保密
                // 代码泄露可能会导致 SecretId 和 SecretKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考，建议采用更安全的方式来使用密钥，请参见：https://cloud.tencent.com/document/product/1278/85305
                // 密钥可前往官网控制台 https://console.cloud.tencent.com/cam/capi 进行获取
                Credential cred = new()
                {
                    SecretId = item.SecretId,
                    SecretKey = item.SecretKey,
                };
                // 实例化一个client选项，可选的，没有特殊需求可以跳过
                ClientProfile clientProfile = new();
                // 实例化一个http选项，可选的，没有特殊需求可以跳过
                HttpProfile httpProfile = new()
                {
                    Endpoint = ("dnspod.tencentcloudapi.com")
                };
                clientProfile.HttpProfile = httpProfile;

                // 实例化要请求产品的client对象,clientProfile是可选的
                DnspodClient client = new(cred, "", clientProfile);
                // 实例化一个请求对象,每个接口都会对应一个request对象
                ModifyRecordRequest req = new()
                {
                    Domain = item.Domain,
                    SubDomain = item.SubDomain,
                    RecordType = item.RecordType,
                    RecordLine = string.IsNullOrWhiteSpace(item.RecordLine) ? "默认" : item.RecordLine,
                    Value = item.Value,
                    RecordId = (ulong?)item.RecordId
                };
                // 返回的resp是一个ModifyRecordResponse的实例，与请求对象对应
                ModifyRecordResponse resp = client.ModifyRecordSync(req);
                // 输出json格式的字符串回包
                Console.WriteLine(AbstractModel.ToJsonString(resp));
                Console.WriteLine("修改完成！");
            }
            catch (Exception e)
            {
                Console.WriteLine("修改失败！");
                Console.WriteLine(e.ToString());
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("--------");
            PrintArg("--secretid", "SecretId");
            PrintArg("--secretkey", "SecretKey");
            PrintArg("-d,--Domain", "域名.如：lishewen.com");
            PrintArg("-r,--RecordId", "RecordId.不输入则通过Record.List接口查询返回，多一次请求");
            PrintArg("-v,--Value", "记录值.");
            PrintArg("-s,--SubDomain", "子域名.如：www");
            PrintArg("--type", "记录类型.如：A、TXT等。不输入则默认为A记录");
            PrintArg("--line", "记录线路，通过 API 记录线路获得，中文，比如：默认");
        }

        private static void PrintArg(string name, string introduction)
        {
            Console.WriteLine($"{name}  :     {introduction}");
        }
    }
}
