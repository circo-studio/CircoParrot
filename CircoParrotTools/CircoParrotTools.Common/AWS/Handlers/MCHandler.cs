using Amazon.MediaConvert;
using Amazon.MediaConvert.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CircoParrotTools.Common.AWS.Handlers
{
    class MCHandler
    {
        public async Task<List<Job>> ListJobs()
        {
            String mediaConvertEndpoint = "";
            if (String.IsNullOrEmpty(mediaConvertEndpoint))
            {
                // Obtain the customer-specific MediaConvert endpoint
                AmazonMediaConvertClient client = new AmazonMediaConvertClient(Amazon.RegionEndpoint.USEast1);
                DescribeEndpointsRequest describeRequest = new DescribeEndpointsRequest();
                DescribeEndpointsResponse describeResponse = await client.DescribeEndpointsAsync(describeRequest);
                mediaConvertEndpoint = describeResponse.Endpoints[0].Url;
            }
            AmazonMediaConvertConfig mcConfig = new AmazonMediaConvertConfig
            {
                ServiceURL = mediaConvertEndpoint
            };
            String key = "";
            String secret = "";
            try
            {
                using (StreamReader sr = new StreamReader("c:\\temp\awskey.txt"))
                {
                    key = sr.ReadLine();
                    secret = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not read the file");
            }


            AmazonMediaConvertClient mcClient = new AmazonMediaConvertClient(key, secret, mcConfig);
            ListJobsRequest req = new ListJobsRequest();
            ListJobsResponse listJobs = await mcClient.ListJobsAsync(req);
            return listJobs.Jobs;
        }
    }
}
