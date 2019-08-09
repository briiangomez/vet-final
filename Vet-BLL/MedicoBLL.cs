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
    public class MedicoBLL
    {
        public readonly MedicoRepository _MedicoRepository = new MedicoRepository();
        public readonly HorarioMedicoRepository _horarioRepository = new HorarioMedicoRepository();
        public readonly EspecialidadRepository _especialidadRepository = new EspecialidadRepository();
        public MedicoBLL()
        {
            //_MedicoRepository = MedicoRepository;
        }
        public List<Medico> ObtenerMedicos()
        {
            List<Medico> Medicos = new List<Medico>();
            try
            {
                Medicos = _MedicoRepository.List().ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Medicos;
        }

        public void Alta(Medico Medico)
        {
            try
            {
                _MedicoRepository.Add(Medico);
                _MedicoRepository.Save();

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        public Medico ObtenerMedico(int id)
        {
            Medico Medico = new Medico();
            try
            {
                Medico = _MedicoRepository.Find(id);
                //foreach (var item in Medico.Especialidad)
                //{
                //    Medico.EspecialidadesIds += item.ID + ";";
                //}

                //foreach (var item in Medico.Horarios)
                //{
                //    Medico.DiasIds += item.Dia + ";";
                //}
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Medico;
        }

        public Dictionary<int, string> ObtenerByName(string name, int idEspecialidad)
        {
            var diccionario = new Dictionary<int, string>();

            List<Medico> esps = new List<Medico>();
            try
            {
                esps = _MedicoRepository.List(o => (o.Nombre.Contains(name) || o.Apellido.Contains(name)) && o.Especialidades.Any(q => q.EspecialidadID == idEspecialidad)).ToList();
                foreach (var item in esps)
                {
                    diccionario.Add(item.ID, item.NombreCompleto);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return diccionario;
        }


        public List<Medico> ObtenerMedicoByEspecialidad(int EspecialidadId)
        {
            List<Medico> Medicos = new List<Medico>();
            try
            {
                Medicos = _MedicoRepository.ObtenerMedicoByEspecialidad(EspecialidadId).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Medicos;
        }

        public void Actualizar(Medico Medico)
        {
            try
            {
                var med = _MedicoRepository.Find(Medico.ID);
                foreach (var item in med.Especialidades)
                {
                    med.Especialidades.Remove(item);
                }
                foreach (var item in med.Horarios)
                {
                    med.Horarios.Remove(item);
                }
                _MedicoRepository.Update(Medico);
                _MedicoRepository.Save();
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
                _MedicoRepository.Delete(id);
                _MedicoRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }
    }
}