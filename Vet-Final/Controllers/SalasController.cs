using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vet_Data.Context;
using Vet_Data.Models;
using Vet_BLL;

namespace Veterinaria_UI.Controllers
{
    public class salasController : Controller
    {
        private SalaBLL _salaService = new SalaBLL();


        // GET: salas
        public ActionResult Index()
        {
            return View(_salaService.ObtenerSalas().ToList());
        }

        // GET: salas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sala sala = _salaService.ObtenerSala(id.Value);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        // GET: salas/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Sala sala)
        {
            if (ModelState.IsValid)
            {
                _salaService.Alta(sala);
                return RedirectToAction("Index");
            }

            return View(sala);
        }

        // GET: salas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sala sala = _salaService.ObtenerSala(id.Value);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        [HttpPost]
        public ActionResult Edit(Sala sala)
        {
            if (ModelState.IsValid)
            {
                _salaService.Actualizar(sala);
                return RedirectToAction("Index");
            }
            return View(sala);
        }

        // GET: salas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sala sala = _salaService.ObtenerSala(id.Value);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        // POST: salas/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _salaService.Borrar(id);
            return RedirectToAction("Index");
        }
    }
}
