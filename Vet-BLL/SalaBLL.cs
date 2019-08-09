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
    public class SalaBLL
    {
        public readonly SalaRepository _SalaRepository =  new SalaRepository();

        public SalaBLL()
        {
           // _SalaRepository = SalaRepository;
        }
        public List<Sala> ObtenerSalas()
        {
            List<Sala> Salas = new List<Sala>();
            try
            {
                Salas = _SalaRepository.List().ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Salas;
        }

        public void Alta(Sala Sala)
        {
            try
            {
                _SalaRepository.Add(Sala);
                _SalaRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        public Sala ObtenerSala(int id)
        {
            Sala Sala = new Sala();
            try
            {
                Sala = _SalaRepository.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Sala;
        }

        public void Actualizar(Sala Sala)
        {
            try
            {
                _SalaRepository.Update(Sala);
                _SalaRepository.Save();
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
                _SalaRepository.Delete(id);
                _SalaRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }
    }
}
