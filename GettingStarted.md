# SiteMonitR Sample #

The SiteMonitR sample demonstrates how a Windows Azure Cloud Service and a Windows Azure Web Site can use SignalR to communicate in an asynchronous communication. The SiteMonitR Cloud Service pings all of the sites in a list of sites stored in Windows Azure Table Storage. As each site's status is obtained, it is sent over to a SignalR Hub housed in a Windows Azure Web Site. When the SignalR Hub receives notifications for a site, the Web Site's user interface is updated in real-time. The result is a web site monitoring application that provides up-to-date status of web sites, giving site administrators real-time data on how their sites are performing. 

## Prerequisites ##

* [Visual Studio 2012](http://www.microsoft.com/visualstudio/en-us/products) 
* [Windows Azure SDK for .NET 1.7](http://www.windowsazure.com/en-us/develop/net/)


## Running the Sample in Windows Azure ##


## MSDN Code Sample ###
This sample is also available on the [MSDN Windows Azure Code Samples site](http://code.msdn.microsoft.com/PhluffyFotos-Sample-7ecffd31). If you would like to download a ZIP file containing all the source code for the sample it is available [here](http://code.msdn.microsoft.com/PhluffyFotos-Sample-7ecffd31/file/63060/1/PhluffyFotos.zip). 