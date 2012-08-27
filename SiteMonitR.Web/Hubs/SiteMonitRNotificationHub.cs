using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;

namespace SiteMonitR.Web.Hubs
{
    [HubName("SiteMonitR")]
    public class SiteMonitRNotificationHub : Hub
    {
        public void ServiceReady()
        {
            Clients.ServiceIsUp();
        }

        public void ReceiveMonitorUpdate(dynamic monitorUpdate)
        {
            Clients.notifySiteStatus(monitorUpdate);
        }

        public void AddSiteToGui(string url)
        {
            Clients.notifySiteAdded(url);
        }

        public void RemoveSiteFromGui(string url)
        {
            Clients.notifySiteRemoved(url);
        }

        public void AddSite(string url)
        {
            Clients.SiteAdded(url);
        }

        public void RemoveSite(string url)
        {
            Clients.SiteRemoved(url);
        }

        public void GetSiteList()
        {
            Clients.siteListRequested();
        }

        public void ListOfSitesObtained(List<string> urls)
        {
            Clients.sitesObtained(urls);
        }

        public void CheckSite(string url)
        {
            Clients.checkingSite(url);
        }
    }
}
