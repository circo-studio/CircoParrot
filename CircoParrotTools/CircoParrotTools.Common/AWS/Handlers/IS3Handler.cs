using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CircoParrotTools.Common.AWS.Handlers
{
    public interface IS3Handler
    {
        Task<List<String>> ListBuckets();
        Task<List<S3Object>> listObjects(String bucket);

    }
}
