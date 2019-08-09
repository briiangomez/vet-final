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
    public class MedicosController : Controller
    {
        private MedicoBLL _medicoService = new MedicoBLL();
        private EspecialidadBLL _especialidadService = new EspecialidadBLL();
        // GET: Medicos
        public ActionResult Index()
        {
            var medico = _medicoService.ObtenerMedicos();
            return View(medico.ToList());
        }

        // GET: Medicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = _medicoService.ObtenerMedico(id.Value);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        public ActionResult Create()
        {
            Medico model = new Medico();
            model.Especialidades = new List<MedicoEspecialidad>();
            model.Horarios = new List<HorarioMedico>();
            ViewBag.EspecialidadesList = new SelectList(_especialidadService.ObtenerEspecialidads(), "ID", "Descripcion");
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Medico model)
        {
            if (model.Especialidades.Count == 0)
                ModelState.AddModelError("Especialidades", "Debe cargar al menos una Especialidad");

            if (model.Horarios.Count == 0)
                ModelState.AddModelError("Dias", "Debe elegir al menos un dia de atencion");

            if (ModelState.IsValid)
            {
                _medicoService.Alta(model);
                return RedirectToAction("Index");
            }

            ViewBag.EspecialidadesList = new SelectList(_especialidadService.ObtenerEspecialidads(), "ID", "Descripcion");
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = _medicoService.ObtenerMedico(id.Value);
            if (medico == null)
            {
                return HttpNotFound();
            }
            ViewBag.EspecialidadesList = new SelectList(_especialidadService.ObtenerEspecialidads(), "ID", "Descripcion");
            return View(medico);
        }

        [HttpPost]
        public ActionResult Edit(Medico medico)
        {
            try
            {
                if (medico.Especialidades.Count == 0)
                    ModelState.AddModelError("Especialidades", "Debe cargar al menos una Especialidad");

                if (medico.Horarios.Count == 0)
                    ModelState.AddModelError("Dias", "Debe elegir al menos un dia de atencion");

                if (ModelState.IsValid)
                {
                    _medicoService.Actualizar(medico);
                    return RedirectToAction("Index");
                }
                return View(medico);
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = _medicoService.ObtenerMedico(id.Value);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _medicoService.Borrar(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GetEspecialidadByName(string term)
        {
            try
            {
                var clientes = _especialidadService.ObtenerByName(term).ToList();
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


        [HttpPost]
        public ActionResult GetByName(string term, int idEspecialidad)
        {
            try
            {
                var clientes = _medicoService.ObtenerByName(term, idEspecialidad).ToList();
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
    }
}
