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
using Vet_Core.Repositories;
using Vet_Core.ViewModels;

namespace Veterinaria_UI.Controllers
{
    public class ClientesController : Controller
    {
        private ClienteBLL _clienteService = new ClienteBLL();

        // GET: Clientes
        public ActionResult Index()
        {
            try
            {
                return View(_clienteService.ObtenerClientes());
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ClienteVM cliente = _clienteService.ObtenerCliente(id.Value);
                if (cliente == null)
                {
                    return HttpNotFound();
                }
                return View(cliente);
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            //ViewBag.TipoDocumentoId = new SelectList(_tipoDocumentoService.ObtenerTipoDocumentos(), "ID", "Descripcion");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(ClienteVM cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _clienteService.Alta(cliente);
                    return RedirectToAction("Index");
                }
                //ViewBag.TipoDocumentoId = new SelectList(_tipoDocumentoService.ObtenerTipoDocumentos(), "ID", "Descripcion");
                return View(cliente);
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ClienteVM cliente = _clienteService.ObtenerCliente(id.Value);
                if (cliente == null)
                {
                    return HttpNotFound();
                }
                //ViewBag.TipoDocumentoId = new SelectList(_tipoDocumentoService.ObtenerTipoDocumentos(), "ID", "Descripcion", cliente.TipoDocumentoId);
                return View(cliente);
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(ClienteVM cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _clienteService.Actualizar(cliente);
                    return RedirectToAction("Index");
                }
                //ViewBag.TipoDocumentoId = new SelectList(_tipoDocumentoService.ObtenerTipoDocumentos(), "ID", "Descripcion",cliente.TipoDocumentoId);
                return View(cliente);
            }
            catch(Exception ex)
            {
                return View("Error");
            }
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ClienteVM cliente = _clienteService.ObtenerCliente(id.Value);
                if (cliente == null)
                {
                    return HttpNotFound();
                }
                return View(cliente);
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _clienteService.Borrar(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult GetByName(string term)
        {
            try
            {
                var clientes = _clienteService.ObtenerClienteByName(term).ToList();
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

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
