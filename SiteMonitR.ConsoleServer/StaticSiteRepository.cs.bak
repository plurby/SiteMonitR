using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteMonitor.ConsoleServer
{
    public class StaticSiteRepository : ISiteUrlRepository
    {
        // pretty basic example huh?
        List<string> _siteUrls;

        public StaticSiteRepository()
        {
            _siteUrls = new List<string>();
        }

        public List<string> GetUrls()
        {
            return _siteUrls;
        }

        public void Add(string url)
        {
            _siteUrls.Add(url);

            var clr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Site Added: " + url);
            Console.ForegroundColor = clr;
        }

        public void Remove(string url)
        {
            _siteUrls.Remove(url);

            var clr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Site Removed: " + url);
            Console.ForegroundColor = clr;
        }
    }
}
