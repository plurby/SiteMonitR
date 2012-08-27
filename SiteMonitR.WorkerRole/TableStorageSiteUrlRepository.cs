using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SiteMonitR.WorkerRole
{
    public class TableStorageSiteUrlRepository : ISiteUrlRepository
    {
        private string _connectionStringName = "SiteMonitRConnectionString";
        private string _tableName = "sitemonitrurls";
        private CloudStorageAccount _storageAccount;
        private CloudTableClient _tableClient;
        private TableServiceContext _tableContext;

        public TableStorageSiteUrlRepository()
        {
            _storageAccount = CloudStorageAccount.Parse(
                RoleEnvironment.GetConfigurationSettingValue(_connectionStringName)
                );

            _tableClient = new CloudTableClient(_storageAccount.TableEndpoint.AbsoluteUri, _storageAccount.Credentials);
            _tableClient.RetryPolicy = RetryPolicies.Retry(3, TimeSpan.FromSeconds(1));
            _tableClient.CreateTableIfNotExist(_tableName);
            _tableContext = _tableClient.GetDataServiceContext();
        }

        public List<string> GetUrls()
        {
            var r = _tableContext.CreateQuery<StoredSiteUrl>(_tableName);
            return r.ToList().Select(x => x.Url).ToList();
        }

        public void Add(string url)
        {
            _tableContext.AddObject(_tableName, new StoredSiteUrl { Url = url });
            _tableContext.SaveChanges();
        }

        public void Remove(string url)
        {
            var o = _tableContext.CreateQuery<StoredSiteUrl>(_tableName).ToList().First(x => x.Url == url);
            _tableContext.DeleteObject(o);
            _tableContext.SaveChanges();
        }
    }
}
