using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.CFE.FHL.Hack.SmartAssistant.APIs.Benefits.Services
{
    public interface IAzureTableStorageService<T> where T : TableEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(string partitionKey, string rowKey);
    }
}
