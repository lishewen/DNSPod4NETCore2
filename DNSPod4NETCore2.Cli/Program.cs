using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

            Console.WriteLine("正在修改域名记录:" + item.Token);

            DnsPodConfiguration configuration = new DnsPodConfiguration
            {
                Token = item.Token
            };
            DnsPodClient client = new DnsPodClient(new HttpClient(), configuration);
            DnsPodRecord record = new DnsPodRecord(client);
            if (record.Modify(item.DomainName, item.RecordId, item.Value, item.SubDomain, item.RecordType))
                Console.WriteLine("修改完成！");
            else
                Console.WriteLine("修改失败！");

            //Console.ReadLine();
        }
    }
}
