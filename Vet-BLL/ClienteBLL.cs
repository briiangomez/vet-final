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

namespace Vet_BLL
{
    public class ClienteBLL
    {
        public readonly ClienteRepository _clienteRepository = new ClienteRepository();

        public ClienteBLL()
        {
            //_clienteRepository = clienteRepository;
        }
        public List<ClienteVM> ObtenerClientes()
        {
            List<ClienteVM> clientes = new List<ClienteVM>();
            try
            {
                var lstModel = _clienteRepository.List().ToList();
                clientes = Mapper.Map<List<ClienteVM>>(lstModel);

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return clientes;
        }

        public List<MascotaVM> ObtenerMascotaCliente(int idCliente)
        {
            List<MascotaVM> mascotas = new List<MascotaVM>();
            try
            {
                var lstModel = _clienteRepository.ObtenerMascotasCliente(idCliente).ToList();
                mascotas = Mapper.Map<List<MascotaVM>>(lstModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return mascotas;

        }

        public void Alta(ClienteVM model)
        {
            try
            {
                var cliente = Mapper.Map<Cliente>(model);
                _clienteRepository.Add(cliente);
                _clienteRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        public ClienteVM ObtenerCliente(int id)
        {
            ClienteVM cliente = new ClienteVM();
            try
            {
                var model = _clienteRepository.Find(id);
                cliente = Mapper.Map<ClienteVM>(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return cliente;
        }

        public Dictionary<int,string> ObtenerClienteByName(string name)
        {
            List<Cliente> cliente = new List<Cliente>();
            var diccionario = new Dictionary<int, string>();
            try
            {
                cliente = _clienteRepository.List(o => o.Nombre.Contains(name) || o.Apellido.Contains(name)).ToList();
                foreach (var item in cliente)
                {
                    diccionario.Add(item.Id, item.NombreCompleto);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return diccionario;
        }

        public void Actualizar(ClienteVM model)
        {
            try
            {
                var cliente = Mapper.Map<Cliente>(model);
                _clienteRepository.Update(cliente);
                _clienteRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        public void Borrar(int id)
        {
            try
            {
                _clienteRepository.Delete(id);
                _clienteRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }
    }
}
