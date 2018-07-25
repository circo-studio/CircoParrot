using Amazon.MediaConvert;
using Amazon.MediaConvert.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace CircoParrotTools.Common.AWS.Handlers
{
    class MCHandler
    {
        

        public async Task<List<Job>> listObjects(String bucket)
        {
            AmazonMediaConvertClient client = new AmazonMediaConvertClient("AKIAIYIYEIUSITOE3FUQ", "2bS3qTyjxyves/zeqR+gmiERhxGvU1fv2xWvCjrg", Amazon.RegionEndpoint.USEast1);
            ListJobsRequest req = new ListJobsRequest();
            ListJobsResponse listJobs = await client.ListJobsAsync(req);
            return listJobs.Jobs;
        }



    }
}
