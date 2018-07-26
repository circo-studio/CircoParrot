using Amazon.MediaConvert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CircoParrotTools.Web.Models
{
    public class JobMediaConvert
    {
        public string Id { get; set; }
        public string FileToConvert { get; set; }
        public string Status { get; set; }
        public string TimeStart { get; set; }
        public string TimeFinish { get; set; }
        public string Queue { get; set; }

        public JobMediaConvert(string id)
        {
            Id = id;    
        }

        public JobMediaConvert(Job j)
        {
            Id = j.Id;
            Status = j.Status;
            TimeStart = j.Timing.StartTime.ToString();
            TimeFinish = j.Timing.FinishTime.ToString();
            Queue = j.Queue;
            FileToConvert = j.Settings.Inputs[0].FileInput;
        }
    }
}
