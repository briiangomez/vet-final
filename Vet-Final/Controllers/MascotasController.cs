using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vet_Data.Context;
using Vet_Data.Models;
using Vet_BLL;

namespace Veterinaria_UI.Controllers
{
    public class MascotasController : Controller
    {
        private ClienteBLL _clienteService = new ClienteBLL();
        private RazaBLL _razaService = new RazaBLL();
        private MascotaBLL _mascotaService = new MascotaBLL();
        // GET: Mascotas
        public ActionResult Index()
        {
            var mascota = _mascotaService.ObtenerMascotas();
            return View(mascota.ToList());
        }

        // GET: Mascotas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascota mascota = _mascotaService.ObtenerMascota(id.Value);
            if (mascota == null)
            {
                return HttpNotFound();
            }
            return View(mascota);
        }

        // GET: Mascotas/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(_clienteService.ObtenerClientes(), "Id", "Nombre");
            ViewBag.RazaId = new SelectList(_razaService.ObtenerRazas(), "ID", "Descripcion");
            return View();
        }

        // POST: Mascotas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                if (mascota.imagen != null)
                {
                    string pic = System.IO.Path.GetFileName(mascota.imagen.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/Content/Images"), pic);
                    mascota.imagen.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        mascota.imagen.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                    mascota.Foto = "/Content/Images/" + mascota.imagen.FileName;
                }
                _mascotaService.Alta(mascota);
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(_clienteService.ObtenerClientes(), "Id", "Nombre", mascota.ClienteId);
            ViewBag.RazaId = new SelectList(_razaService.ObtenerRazas(), "ID", "Descripcion", mascota.RazaId);
            return View(mascota);
        }

        // GET: Mascotas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascota mascota = _mascotaService.ObtenerMascota(id.Value);
            if (mascota == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(_clienteService.ObtenerClientes(), "Id", "Nombre", mascota.ClienteId);
            ViewBag.RazaId = new SelectList(_razaService.ObtenerRazas(), "ID", "Descripcion", mascota.RazaId);
            return View(mascota);
        }

        // POST: Mascotas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                if (mascota.imagen != null)
                {
                    string pic = System.IO.Path.GetFileName(mascota.imagen.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/Content/Images"), pic);
                    mascota.imagen.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        mascota.imagen.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                    mascota.Foto = "/Content/Images/" + mascota.imagen.FileName;
                }
                _mascotaService.Actualizar(mascota);
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(_clienteService.ObtenerClientes(), "Id", "Nombre", mascota.ClienteId);
            ViewBag.RazaId = new SelectList(_razaService.ObtenerRazas(), "ID", "Descripcion", mascota.RazaId);
            return View(mascota);
        }

        // GET: Mascotas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascota mascota = _mascotaService.ObtenerMascota(id.Value);
            if (mascota == null)
            {
                return HttpNotFound();
            }
            return View(mascota);
        }

        [HttpPost]
        public ActionResult GetRazaByName(string term)
        {
            try
            {
                var clientes = _razaService.ObtenerRazaByName(term).ToList();
                JsonResult jsonResult = new JsonResult
                {
                    Data = clientes,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                return jsonResult;
            }
            catch (Exception ex)
            {
                JsonResult jsonResult = new JsonResult
                {
                    Data = null,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                return jsonResult;
            }
        }

        public ActionResult MascotasPorCliente(int clienteID)
        {
            var mascotas = _clienteService.ObtenerMascotaCliente(clienteID);
            var result = (from s in mascotas
                          select new
                          {
                              id = s.ID,
                              name = s.Nombre
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _mascotaService.Borrar(id);
            return RedirectToAction("Index");
        }
    }
}
