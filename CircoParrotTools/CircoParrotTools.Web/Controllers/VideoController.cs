using Microsoft.AspNetCore.Mvc;
using CircoParrotTools.Common;
using System.Collections.Generic;
using CircoParrotTools.Common.AWS.Handlers;
using Amazon.S3.Model;
using CircoParrotTools.Web.Models;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CircoParrotTools.Web.Controllers
{
    public class VideoController : Controller
    {
        private readonly IS3Handler _s3Handler;

        public VideoController(IS3Handler s3Handler)
        {
            _s3Handler = s3Handler;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListaVideos(string bucket = "videosdeparrots")
        {
            List<S3Object> listaobj = await _s3Handler.listObjects(bucket);
            List<Video> listaVideos = new List<Video>();
            string name, url, path , urlbase = "https://s3.amazonaws.com/" + bucket + "/";
            foreach (S3Object obj in listaobj)
            {
                if (!obj.Key.EndsWith("/")) {
                    path = obj.Key;
                    url = urlbase + path;
                    name = path.Split("/")[path.Split("/").Length-1];
                    Video v = new Video(name, path, url);
                    listaVideos.Add(v);
                }
            }

            //ViewBag.Videos = listaVideos;
            return View(listaVideos);
        }
    }
}
