using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GAB.ImageProcessingPipeline.Common.Storage
{
    public static class CloudBlobStorage
    {
        private static readonly CloudStorageAccount StorageAccount =
            CloudStorageAccount.Parse(AppSettings.BlobStorageConnectionString);
        
        public static void Init()
        {
            foreach (var name in BlobContainers.All)
            {
                InitBlobContainer(name);
            }
        }

        private static void InitBlobContainer(string name)
        {
            var container = GetBlobContainerReference(name);

            container.CreateIfNotExists();
            container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
        }

        public static string GetBaseUri()
        {
            var client = StorageAccount.CreateCloudBlobClient();
            return client.BaseUri.AbsoluteUri;
        }

        public static CloudBlobContainer GetBlobContainerReference(string name)
        {
            var client = StorageAccount.CreateCloudBlobClient();
            return client.GetContainerReference(name);
        }
        
        public static bool DeleteBlob(string containerName, string fileName)
        {
            var container = GetBlobContainerReference(containerName);
            var blob = container.GetBlockBlobReference(fileName);
            return blob.DeleteIfExists();
        }
    }
}
