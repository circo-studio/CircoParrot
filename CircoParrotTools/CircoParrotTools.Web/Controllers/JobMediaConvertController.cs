using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.MediaConvert.Model;
using CircoParrotTools.Common.AWS.Handlers;
using CircoParrotTools.Web.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CircoParrotTools.Web.Controllers
{
    public class JobMediaConvertController : Controller
    {
        private readonly IMCHandler _mCHandler;

        public JobMediaConvertController(IMCHandler mCHandler)
        {
            _mCHandler = mCHandler;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            List<JobMediaConvert> listaJobs = new List<JobMediaConvert>();
            List<Job> lj = await _mCHandler.listJobs();
            foreach (Job j in lj)
            {                
                listaJobs.Add(new JobMediaConvert(j));   
            }
            return View(listaJobs);
        }
    }
}
