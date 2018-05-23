using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace ControladorS3
{



    public static class S3Handler
    {

        public static List<String> listBuckets(String accessKey, String secretAccessKey)
        {
            try
            {
                IAmazonS3 client = new AmazonS3Client(accessKey, secretAccessKey);

                ListBucketsResponse response = client.ListBucketsAsync().Result;
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

        public static List<S3Object> listObjects(String bucket)
        {
            try
            {
                IAmazonS3 client = new AmazonS3Client();

                ListObjectsRequest request = new ListObjectsRequest();
                request.BucketName = bucket;
                ListObjectsResponse response = client.ListObjectsAsync(request).Result;
                return response.S3Objects;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                throw amazonS3Exception;
            }
        }

        public static void createBucket(String bucketName)
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

        public static void uploadObject(String bucketName, String path, String fileName)
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

                PutObjectResponse response = client.PutObjectAsync(request).Result;
               
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
