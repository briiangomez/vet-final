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
    public class HorarioMedicoBLL
    {
        public readonly HorarioMedicoRepository _HorarioMedicoRepository = new HorarioMedicoRepository();

        public HorarioMedicoBLL()
        {
            //_HorarioMedicoRepository = HorarioMedicoRepository;
        }
        public List<HorarioMedico> ObtenerHorarioMedicos()
        {
            List<HorarioMedico> HorarioMedicos = new List<HorarioMedico>();
            try
            {
                HorarioMedicos = _HorarioMedicoRepository.List().ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return HorarioMedicos;
        }

        public List<HorarioMedico> ObtenerHorarios(int MedicoId, DayOfWeek Dia)
        {
            List<HorarioMedico> HorarioMedicos = new List<HorarioMedico>();
            try
            {
                HorarioMedicos = _HorarioMedicoRepository.List(o => o.MedicoId.Equals(MedicoId) && o.Dia.Equals(Dia)).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return HorarioMedicos;
        }

        public void Alta(HorarioMedico HorarioMedico)
        {
            try
            {
                _HorarioMedicoRepository.Add(HorarioMedico);
                _HorarioMedicoRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        public string obtenerDiaSemana(int dia)
        {
            try
            {
                switch (dia)
                {
                    case 1:
                        return "Lunes";
                    case 2:
                        return "Martes";
                    case 3:
                        return "Miercoles";
                    case 4:
                        return "Jueves";
                    case 5:
                        return "Viernes";
                    case 6:
                        return "Sabado";
                    case 7:
                        return "Domingo";
                    default:
                        return String.Empty;
                }

            }
            catch(Exception ex)
            {
                Log.Error(ex.ToString());
                return String.Empty;
            }
        }

        public HorarioMedico ObtenerHorarioMedico(int id)
        {
            HorarioMedico HorarioMedico = new HorarioMedico();
            try
            {
                HorarioMedico = _HorarioMedicoRepository.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return HorarioMedico;
        }

        public void Actualizar(HorarioMedico HorarioMedico)
        {
            try
            {
                _HorarioMedicoRepository.Update(HorarioMedico);
                _HorarioMedicoRepository.Save();
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
                _HorarioMedicoRepository.Delete(id);
                _HorarioMedicoRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }
    }
}
