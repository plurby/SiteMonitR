using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteMonitR
{
    public interface ISiteUrlRepository
    {
        List<string> GetUrls();
        void Add(string url);
        void Remove(string url);
    }
}
