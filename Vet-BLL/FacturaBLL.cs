using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet_Core.Repositories;
using Veterinaria_Services;
using Vet_Data.Models;
using Vet_Core.ViewModels;
using AutoMapper;
using Veterinaria_Services.PDF;

namespace Vet_BLL
{
    public class FacturaBLL
    {
        public readonly FacturaRepository _FacturaRepository =  new FacturaRepository();
        public readonly ItemFacturaRepository _itemFacturaRepository =  new ItemFacturaRepository();
        public readonly ItemRepository _itemRepository = new ItemRepository();
        public readonly TurnoRepository _turnoRepository = new TurnoRepository();

        public FacturaBLL()
        {
            //_FacturaRepository = FacturaRepository;
            //_itemFacturaRepository = itemFacturaRepository;
        }


        public int ObtenerUltimoNumero()
        {
            try
            {
                return ObtenerFacturas().OrderByDescending(x => x.Numero).FirstOrDefault().Numero;
            }
            catch
            {
                return 1;
            }
        }

        public List<Factura> ObtenerFacturas()
        {
            List<Factura> Facturas = new List<Factura>();
            try
            {
                Facturas = _FacturaRepository.List().ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Facturas;
        }

        public List<ItemFactura> ObtenerItemFactura(int idFactura)
        {
            List<ItemFactura> items = new List<ItemFactura>();
            try
            {
                items = _FacturaRepository.ObtenerItemFactura(idFactura).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return items;

        }

        public void Alta(Factura model)
        {
            try
            {
                model.Numero = ObtenerUltimoNumero();
                model.Fecha = DateTime.Now;
                _FacturaRepository.Add(model);
                var turno = _turnoRepository.Find(model.TurnoId);
                turno.Estado = EstadoTurno.Facturados;
                _turnoRepository.Update(turno);
                _turnoRepository.Save();
                _FacturaRepository.Save();

                //foreach (var item in Factura.ItemFactura)
                //{
                //    item.FacturaId = Factura.ID;
                //    _itemFacturaRepository.Add(item);
                //}
                //_itemFacturaRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        public decimal getImporte(int ItemId)
        {
            try
            {
                return _itemRepository.Find(ItemId).Valor;
            }
            catch(Exception ex)
            {
                return 0;
            }
        
         }

        public Factura ObtenerFactura(int id)
        {
            Factura Factura = new Factura();
            try
            {
                Factura = _FacturaRepository.Find(id);
                Factura.ItemFactura = _FacturaRepository.ObtenerItemFactura(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Factura;
        }

        public void Actualizar(Factura Factura)
        {
            try
            {

                Factura factura = _FacturaRepository.Find(Factura.ID);
                factura.ItemFactura = _FacturaRepository.ObtenerItemFactura(Factura.ID);

                foreach (var item in factura.ItemFactura)
                {
                    _itemFacturaRepository.Delete(item);
                }


                foreach (var item in Factura.ItemFactura)
                {
                    item.FacturaId = Factura.ID;
                    _itemFacturaRepository.Add(item);
                }

                _FacturaRepository.Update(Factura);
                _itemFacturaRepository.Save();
                _FacturaRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        public byte[] GetPDF(int id)
        {
            var entity = _FacturaRepository.Find(id);
            return new PdfGenerator().GetPDF(entity).ToArray();
        }

        public void Borrar(int id)
        {
            try
            {
                Factura factura = _FacturaRepository.Find(id);
                factura.ItemFactura = _FacturaRepository.ObtenerItemFactura(id);

                foreach (var item in factura.ItemFactura)
                {
                    _itemFacturaRepository.Delete(item);
                }

                _itemFacturaRepository.Save();

                _FacturaRepository.Delete(id);
                _FacturaRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }
    }
}
