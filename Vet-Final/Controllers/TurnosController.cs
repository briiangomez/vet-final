using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vet_BLL;
using Vet_Core.ViewModels;
using Vet_Data.Context;
using Vet_Data.Models;
using Veterinaria_Services;

namespace Veterinaria_UI.Controllers
{
    public class TurnosController : Controller
    {
        private TurnoBLL _turnosService = new TurnoBLL();
        private MedicoBLL _medicosService = new MedicoBLL();
        private SalaBLL _salaService = new SalaBLL();
        private ItemBLL _itemService = new ItemBLL();
        private EspecialidadBLL _especialidadService = new EspecialidadBLL();
        // GET: Turnos
        public ActionResult Index()
        {
            try
            {
                //var turno = _turnosService.ObtenerTurnos();
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return View("Error");
            }
        }

        public ActionResult ObtenerTurnos()
        {
            return Json(_turnosService.ObtenerTurnos().Select(x => new {
                Desc = x.Medico.NombreCompleto + "- " + x.Especialidad.Descripcion,
                Start_Date = x.FechaInicio,
                End_Date = x.FechaFin,
                Title = x.Item.Descripcion + " - " + x.Mascota.Nombre,
                url = "/Turnos/Details/" + x.ID
            }), JsonRequestBehavior.AllowGet);
        }

        // GET: Turnos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = _turnosService.ObtenerTurno(id.Value);
            if (turno == null)
            {
                return HttpNotFound();
            }
            return View(turno);
        }

        // GET: Turnos/Create
        public ActionResult Create()
        {
            ViewBag.SalaId = new SelectList(_salaService.ObtenerSalas(), "ID", "Nombre");
            ViewBag.EspecialidadId = new SelectList(_especialidadService.ObtenerEspecialidads(), "ID", "Descripcion");
            ViewBag.ItemId = new SelectList(_itemService.ObtenerItems().Where(o => o.Tipo == TipoItem.Servicio), "ID", "Descripcion");
            return View();
        }

        [HttpPost]
        public ActionResult Create(TurnoVM turno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _turnosService.Alta(turno);
                    return RedirectToAction("Index");
                }
                ViewBag.SalaId = new SelectList(_salaService.ObtenerSalas(), "ID", "Nombre",turno.SalaId);
                ViewBag.EspecialidadId = new SelectList(_especialidadService.ObtenerEspecialidads(), "ID", "Descripcion",turno.EspecialidadId);
                ViewBag.ItemId = new SelectList(_itemService.ObtenerItems().Where(o => o.Tipo == TipoItem.Servicio).Where(o => o.Tipo == TipoItem.Servicio), "ID", "Descripcion",turno.ItemId);
                return View(turno);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return View("Error");
            }
        }

        // GET: Turnos/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TurnoVM turno = _turnosService.ObtenerTurnoVM(id.Value);
                if (turno == null)
                {
                    return HttpNotFound();
                }
                ViewBag.SalaId = new SelectList(_salaService.ObtenerSalas(), "ID", "Nombre", turno.SalaId);
                ViewBag.EspecialidadId = new SelectList(_especialidadService.ObtenerEspecialidads(), "ID", "Descripcion", turno.EspecialidadId);
                ViewBag.ItemId = new SelectList(_itemService.ObtenerItems().Where(o => o.Tipo == TipoItem.Servicio), "ID", "Descripcion", turno.ItemId);
                return View(turno);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(TurnoVM turno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _turnosService.Actualizar(turno);
                    return RedirectToAction("Index");
                }
                ViewBag.SalaId = new SelectList(_salaService.ObtenerSalas(), "ID", "Nombre", turno.SalaId);
                ViewBag.EspecialidadId = new SelectList(_especialidadService.ObtenerEspecialidads(), "ID", "Descripcion", turno.EspecialidadId);
                ViewBag.ItemId = new SelectList(_itemService.ObtenerItems().Where(o => o.Tipo == TipoItem.Servicio), "ID", "Descripcion", turno.ItemId);
                return View(turno);
            }
            catch(Exception ex)
            {
                Log.Error(ex.ToString());
                return View("Error");
            }
        }

        // GET: Turnos/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Turno turno = _turnosService.ObtenerTurno(id.Value);
                if (turno == null)
                {
                    return HttpNotFound();
                }
                return View(turno);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return View("Error");
            }
        }

        // POST: Turnos/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _turnosService.Borrar(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Log.Error(ex.ToString());
                return View("Error");
            }
        }

        public IEnumerable<SelectListItem> DoctoresByEspecialidad(int EspecialidadID)
        {
            var medicos = _medicosService.ObtenerMedicoByEspecialidad(EspecialidadID);
            return medicos.Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.NombreCompleto });
        }

        public ActionResult ObtenerHorarios(int medicoID, int salaID, string fecha, int EspecialidadID)
        {
            DateTime fechaDateTime = DateTime.Parse(fecha);

            List<DateTime> listaHorarios = _turnosService.GetHorarios(medicoID, EspecialidadID, salaID, fechaDateTime);
            var result = (from s in listaHorarios
                          select new
                          {
                              id = s.ToString("dd/MM/yyyy HH:mm"),
                              name = s.ToString("HH:mm")
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
