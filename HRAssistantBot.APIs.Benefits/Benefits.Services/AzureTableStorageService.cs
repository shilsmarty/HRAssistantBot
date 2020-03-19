using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System;

namespace MS.CFE.FHL.Hack.SmartAssistant.APIs.Benefits.Services
{
    public class AzureTableStorageService<T> : IAzureTableStorageService<T> where T : TableEntity, new()
    {
        private readonly CloudTableClient _cloudTableClient;
        private string _tableName;
        public string TableName
        {
            get { return _tableName; }
            set
            {
                _tableName = value;
            }
        }
        public AzureTableStorageService(string connectionString, string tableName)
        {
            _cloudTableClient = GetCloudTableFromType(connectionString);
            _tableName = tableName;
        }
        private CloudTableClient GetCloudTableFromType(string storageConnectionString)
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(storageConnectionString);

            var requestOptions = new TableRequestOptions
            {
                RetryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(1), 3)
            };

            var cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            cloudTableClient.DefaultRequestOptions = requestOptions;
            return cloudTableClient;
        }
        public async Task<List<T>> GetAllAsync()
        {
            var query = new TableQuery<T>();
            var entities = new List<T>();
            TableContinuationToken token = null;

            do
            {
                CloudTable cloudTable = _cloudTableClient.GetTableReference(_tableName);
                TableQuerySegment<T> resultSegment = await cloudTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                entities.AddRange(resultSegment.Results);
            } while (token != null);

            return entities;
        }
        public async Task<T> GetAsync(string partitionKey, string rowKey)
        {
            var operation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            CloudTable cloudTable = _cloudTableClient.GetTableReference(_tableName);
            var tableResult = await cloudTable.ExecuteAsync(operation);
            return (T)tableResult.Result;
        }
    }
}





      


    