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
    public class EspecialidadBLL
    {
        public readonly EspecialidadRepository _EspecialidadRepository = new EspecialidadRepository();

        public EspecialidadBLL()
        {
            //_EspecialidadRepository = EspecialidadRepository;
        }
        public List<Especialidad> ObtenerEspecialidads()
        {
            List<Especialidad> Especialidads = new List<Especialidad>();
            try
            {
                Especialidads = _EspecialidadRepository.List().ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Especialidads;
        }

        public TimeSpan ObtenerDuracionEspecialidad(int idMedico, int idEsp)
        {
            return _EspecialidadRepository.ObtenerDuracionEspecialidad(idMedico, idEsp);
        }

        public TimeSpan ObtenerMinimaDuracion(int idMedico)
        {
            return _EspecialidadRepository.ObtenerMinimaDuracion(idMedico);
        }


        public List<Especialidad> ObtenerEspecialidadesByMedico(int MedicoId)
        {
            List<Especialidad> Especialidads = new List<Especialidad>();
            try
            {
                Especialidads = _EspecialidadRepository.ObtenerEspecialidadesByDoctor(MedicoId).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Especialidads;
        }

        public void Alta(Especialidad Especialidad)
        {
            try
            {
                _EspecialidadRepository.Add(Especialidad);
                _EspecialidadRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        public Especialidad ObtenerEspecialidad(int id)
        {
            Especialidad Especialidad = new Especialidad();
            try
            {
                Especialidad = _EspecialidadRepository.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Especialidad;
        }

        public string ObtenerOnlyName(int id)
        {
            Especialidad Especialidad = new Especialidad();
            try
            {
                Especialidad = _EspecialidadRepository.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Especialidad.Descripcion;
        }

        public Dictionary<int, string> ObtenerByName(string name)
        {
            var diccionario = new Dictionary<int, string>();

            List<Especialidad> esps = new List<Especialidad>();
            try
            {
                esps = _EspecialidadRepository.List(o => o.Descripcion.Contains(name)).ToList();
                foreach (var item in esps)
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

        public void Actualizar(Especialidad Especialidad)
        {
            try
            {
                _EspecialidadRepository.Update(Especialidad);
                _EspecialidadRepository.Save();
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
                _EspecialidadRepository.Delete(id);
                _EspecialidadRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }
    }
}
