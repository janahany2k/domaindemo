using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainApplication.Controllers
{
    [Route("api/[controller]")]
    public class DomainController : Controller
    {
        [HttpGet("subdomain/{enumerate}/{domain}")]
        public IEnumerable<string> Get(int enumerate, string domain)
        {
            try
            {
                const string alphabet = "abcdefghijklmnopqrstuvwxyz";
                const int size = 2;

                var subdomainList = alphabet.Select(x => x.ToString()).ToList();
                for (var i = 0; i < size - 1; i++)
                    subdomainList.AddRange(subdomainList.SelectMany(x => alphabet, (x, y) => x + y).ToList());

                var subdomains = new List<string>();

                for (var i = 0; i < enumerate; i++)
                {
                    subdomains.Add(string.Format("{0}.{1}", subdomainList.OrderBy(x => Guid.NewGuid()).FirstOrDefault(), domain));
                }

                return subdomains;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        [HttpGet("subdomain/findIPAddresses")]
        public async Task<Dictionary<string, string>> FindIpAddresses(List<string> subdomains)
        {
            try
            {
                var ipAddresses = new Dictionary<string, string>();

                foreach (var subdomain in subdomains)
                {
                    try
                    {
                        var addresses = await System.Net.Dns.GetHostAddressesAsync(subdomain);
                        var ipAddress = string.Join(",", addresses.Select(x => x.MapToIPv4().ToString()));
                        ipAddresses.Add(subdomain, ipAddress);
                    }
                    catch (Exception e)
                    {
                        ipAddresses.Add(subdomain, "-");
                    }
                }

                return ipAddresses;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
