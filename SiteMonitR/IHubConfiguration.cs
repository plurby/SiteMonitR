using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteMonitR
{
    public interface IHubConfiguration
    {
        string GetHubContainingSiteUrl();
        int GetPingTimeout();
    }
}
