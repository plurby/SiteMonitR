using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.ServiceRuntime;
using SiteMonitR.WorkerRole.Properties;

namespace SiteMonitR.WorkerRole
{
    public class WorkerRoleHubConfiguration : IHubConfiguration
    {
        public string GetHubContainingSiteUrl()
        {
            return RoleEnvironment.GetConfigurationSettingValue("GUI_URL");
        }

        public int GetPingTimeout()
        {
            return Settings.Default.PingTimeout;
        }
    }
}
