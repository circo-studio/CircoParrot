using Amazon.MediaConvert.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CircoParrotTools.Common.AWS.Handlers
{
    public interface IMCHandler
    {

        Task<List<Job>> listJobs();
    }
}
