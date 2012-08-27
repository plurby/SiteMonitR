# SiteMonitR Sample #

The SiteMonitR sample demonstrates how a Windows Azure Cloud Service and a Windows Azure Web Site can use SignalR to communicate in an asynchronous communication. The SiteMonitR Cloud Service pings all of the sites in a list of sites stored in Windows Azure Table Storage. As each site's status is obtained, it is sent over to a SignalR Hub housed in a Windows Azure Web Site. When the SignalR Hub receives notifications for a site, the Web Site's user interface is updated in real-time. The result is a web site monitoring application that provides up-to-date status of web sites, giving site administrators real-time data on how their sites are performing. 

### Prerequisites

* [Visual Studio 2012](http://www.microsoft.com/visualstudio/en-us/products) 
* [Windows Azure SDK for .NET 1.7](http://www.windowsazure.com/en-us/develop/net/)

### Running the Sample Locally
To get the site and cloud service running locally on a development workstation execute the following steps. 

1. Open Visual Studio 2012 as an Administrator (the Windows Azure SDK requires elevated priveleges to run the Windows Azure Compute and Storage emulators)
1. Open the SiteMonitR.sln solution
1. Press the F5 key to run the Cloud Service and Web Site simultaneously
1. Use the HTML form to add a URL (include the "http://" in the text box)
1. Observe as the site is monitored
1. Add any additional sites and observe how they are monitored in real time


### Running the Sample in Windows Azure
To get the site and cloud service running in Windows Azure, execute the following steps. A more comprehensive walk-through on setting up the entire application, see the [Getting Started](https://github.com/WindowsAzure-Samples/SiteMonitR/blob/master/GettingStarted.md) document for this sample. 

1. Log in to the Windows Azure portal. 
1. Create a new storage account to be used by the application.
1. Create a new Cloud Service to use as the background service for the SiteMonitR application.
1. Create a new Web Site to use to as the front-end web site for the SiteMonitR application.
1. Go into the dashboard for the new storage account you created and click the **Manage Keys** button at the bottom of the portal. Copy the storage account's key to the clipboard.
1. In Visual Studio 2012, expand the **SiteMonitR.Azure** project's **Roles** node. Double-click the **SiteMonitR.WorkerRole** node to open up the role's settings pane.
1. Select the **Cloud** option from the **Service Configuration** drop-down menu. 
1. Click the ellipse button next to the **SiteMonitRConnectionString** setting. 
1. Enter in the storage account name and primary access key copied from the portal.
1. Click the OK button.
1. Repeat the same steps to set the **Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString** setting.
1. Change the **GUI_URL** setting to reflect the URL of the Windows Azure Web Site you created using the Windows Azure portal. 
1. Right-click the **SiteMonitR.Azure** project and select the **Publish** menu item from the context menu.
1. If you haven't yet imported your publish settings, click the **Sign in to download credentials** link in the publish dialog. 
1. Your web browser will open up and browse to the Windows Azure publish profile download page. When the page tries to download the publish settings file, click the **Save** button to save the file to your local workstation.
1. Go back to Visual Studio 2012. Click the **Import** button in the publish dialog. Then, browse to the publish settings file you just downloaded and select it. 
1. Click the **Publish** button to deploy the Cloud Service to Windows Azure.
1. The **Windows Azure Activity Log** window should open to display the Cloud Service's publishing process happening.
1. Go back to the Windows Azure portal. Click the web site you just created to load the site's dashboard page. 
1. Once the site's dashboard loads in the browser, click the **Download publish profile** link.
1. When the browser tries to download the file, save it to your local workstation. 
1. Right-click the **SiteMonitR.Web** project in Visual Studio 2012. Then select the **Publish** menu item from the context menu.
1. Click the **Import** button on the publish dialog. Then, find the web site publish settings file downloaded from the Windows Azure portal.
1. Click the **Publish** button in the dialog to publish the web site to Windows Azure.
1. Once the site has been published, go back to the site's dashboard page in the Windows Azure portal. Click the **Configure** tab. 
1. Change the default document from **Default.htm** to **Default.html**. Then delete the other options from the list of default pages. Then, click the **Save** button to save the site configuration.
1. Click the **Browse** button at the bottom of the web site's dashboard to browse the site.
1. The site will open and present you with a simple form you can use to provide URL's of sites you'd like to monitor. 
1. Type in a site URL and click the **Add Site** button. The site will be added to the list of sites you are monitoring. 
1. Add in as many sites as you would like. All of the sites are monitored by the Cloud Service. Their status will update in real-time as the sites are hit by the service and reported in the web site. To remove a site, click the X button and the site will be removed from the list of sites monitored by the application. 

### Detailed Instructions ###
For a more detailed walk-through on how to perform each of the individual tasks required to get this sample working in Windows Azure, see the sample's [Getting Started](https://github.com/WindowsAzure-Samples/SiteMonitR/blob/master/GettingStarted.md) document in the repository containing the code for the sample. 