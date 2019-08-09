using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet_Core.Repositories;
using Veterinaria_Services;
using Vet_Data.Models;

namespace Vet_BLL
{
    public class RazaBLL
    {
        public readonly RazaRepository _RazaRepository =  new RazaRepository();

        public RazaBLL()
        {
            //_RazaRepository = RazaRepository;
        }
        public List<Raza> ObtenerRazas()
        {
            List<Raza> Razas = new List<Raza>();
            try
            {
                Razas = _RazaRepository.List().ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Razas;
        }

        public void Alta(Raza Raza)
        {
            try
            {
                _RazaRepository.Add(Raza);
                _RazaRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        public Raza ObtenerRaza(int id)
        {
            Raza Raza = new Raza();
            try
            {
                Raza = _RazaRepository.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Raza;
        }

        public void Actualizar(Raza Raza)
        {
            try
            {
                _RazaRepository.Update(Raza);
                _RazaRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        public Dictionary<int, string> ObtenerRazaByName(string name)
        {
            var diccionario = new Dictionary<int, string>();

            List<Raza> razas = new List<Raza>();
            try
            {
                razas = _RazaRepository.List(o => o.Descripcion.Contains(name)).ToList();
                foreach (var item in razas)
                {
                    diccionario.Add(item.ID, item.Descripcion);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return diccionario;
        }

        public void Borrar(int id)
        {
            try
            {
                _RazaRepository.Delete(id);
                _RazaRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }
    }
}
