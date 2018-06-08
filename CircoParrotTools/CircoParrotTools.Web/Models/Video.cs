using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CircoParrotTools.Web.Models
{
    public class Video
    {
        public string VideoName { get; set; }
        public string VideoKey { get; set; }
        public string VideoURL { get; set; }

        public Video(string videoName, string videoKey, string videoURL)
        {
            VideoName = videoName;
            VideoKey = videoKey;
            VideoURL = videoURL;
        }
    }
}
