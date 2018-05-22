using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CircoParrotTools.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CircoParrotTools.Web.Controllers
{
    public class FilesUploadController : Controller
    {
        // GET: FilesUpload
        public ActionResult Index()
        {
            // Cargamos los archivos de una carpeta temporal
            List<FilesUploadViewModel> filesVM = new List<FilesUploadViewModel>();

            var files = Directory.GetFiles(@"C:\temp");

            foreach (string item in files)
            {
                FilesUploadViewModel fi = new FilesUploadViewModel();
                fi.FileName = Path.GetFileName(item);
                fi.FileSize = new FileInfo(item).Length;

                filesVM.Add(fi);
            }

            return View("Index", filesVM);
        }

        // GET: FilesUpload/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FilesUpload/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FilesUpload/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FilesUpload/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FilesUpload/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FilesUpload/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FilesUpload/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}