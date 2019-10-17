using DNSPod4NETCore2.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DNSPod4NETCore2.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DDnsConfiguration configuration;
        private readonly DnsPodRecord record;
        private const string MyDDNS = "lishewen.vicp.net";
        public HomeController(IOptions<DDnsConfiguration> config, DnsPodRecord dnsPodRecord)
        {
            configuration = config.Value;
            record = dnsPodRecord;
        }
        public IActionResult Index()
        {
            return RedirectToAction("IP");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> IP()
        {
            ViewData["IP"] = await GetIPAsync();
            ViewData["DDNSIP"] = GetDDNSIP();
            return View();
        }
        [HttpGet("[controller]/[action]/{ip}")]
        public IActionResult EditIP(string ip)
        {
            var recordId = Convert.ToInt32(record.List(configuration.DomainName).records.FirstOrDefault(r => r.name == configuration.SubDomain)?.id);
            if (record.Modify(configuration.DomainName, recordId, ip, configuration.SubDomain))
                return Content("修改完成！");
            else
                return Content("修改失败！");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private static async Task<string> GetIPAsync()
        {
            HttpClient client = new HttpClient();

            string all = string.Empty;

            var res = await client.GetAsync("http://pv.sohu.com/cityjson?ie=utf-8");
            if (res.IsSuccessStatusCode)
                all = await res.Content.ReadAsStringAsync();

            Match rebool = Regex.Match(all, @"\d{2,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
            return rebool.Value;
        }

        private string GetDDNSIP()
        {
            IPHostEntry IPinfo = Dns.GetHostEntry(MyDDNS);
            return IPinfo.AddressList[0].ToString();
        }
    }
}
