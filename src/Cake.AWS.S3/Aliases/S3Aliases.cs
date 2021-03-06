﻿#region Using Statements
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Annotations;

using Amazon.S3.Model;
#endregion



namespace Cake.AWS.S3
{
    /// <summary>
    ///  Contains Cake aliases for configuring Amazon Simple Storage Service
    /// </summary>
    [CakeAliasCategory("AWS")]
    [CakeNamespaceImport("Amazon")]
    [CakeNamespaceImport("Amazon.S3")]
    public static class S3Aliases
    {
        private static IS3Manager CreateManager(this ICakeContext context)
        {
            return new S3Manager(context.FileSystem, context.Environment, context.Log);
        }



        /// <summary>
        /// Syncs the specified directory to Amazon S3, checking the modified date of the local fiels with existing S3Objects.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="dirPath">The directory path to sync to S3</param>
        /// <param name="settings">The <see cref="SyncSettings"/> required to sync to Amazon S3.</param>
        /// <returns>A list of keys that require invalidating.</returns>
        [Obsolete("Use S3SyncUpload instead.")]
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<IList<string>> S3Sync(this ICakeContext context, DirectoryPath dirPath, SyncSettings settings)
        {
           return await context.CreateManager().SyncUpload(dirPath, settings);
        }
        

        
        /// <summary>
        /// Syncs the specified file to Amazon S3, checking the modified date of the local file with a existing S3Object and uploads it if its changes.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="filePath">The file path to sync to S3</param>
        /// <param name="settings">The <see cref="SyncSettings"/> required to sync to Amazon S3.</param>
        /// <returns>AThe key that require invalidating.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<string> S3SyncUpload(this ICakeContext context, FilePath filePath, SyncSettings settings)
        {
           return await context.CreateManager().SyncUpload(filePath, settings);
        }

        /// <summary>
        /// Syncs the specified directory to Amazon S3, checking the modified date of the local files with existing S3Objects and uploading them if its changes.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="dirPath">The directory path to sync to S3</param>
        /// <param name="settings">The <see cref="SyncSettings"/> required to sync to Amazon S3.</param>
        /// <returns>A list of keys that require invalidating.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<IList<string>> S3SyncUpload(this ICakeContext context, DirectoryPath dirPath, SyncSettings settings)
        {
           return await context.CreateManager().SyncUpload(dirPath, settings);
        }
               
        
         
        /// <summary>
        ///  Syncs the specified file from Amazon S3, checking the modified date of the local file with a existing S3Object and downloads it if its changed.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="filePath">The file path to sync to S3</param>
        /// <param name="settings">The <see cref="SyncSettings"/> required to sync to Amazon S3.</param>
        /// <returns>The key that require invalidating.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<string> S3SyncDownload(this ICakeContext context, FilePath filePath, SyncSettings settings)
        {
           return await context.CreateManager().SyncDownload(filePath, settings);
        }

        /// <summary>
        ///  Syncs the specified directory from Amazon S3, checking the modified date of the local files with existing S3Objects and downloading them if its changed.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="dirPath">The directory path to sync to S3</param>
        /// <param name="settings">The <see cref="SyncSettings"/> required to sync to Amazon S3.</param>
        /// <returns>A list of keys that require invalidating.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<IList<string>> S3SyncDownload(this ICakeContext context, DirectoryPath dirPath, SyncSettings settings)
        {
           return await context.CreateManager().SyncDownload(dirPath, settings);
        }



        /// <summary>
        /// Uploads the specified file. For large uploads, the file will be divided and uploaded in parts 
        /// using Amazon S3's multipart API. The parts will be reassembled as one object in Amazon S3.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="filePath">The file path of the file to upload.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="settings">The <see cref="UploadSettings"/> required to upload to Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task S3Upload(this ICakeContext context, FilePath filePath, string key, UploadSettings settings)
        {
            await context.CreateManager().Upload(filePath, key, settings);
        }

        /// <summary>
        /// Uploads the contents of the specified stream. For large uploads, the file will be divided and uploaded in parts 
        /// using Amazon S3's multipart API. The parts will be reassembled as one object in Amazon S3.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="stream">The stream to read to obtain the content to upload.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="settings">The <see cref="UploadSettings"/> required to upload to Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task S3Upload(this ICakeContext context, Stream stream, string key, UploadSettings settings)
        {
            await context.CreateManager().Upload(stream, key, settings);
        }



        /// <summary>
        /// Downloads the content from Amazon S3 and writes it to the specified file.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="filePath">The file path of the file to upload.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="settings">The <see cref="DownloadSettings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task S3Download(this ICakeContext context, FilePath filePath, string key, DownloadSettings settings)
        {
            await context.CreateManager().Download(filePath, key, "", settings);
        }

        /// <summary>
        /// Downloads the content from Amazon S3 and writes it to the specified file.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="filePath">The file path of the file to upload.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="version">The identifier for the specific version of the object to be downloaded, if required.</param>
        /// <param name="settings">The <see cref="DownloadSettings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task S3Download(this ICakeContext context, FilePath filePath, string key, string version, DownloadSettings settings)
        {
            await context.CreateManager().Download(filePath, key, version, settings);
        }
        


        /// <summary>
        /// Opens a stream of the content from Amazon S3.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="settings">The <see cref="DownloadSettings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<Stream> S3Open(this ICakeContext context, string key, DownloadSettings settings)
        {
            return await context.CreateManager().Open(key, "", settings);
        }

        /// <summary>
        /// Opens a stream of the content from Amazon S3.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="version">The identifier for the specific version of the object to be downloaded, if required.</param>
        /// <param name="settings">The <see cref="DownloadSettings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<Stream> S3Open(this ICakeContext context, string key, string version, DownloadSettings settings)
        {
            return await context.CreateManager().Open(key, version, settings);
        }
        

        
        /// <summary>
        /// Get the byte array of a S3 object
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="settings">The <see cref="DownloadSettings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<byte[]> GetS3Bytes(this ICakeContext context, string key, DownloadSettings settings)
        {
            return await context.GetS3Bytes(key, "", settings);
        }

        /// <summary>
        /// Get the byte array of a S3 object
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="version">The identifier for the specific version of the object to be downloaded, if required.</param>
        /// <param name="settings">The <see cref="DownloadSettings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<byte[]> GetS3Bytes(this ICakeContext context, string key, string version, DownloadSettings settings)
        {
            return await context.CreateManager().GetBytes(key, version, settings);
        }
                

        
        /// <summary>
        /// Get the string of a S3 object
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="settings">The <see cref="DownloadSettings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<string> GetS3String(this ICakeContext context, string key, DownloadSettings settings)
        {
            return await context.GetS3String(key, "", settings);
        }

        /// <summary>
        /// Get the string of a S3 object
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="version">The identifier for the specific version of the object to be downloaded, if required.</param>
        /// <param name="settings">The <see cref="DownloadSettings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<string> GetS3String(this ICakeContext context, string key, string version, DownloadSettings settings)
        {
            byte[] bytes = await context.CreateManager().GetBytes(key, version, settings);
            
            if (bytes != null)
            {
                return Encoding.UTF8.GetString(bytes);
            }
            else
            {
                return "";
            }
        }



        /// <summary>
        /// Removes the null version (if there is one) of an object and inserts a delete
        /// marker, which becomes the latest version of the object. If there isn't a null
        /// version, Amazon S3 does not remove any objects.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="settings">The <see cref="S3Settings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task S3Delete(this ICakeContext context, string key, S3Settings settings)
        {
            await context.S3Delete(key, "", settings);
        }

        /// <summary>
        /// Removes the null version (if there is one) of an object and inserts a delete
        /// marker, which becomes the latest version of the object. If there isn't a null
        /// version, Amazon S3 does not remove any objects.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="version">The identifier for the specific version of the object to be deleted, if required.</param>
        /// <param name="settings">The <see cref="S3Settings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task S3Delete(this ICakeContext context, string key, string version, S3Settings settings)
        {
            await context.CreateManager().Delete(key, version, settings);
        }
            
        
                        
        /// <summary>
        /// Removes all objects from the bucket
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="settings">The <see cref="S3Settings"/> required to delete from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<IList<string>> S3DeleteAll(this ICakeContext context, S3Settings settings)
        {
            return await context.CreateManager().DeleteAll("", DateTimeOffset.MinValue, settings);
        }

        /// <summary>
        /// Removes all objects from the bucket
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="prefix">Only delete objects that begin with the specified prefix.</param>
        /// <param name="settings">The <see cref="S3Settings"/> required to delete from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<IList<string>> S3DeleteAll(this ICakeContext context, string prefix, S3Settings settings)
        {
            return await context.CreateManager().DeleteAll(prefix, DateTimeOffset.MinValue, settings);
        }

        /// <summary>
        /// Removes all objects from the bucket
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="prefix">Only delete objects that begin with the specified prefix.</param>
        /// <param name="lastModified">Only delete objects that where modified prior to this date</param>
        /// <param name="settings">The <see cref="S3Settings"/> required to delete from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<IList<string>> S3DeleteAll(this ICakeContext context, string prefix, DateTimeOffset lastModified, S3Settings settings)
        {
            return await context.CreateManager().DeleteAll(prefix, lastModified, settings);
        }



        /// <summary>
        /// Retrieves object from Amazon S3.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="settings">The <see cref="S3Settings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<S3Object> GetS3Object(this ICakeContext context, string key, S3Settings settings)
        {
            return await context.CreateManager().GetObject(key, "", settings);
        }

        /// <summary>
        /// Retrieves object from Amazon S3.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="version">The identifier for the specific version of the object to be deleted, if required.</param>
        /// <param name="settings">The <see cref="S3Settings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<S3Object> GetS3Object(this ICakeContext context, string key, string version, S3Settings settings)
        {
            return await context.CreateManager().GetObject(key, version, settings);
        }


        
        /// <summary>
        /// Returns all the objects in a S3 bucket.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="settings">The <see cref="S3Settings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<IList<S3Object>> GetS3Objects(this ICakeContext context, S3Settings settings)
        {
            return await context.CreateManager().GetObjects("", settings);
        }

        /// <summary>
        /// Returns all the objects in a S3 bucket.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="prefix">Limits the response to keys that begin with the specified prefix.</param>
        /// <param name="settings">The <see cref="S3Settings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<IList<S3Object>> GetS3Objects(this ICakeContext context, string prefix, S3Settings settings)
        {
            return await context.CreateManager().GetObjects(prefix, settings);
        }



        /// <summary>
        /// Gets the last modified date of an S3 object
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="settings">The <see cref="S3Settings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<DateTimeOffset> S3LastModified(this ICakeContext context, string key, S3Settings settings)
        {
            return await context.S3LastModified(key, "", settings);
        }

        /// <summary>
        /// Gets the last modified date of an S3 object
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="version">The identifier for the specific version of the object to be deleted, if required.</param>
        /// <param name="settings">The <see cref="S3Settings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<DateTimeOffset> S3LastModified(this ICakeContext context, string key, string version, S3Settings settings)
        {
            S3Object result = await context.CreateManager().GetObject(key, version, settings);

            if (result != null)
            {
                return result.LastModified;
            }
            else
            {
                return DateTime.MinValue;
            }
        }


                
        /// <summary>
        /// Gets the ETag of an S3 object
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="settings">The <see cref="S3Settings"/> required to access Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<string> S3GetETag(this ICakeContext context, string key, S3Settings settings)
        {
            return await context.S3GetETag(key, "", settings);
        }

        /// <summary>
        /// Gets the ETag of an S3 object
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="version">The identifier for the specific version of the object to be deleted, if required.</param>
        /// <param name="settings">The <see cref="S3Settings"/> required to access Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<string> S3GetETag(this ICakeContext context, string key, string version, S3Settings settings)
        {
            S3Object result = await context.CreateManager().GetObject(key, version, settings);

            if (result != null)
            {
                return result.ETag;
            }
            else
            {
                return "";
            }
        }
              

                
        /// <summary>
        /// Gets the ETag of an S3 object
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="settings">The <see cref="S3Settings"/> required to access Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<string> S3GetHashTag(this ICakeContext context, string key, S3Settings settings)
        {
            return await context.S3GetHashTag(key, "", settings);
        }

        /// <summary>
        /// Gets the ETag of an S3 object
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="version">The identifier for the specific version of the object to be deleted, if required.</param>
        /// <param name="settings">The <see cref="S3Settings"/> required to access Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static async Task<string> S3GetHashTag(this ICakeContext context, string key, string version, S3Settings settings)
        {
            MetadataCollection result = await context.CreateManager().GetObjectMetaData(key, version, settings);

            if ((result != null) && result.Keys.Contains("x-amz-meta-hashtag"))
            {
                return result["x-amz-meta-hashtag"];
            }
            else
            {
                return "";
            }
        }
        
          
        /// <summary>
        /// Gets the hash of a file
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="path">The path to the file to calulate the hash from.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static string GetFileHash(this ICakeContext context, FilePath path)
        {
            IFile file = context.FileSystem.GetFile(path);

            return context.CreateManager().GetHash(file);
        }



        /// <summary>
        /// Generates a base64-encoded encryption key for Amazon S3 to use to encrypt / decrypt objects
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="filePath">The file path to store the key in.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static void GenerateEncryptionKey(this ICakeContext context, FilePath filePath)
        {
            context.CreateManager().GenerateEncryptionKey(filePath, 256);
        }
        
        /// <summary>
        /// Generates a base64-encoded encryption key for Amazon S3 to use to encrypt / decrypt objects
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="filePath">The file path to store the key in.</param>
        /// <param name="size">The size in bits of the secret key used by the symmetric algorithm</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static void GenerateEncryptionKey(this ICakeContext context, FilePath filePath, int size)
        {
            context.CreateManager().GenerateEncryptionKey(filePath, size);
        }



        /// <summary>
        /// Create a signed URL allowing access to a resource that would usually require authentication. cts
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="expires">The expiry date and time for the pre-signed url. </param>
        /// <param name="settings">The <see cref="S3Settings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static string GetPreSignedURL(this ICakeContext context, string key, DateTime expires, S3Settings settings)
        {
            return context.CreateManager().GetPreSignedURL(key, "", expires, settings);
        }

        /// <summary>
        /// Create a signed URL allowing access to a resource that would usually require authentication. cts
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="key">The key under which the Amazon S3 object is stored.</param>
        /// <param name="version">The identifier for the specific version of the object to be deleted, if required.</param>
        /// <param name="expires">The expiry date and time for the pre-signed url. </param>
        /// <param name="settings">The <see cref="S3Settings"/> required to download from Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("S3")]
        public static string GetPreSignedURL(this ICakeContext context, string key, string version, DateTime expires, S3Settings settings)
        {
            return context.CreateManager().GetPreSignedURL(key, version, expires, settings);
        }
    }
}
