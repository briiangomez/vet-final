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
    public class TurnoBLL
    {
        public readonly TurnoRepository _TurnoRepository =  new TurnoRepository();
        public readonly MedicoRepository _medicoepository = new MedicoRepository();

        public readonly HorarioMedicoRepository _horariosRepository = new HorarioMedicoRepository();
        public readonly EspecialidadRepository _especialidadRepository = new EspecialidadRepository();
        private HorarioMedicoBLL schedule = new HorarioMedicoBLL();
        public TurnoBLL()
        {
            //_TurnoRepository = TurnoRepository;
        }
        public List<Turno> ObtenerTurnos()
        {
            List<TurnoVM> Turnos = new List<TurnoVM>();
            var lstModel = _TurnoRepository.List().ToList();
            return lstModel;
        }

        public void Alta(TurnoVM model)
        {
            var Turno = new Turno();
            Turno.SalaId = model.SalaId;
            Turno.EspecialidadId = model.EspecialidadId;
            Turno.ItemId = model.ItemId;
            Turno.MascotaId = model.MascotaId;
            Turno.MedicoId = model.MedicoId;
            Turno.FechaInicio = DateTime.Parse(model.Fecha);
            Turno.Estado = EstadoTurno.Pendientes;
            Turno.FechaFin = Turno.FechaInicio.Value.AddMinutes(_especialidadRepository.Find(model.EspecialidadId).Duracion);
            _TurnoRepository.Add(Turno);
            _TurnoRepository.Save();
        }

        public TurnoVM ObtenerTurnoVM(int id)
        {
            TurnoVM Turno = new TurnoVM();
            var model = _TurnoRepository.Find(id);
            Turno = Mapper.Map<TurnoVM>(model);
            return Turno;
        }


        public Turno ObtenerTurno(int id)
        {
            TurnoVM Turno = new TurnoVM();
            var model = _TurnoRepository.Find(id);
            return model;
        }
        public void Actualizar(TurnoVM model)
        {
            var Turno = _TurnoRepository.Find(model.ID);
            Turno.FechaInicio = DateTime.Parse(model.Fecha);
            Turno.Estado = EstadoTurno.Pendientes;
            Turno.FechaFin = Turno.FechaInicio.Value.AddMinutes(_especialidadRepository.Find(Turno.EspecialidadId).Duracion);
            _TurnoRepository.Update(Turno);
            _TurnoRepository.Save();
        }

        public void Borrar(int id)
        {
            _TurnoRepository.Delete(id);
            _TurnoRepository.Save();
        }

        private List<DateTime> GetTurnosPosibles(DateTime inicio, DateTime fin, int duracion, DateTime fecha)
        {
            List<DateTime> turnos = new List<DateTime>();
            inicio = new DateTime(fecha.Year, fecha.Month, fecha.Day, inicio.Hour, inicio.Minute,0);
            fin = new DateTime(fecha.Year, fecha.Month, fecha.Day, fin.Hour, fin.Minute, 0);
            TimeSpan ts = new TimeSpan(0, duracion, 0);
            while (inicio < fin)
            {
                if (inicio > DateTime.Now)
                {
                    turnos.Add(inicio);
                }
                inicio += ts;
            }

            return turnos;
        }

        public List<DateTime> GetHorarios(int idMedico, int idEspecialidad, int idSala, DateTime? fecha)
        {
            try
            {
                List<DateTime> list = new List<DateTime>();
                var especialidades = _especialidadRepository.ObtenerDuracionEspecialidad(idMedico, idEspecialidad);
                TimeSpan minima = _especialidadRepository.ObtenerMinimaDuracion(idMedico);
                double turnosNec = Math.Ceiling(especialidades.TotalMilliseconds / minima.TotalMilliseconds);
                var dia = fecha.Value.DayOfWeek;
                var horariosMedico = _horariosRepository.Find(o => o.Dia == dia && o.MedicoId == idMedico);
                List<Turno> turnos = _TurnoRepository.List(t => t.MedicoId == idMedico && t.FechaInicio > fecha && t.Estado == EstadoTurno.Pendientes && t.SalaId == idSala).ToList();
                List<DateTime> turnosPosibles = this.GetTurnosPosibles(horariosMedico.HoraDesde, horariosMedico.HoraHasta, especialidades.Minutes,fecha.Value);

                foreach (var app in turnos)
                {
                    turnosPosibles.RemoveAll(x => x >= app.FechaInicio && x < app.FechaFin);
                }

                turnosPosibles = FilterTimetable(minima, turnosNec, turnosPosibles);

                return turnosPosibles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DateTime> GetHorariosActualizar(int idMedico, int idEspecialidad, DateTime? fecha)
        {
            try
            {
                List<DateTime> list = new List<DateTime>();
                var especialidades = _especialidadRepository.ObtenerDuracionEspecialidad(idMedico, idEspecialidad);
                TimeSpan minima = _especialidadRepository.ObtenerMinimaDuracion(idMedico);
                double turnosNec = Math.Ceiling(especialidades.TotalMilliseconds / minima.TotalMilliseconds);
                var horariosMedico = _horariosRepository.Find(o => o.Dia == fecha.Value.DayOfWeek && o.MedicoId == idMedico);
                List<Turno> turnos = _TurnoRepository.List(t => t.MedicoId == idMedico && t.FechaInicio > fecha && t.Estado == EstadoTurno.Pendientes).ToList();
                List<DateTime> turnosPosibles = this.GetTurnosPosibles(horariosMedico.HoraDesde, horariosMedico.HoraHasta, especialidades.Minutes,fecha.Value);

                foreach (var app in turnos)
                {
                    turnosPosibles.RemoveAll(x => x == app.FechaInicio);
                }

                turnosPosibles = FilterTimetable(minima, turnosNec, turnosPosibles);

                return turnosPosibles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DateTime> FilterTimetable(TimeSpan minima, double turnosNec, List<DateTime> turnosPosibles)
        {
            DateTime lastIndexDate = new DateTime();

            if (turnosNec != 1)
            {
                if (turnosPosibles.Count >= turnosNec)
                {
                    for (int i = 0; i < turnosPosibles.Count - turnosNec; i++)
                    {
                        TimeSpan ts = new TimeSpan(0, 0, 0);

                        for (int j = 0; j < turnosNec; j++)
                        {
                            ts = ts.Add(turnosPosibles[i + j + 1].TimeOfDay.Subtract(turnosPosibles[i + j].TimeOfDay));
                        }

                        var x = Math.Ceiling(ts.TotalMilliseconds / minima.TotalMilliseconds);

                        if (x > turnosNec)
                        {
                            turnosPosibles.Remove(turnosPosibles[i]);
                            i -= 1;
                        }
                        else
                        {
                            lastIndexDate = turnosPosibles[i + 1];
                        }
                    }

                    foreach (var remainder in turnosPosibles.ToList())
                        turnosPosibles.RemoveAll(x => x > lastIndexDate);
                }
                else
                {
                    turnosPosibles.Clear();
                }
            }

            return turnosPosibles;
        }


        private DateTime SetBound(DateTime? date, int minutes)
        {
            if (date.HasValue)
            {
                DateTime sd = date.Value;
                sd = sd.Date.AddMinutes(minutes);
                return sd;
            }
            else
            {
                throw new Exception("Date was null.");
            }
        }

    }
}