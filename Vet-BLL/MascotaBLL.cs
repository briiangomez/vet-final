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
    public class MascotaBLL
    {
        public readonly MascotaRepository _MascotaRepository = new MascotaRepository();

        public MascotaBLL()
        {
            //_MascotaRepository = MascotaRepository;
        }
        public List<Mascota> ObtenerMascotas()
        {
            List<Mascota> Mascotas = new List<Mascota>();
            try
            {
                Mascotas = _MascotaRepository.List().ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Mascotas;
        }

        public void Alta(Mascota Mascota)
        {
            try
            {

                _MascotaRepository.Add(Mascota);
                _MascotaRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        public Mascota ObtenerMascota(int id)
        {
            Mascota Mascota = new Mascota();
            try
            {
                Mascota = _MascotaRepository.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Mascota;
        }

        public void Actualizar(Mascota Mascota)
        {
            try
            {
                _MascotaRepository.Update(Mascota);
                _MascotaRepository.Save();
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
                _MascotaRepository.Delete(id);
                _MascotaRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }
    }
}