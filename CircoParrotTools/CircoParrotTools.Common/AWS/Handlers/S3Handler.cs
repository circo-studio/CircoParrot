using Amazon.S3;
using Amazon.S3.Model;
using CircoParrotTools.Common.AWS.Handlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CircoParrotTools.Common.AWS.Handlers
{
    public class S3Handler : IS3Handler
    {
        public S3Handler()
        {
        }

        /*
private static bool comprobarCampos()
{
   IAmazonS3 client = new AmazonS3Client();
   //NameValueCollection appConfig = ConfigurationManager.AppSettings;

   if (string.IsNullOrEmpty(appConfig["AWSProfileName"]))
   {
       throw new Exception("AWSProfileName was not set in the App.config file.");
   }
   return true;
}
*/
        public async Task<List<String>> ListBuckets()
        {
            try
            {
                //Configurar autenticacion
                IAmazonS3 client = new AmazonS3Client("AKIAIYIYEIUSITOE3FUQ", "2bS3qTyjxyves/zeqR+gmiERhxGvU1fv2xWvCjrg", Amazon.RegionEndpoint.USEast1);

                ListBucketsResponse response = await client.ListBucketsAsync();
                List<String> list = new List<string>();
                foreach (S3Bucket bucket in response.Buckets)
                {
                    list.Add(bucket.BucketName);
                }
                return list;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                throw amazonS3Exception;
            }
        }

        public async Task<List<S3Object>> listObjects(String bucket)
        {
            try
            {
                //Configurar autenticacion
                IAmazonS3 client = new AmazonS3Client("AKIAIYIYEIUSITOE3FUQ", "2bS3qTyjxyves/zeqR+gmiERhxGvU1fv2xWvCjrg", Amazon.RegionEndpoint.USEast1);

                ListObjectsRequest request = new ListObjectsRequest();
                request.BucketName = bucket;
                ListObjectsResponse response = await client.ListObjectsAsync(request);
                return response.S3Objects;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                throw amazonS3Exception;
            }
        }

        public void createBucket(String bucketName)
        {
            try
            {
                IAmazonS3 client = new AmazonS3Client();

                PutBucketRequest request = new PutBucketRequest();
                request.BucketName = bucketName;
                client.PutBucketAsync(request);
                return;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                throw amazonS3Exception;
            }
        }

        public static async Task<PutObjectResponse> uploadObject(String bucketName, String path, String fileName)
        {
            try
            {
                IAmazonS3 client = new AmazonS3Client();

                PutObjectRequest request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = fileName,
                    FilePath = path
                };
                // titledRequest.Metadata.Add("title", "the title");

                PutObjectResponse response = await client.PutObjectAsync(request);
                return response;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                throw amazonS3Exception;
            }
        }
        //Delet this
        public static void deletObject(String bucketName, String objectName)
        {
            try
            {
                IAmazonS3 client = new AmazonS3Client();

                DeleteObjectRequest request = new DeleteObjectRequest()
                {
                    BucketName = bucketName,
                    Key = objectName
                };
                client.DeleteObjectAsync(request);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                throw amazonS3Exception;
            }
        }
    }

}
