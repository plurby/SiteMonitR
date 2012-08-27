using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace SiteMonitor.WorkerRole
{
    public class StoredSiteUrl
        : TableServiceEntity
    {
        public StoredSiteUrl()
        {
            this.PartitionKey = "default";
            this.RowKey = Guid.NewGuid().ToString();
            this.Timestamp = DateTime.Now;
        }

        public string Url { get; set; }
    }
}
