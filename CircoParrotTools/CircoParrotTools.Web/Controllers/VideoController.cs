using Microsoft.AspNetCore.Mvc;
using CircoParrotTools.Common;
using System.Collections.Generic;
using CircoParrotTools.Common.AWS.Handlers;
using Amazon.S3.Model;

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

        public async System.Threading.Tasks.Task<IActionResult> ListaVideos(string bucket = "videosdeparrots")
        {
            List<S3Object> listaobj = await _s3Handler.listObjects(bucket);
            List<string> listaVideos = new List<string>();
            foreach (S3Object obj in listaobj)
            {
                listaVideos.Add(obj.Key);
            }

            ViewBag.Videos = listaVideos;
            ViewBag.UrlBase = "https://s3.amazonaws.com/" + bucket + "/";
            ViewBag.UrlActual = ViewData["urlBase"] + listaVideos[5];


            return View();
        }
    }
}
