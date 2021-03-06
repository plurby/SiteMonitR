﻿// ---------------------------------------------------------------------------------- 
// Microsoft Developer & Platform Evangelism 
//  
// Copyright (c) Microsoft Corporation. All rights reserved. 
//  
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,  
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES  
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
// ---------------------------------------------------------------------------------- 
// The example companies, organizations, products, domain names, 
// e-mail addresses, logos, people, places, and events depicted 
// herein are fictitious.  No association with any real company, 
// organization, product, domain name, email address, logo, person, 
// places, or events is intended or should be inferred. 
// ---------------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;
using Microsoft.WindowsAzure.ServiceRuntime;
using SignalR.Client.Hubs;

namespace SiteMonitR
{
    public class Server
    {
        Timer _timer;
        HubConnection _connection;
        IHubProxy _hub;
        ISiteUrlRepository _siteRepository;
        IHubConfiguration _hubConfiguration;

        public Server(ISiteUrlRepository siteRepository,
            IHubConfiguration hubConfiguration)
        {
            _siteRepository = siteRepository;
            _hubConfiguration = hubConfiguration;
        }

        private void Log(string log)
        {
            Console.WriteLine(log, "Information");
            Trace.WriteLine(log, "Information");
        }

        public void Stop()
        {
            if (_timer != null)
                _timer.Stop();
        }

        public void Run()
        {
            if (_timer != null && _timer.Enabled)
                return;

            var url = _hubConfiguration.GetHubContainingSiteUrl();

            try
            {
                if (_connection == null)
                {
                    // connect to the hub
                    _connection = new HubConnection(url);

                    // create a proxy
                    _hub = _connection.CreateProxy("SiteMonitR");

                    // whenever a site is added
                    _hub.On<string>("siteAddedToStorage", (siteUrl) =>
                        {
                            _siteRepository.Add(siteUrl);
                            _hub.Invoke("addSiteToGui", siteUrl);
                        });

                    // whenever a site is removed
                    _hub.On<string>("siteRemovedFromStorage", (siteUrl) =>
                        {
                            _siteRepository.Remove(siteUrl);
                            _hub.Invoke("removeSiteFromGui", siteUrl);
                        });

                    // whenever the list of sites is requested
                    _hub.On("siteListRequested", () =>
                        {
                            var sites = _siteRepository.GetUrls();
                            _hub.Invoke("listOfSitesObtained", sites);
                        });

                    // now start the connection
                    _connection.Start().ContinueWith((t) =>
                        {
                            _hub.Invoke("serviceReady");
                        });
                }

                _timer = new Timer(_hubConfiguration.GetPingTimeout());

                _timer.Elapsed += (s, e) =>
                {
                    _timer.Stop();

                    _siteRepository.GetUrls()
                       .ForEach(x =>
                       {
                           var result = false;

                           try
                           {
                               // inform the client the site is being checked
                               _hub.Invoke("checkSite", x);

                               // check the site
                               var output = new WebClient().DownloadString(x);
                               result = true;
                               Log(x + " is up");
                           }
                           catch
                           {
                               result = false;
                               Log(x + " is down");
                           }

                           // invoke a method on the hub
                           _hub.Invoke("receiveMonitorUpdate", new
                           {
                               Url = x,
                               Result = result
                           });
                       });

                    _timer.Start();

                    Log("All sites pinged. Sleeping...");
                };

                _timer.Start();
            }
            catch
            {
                if (_timer != null)
                    _timer.Stop();
            }
        }
    }
}
