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

namespace Vet_Final.Controllers
{
    public class FacturacionController : Controller
    {
        private FacturaBLL _facturaService = new FacturaBLL();
        private TurnoBLL _turnosService = new TurnoBLL();
        private ClienteBLL _clienteService = new ClienteBLL();
        private ItemBLL _itemService = new ItemBLL();
        // GET: Facturacion
        public ActionResult Index()
        {
            var factura = _facturaService.ObtenerFacturas();
            return View(factura.ToList());
        }

        // GET: Facturacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = _facturaService.ObtenerFactura(id.Value);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Facturacion/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura fact = new Factura();
            fact.Turno = _turnosService.ObtenerTurno(id.Value);
            fact.Cliente = fact.Turno.Mascota.Cliente;
            ViewBag.ItemId = new SelectList(_itemService.ObtenerItems().Where(o => o.Tipo == TipoItem.Producto), "ID", "Descripcion");
            return View(fact);
        }

        // POST: Facturacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Factura factura)
        {
            if (ModelState.IsValid)
            {
                _facturaService.Alta(factura);
                return RedirectToAction("Index");
            }
            factura.Turno = _turnosService.ObtenerTurno(factura.TurnoId);
            factura.Cliente = factura.Turno.Mascota.Cliente;
            ViewBag.ItemId = new SelectList(_itemService.ObtenerItems().Where(o => o.Tipo == TipoItem.Producto), "ID", "Descripcion");
            return View(factura);
        }

        // GET: Facturacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = _facturaService.ObtenerFactura(id.Value);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        public decimal GetImporte(int ItemId)
        {
            try
            {
                return _facturaService.getImporte(ItemId);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public FileResult GeneratePDF(int id)
        {
            Factura factura = _facturaService.ObtenerFactura(id);
            return File(_facturaService.GetPDF(id), "application/pdf", factura.Cliente.NombreCompleto + "-" + factura.Fecha + ".pdf");
        }
        // POST: Facturacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Numero,ClienteId,Fecha,TurnoId,Importe,LetraFactura,FormaPago")] Factura factura)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(factura).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ClienteId = new SelectList(db.Cliente, "Id", "Nombre", factura.ClienteId);
        //    ViewBag.TurnoId = new SelectList(db.Turno, "ID", "ID", factura.TurnoId);
        //    return View(factura);
        //}

        // GET: Facturacion/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Factura factura = db.Factura.Find(id);
        //    if (factura == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(factura);
        //}

        // POST: Facturacion/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Factura factura = db.Factura.Find(id);
        //    db.Factura.Remove(factura);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


    }
}
