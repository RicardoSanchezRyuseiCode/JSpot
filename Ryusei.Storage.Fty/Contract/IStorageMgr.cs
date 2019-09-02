using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Storage.Fty.Contract
{
    /// <summary>
    /// Name: IStorageBlobMgr
    /// Description: Interface to define the behavior of IStorageBlobMgr
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@faurecia.com)
    /// LogBook:
    ///     23-08-2019: Creation
    /// </summary>
    public interface IStorageBlobMgr
    {
        /// <summary>
        /// Name: Upload
        /// Description: Method to upload a file to a storage
        /// </summary>
        /// <param name="containerName">ContainerName</param>
        /// <param name="resourceStream">ResourceStream</param>
        /// <returns>ResourceId</returns>
        string Upload(string containerName, Stream resourceStream);
        /// <summary>
        /// Name: Download
        /// Description: Method to download a file from storage
        /// </summary>
        /// <param name="containerName">ContainerName</param>
        /// <param name="fileId">FileId</param>
        /// <returns>Resource Stream</returns>
        Stream Download(string containerName, string fileId);
        /// <summary>
        /// Name: Delete
        /// Description: Method to delete a file from storage
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="fileId"></param>
        void Delete(string containerName, string fileId);
    }
}
