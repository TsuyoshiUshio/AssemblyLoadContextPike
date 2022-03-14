using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.Cosmos;
using PluginBase;

namespace BindingRedirectPlugin
{
    public class BindingRedirectsPlugin : ICommand
    {
        public string Name { get => "redirect"; }

        public string Description { get => "Binding Redirect Testing."; }

        public int Execute()
        {
            Console.WriteLine("See if the binding redirect happens on the framework.");
            ExecuteAsync().GetAwaiter().GetResult();
            return 0;
        }

        public async Task ExecuteAsync()
        {
            await ListBlobAsync();
            await ListComosDBAsync();
        }

        private async Task ListBlobAsync()
        {
            // This code is for sample. Should cache the client for production.
            string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
            string containerName = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONTAINER_NAME");

            var blobClient = new BlobServiceClient(connectionString);
            var containerClient = blobClient.GetBlobContainerClient(containerName);
            Console.WriteLine("Connected Blob storage");
            await foreach(BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                Console.WriteLine($"\t{blobItem.Name}");
            }
        }

        private async Task ListComosDBAsync()
        {
            string connectionString = Environment.GetEnvironmentVariable("COSMOS_DB_CONNECTION_STRING");
            string databaseName = Environment.GetEnvironmentVariable("COSMOS_DB_DATABASE_NAME");
            var cosmosClient = new CosmosClient(connectionString);
            var database = cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
            Console.WriteLine($"Connected CosmosDB: {database.Id}");            
        }


    }
}
