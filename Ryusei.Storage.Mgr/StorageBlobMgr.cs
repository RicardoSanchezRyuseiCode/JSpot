using Ryusei.Storage.Fty.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ryusei.Storage.Mgr
{
    /// <summary>
    /// Name: StorageBlobMgr
    /// Description: Manager to implement the behavior of IStorageBlobMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-23: Creation
    /// </summary>
    public class StorageBlobMgr : IStorageBlobMgr
    {
        #region [Constants]
        #endregion

        #region [Static Attributes]
        /// <summary>
        /// Singleton attributes
        /// </summary>
        private static StorageBlobMgr Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// ConnectionString
        /// </summary>
        private string ConnectionString { get; set; }
        /// <summary>
        /// Named Mutex
        /// </summary>
        private Mutex Mutex { get; set; }
        /// <summary>
        /// RootPath
        /// </summary>
        private string RootPath { get; set; }
        #endregion

        #region [Static Constructor]
        /// <summary>
        /// Static constructor
        /// </summary>
        static StorageBlobMgr()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        private StorageBlobMgr()
        {
            // Assign connection string
            this.ConnectionString = ConfigurationManager.AppSettings["STORAGE::ConnectionString"];
            // Assign rootpath
            this.RootPath = ConfigurationManager.AppSettings["STORAGE::RootPath"];
            // Create named mutex
            bool created;
            this.Mutex = new Mutex(false, "JSpot Storage Mutex", out created);
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: 
        /// </summary>
        /// <returns></returns>
        public static StorageBlobMgr GetInstance()
        {
            return Singleton ?? (Singleton = new StorageBlobMgr());
        }
        #endregion

        #region [Methods]

        /// <summary>
        /// Name: Upload
        /// Description: Method to upload a file to a storage
        /// </summary>
        /// <param name="containerName">ContainerName</param>
        /// <param name="resourceStream">ResourceStream</param>
        /// <returns>ResourceId</returns>
        public string Upload(string containerName, Stream resourceStream)
        {
            Mutex.WaitOne();
            string root = System.IO.Path.Combine(this.RootPath, containerName);
            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);
            string resourceId = Guid.NewGuid().ToString();
            using (System.IO.FileStream output = new System.IO.FileStream(Path.Combine(root, resourceId), FileMode.Create))
            {
                resourceStream.CopyTo(output);
            }
            Mutex.ReleaseMutex();
            return resourceId;
        }
        /// <summary>
        /// Name: Download
        /// Description: Method to download a file from storage
        /// </summary>
        /// <param name="containerName">ContainerName</param>
        /// <param name="fileId">FileId</param>
        /// <returns>Resource Stream</returns>
        public Stream Download(string containerName, string fileId)
        {
            Mutex.WaitOne();
            string path = System.IO.Path.Combine(this.RootPath, containerName, fileId);
            Stream stream = new MemoryStream();
            using (System.IO.FileStream fileStream = new System.IO.FileStream(path, FileMode.Open))
            {
                fileStream.CopyTo(stream);
            }
            Mutex.ReleaseMutex();
            return stream;
        }
        /// <summary>
        /// Name: Delete
        /// Description: Method to delete a file from storage
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="fileId"></param>
        public void Delete(string containerName, string fileId)
        {
            Mutex.WaitOne();
            string path = System.IO.Path.Combine(this.RootPath, containerName, fileId);
            System.IO.File.Delete(path);
            Mutex.ReleaseMutex();
        }
        #endregion
    }
}
