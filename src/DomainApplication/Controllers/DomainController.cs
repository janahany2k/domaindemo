using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainApplication.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainApplication.Controllers
{public class DomainController : Controller
    {
        [HttpGet("subdomain/{enumerate}/{domain}")]
        public List<SubdomainModel> Get(int enumerate, string domain)
        {
            try
            {
                const string alphabet = "abcdefghijklmnopqrstuvwxyz";
                const int size = 2;

                var subdomainList = alphabet.Select(x => x.ToString()).ToList();
                for (var i = 0; i < size - 1; i++)
                    subdomainList.AddRange(subdomainList.SelectMany(x => alphabet, (x, y) => x + y).ToList());

                var subdomains = new List<SubdomainModel>();

                for (var i = 0; i < enumerate; i++)
                {
                    subdomains.Add(new SubdomainModel
                    {
                        Url = string.Format("{0}.{1}", subdomainList.OrderBy(x => Guid.NewGuid()).FirstOrDefault(), domain),
                        IpAddress = string.Empty
                    });
                }

                return subdomains;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        [HttpPost("subdomain/findIPAddresses")]
        public async Task<List<SubdomainModel>> FindIpAddresses([FromBody]List<SubdomainModel> subdomains)
        {
            try
            {
                foreach (var subdomain in subdomains)
                {
                    try
                    {
                        var addresses = await System.Net.Dns.GetHostAddressesAsync(subdomain.Url);
                        var ipAddress = string.Join(",", addresses.Select(x => x.MapToIPv4().ToString()));

                        subdomain.IpAddress = ipAddress;
                    }
                    catch (Exception ex)
                    {
                        subdomain.IpAddress = "-";
                    }
                }

                return subdomains;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
