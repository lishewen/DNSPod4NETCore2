using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace DNSPod4NETCore2.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DnsPod Cli Tools v1.0");

            var builder = new ConfigurationBuilder();
            builder.AddCommandLine(args, new Dictionary<string, string> {
                {"-t","Token" },
                {"-d","DomainName" },
                {"-r","RecordId" },
                {"-v","Value" },
                {"-s","SubDomain" },
                {"--type","RecordType" }
            });
            var item = builder.Build().Get<CommandLineArgs>();

            if (item == null)
            {
                Console.WriteLine("请输入参数");
                PrintMenu();
                return;
            }

            Console.WriteLine("正在修改域名记录:" + item.SubDomain + "." + item.DomainName);

            DnsPodConfiguration configuration = new DnsPodConfiguration
            {
                Token = item.Token
            };
            DnsPodClient client = new DnsPodClient(new HttpClient(), configuration);
            DnsPodRecord record = new DnsPodRecord(client);

            //如果用户没有输入RecordId，则通过List接口获取
            if (item.RecordId == 0)
            {
                item.RecordId = Convert.ToInt32(record.List(item.DomainName).records.FirstOrDefault(r => r.name == item.SubDomain)?.id);
            }

            if (record.Modify(item.DomainName, item.RecordId, item.Value, item.SubDomain, item.RecordType))
                Console.WriteLine("修改完成！");
            else
                Console.WriteLine("修改失败！");
        }

        private static void PrintMenu()
        {
            Console.WriteLine("--------");
            PrintArg("-t,--Token", "API Token.格式：[api_id],[api_token]");
            PrintArg("-d,--DomainName", "域名.如：lishewen.com");
            PrintArg("-r,--RecordId", "RecordId.不输入则通过Record.List接口查询返回，多一次请求");
            PrintArg("-v,--Value", "记录值.");
            PrintArg("-s,--SubDomain", "子域名.如：www");
            PrintArg("--type", "记录类型.如：A、TXT等。不输入则默认为A记录");
        }

        private static void PrintArg(string name, string introduction)
        {
            Console.WriteLine($"{name}  :     {introduction}");
        }
    }
}
