using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexosMedical.Api.Data.Entities;

namespace NexosMedical.Api.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;

        public SeedDB(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckDoctorAsync();
            await CheckPacienteAsync();
            await CheckCitasMedicasAsync();
        }      

        private async Task CheckDoctorAsync()
        {
            if(!_context.Doctors.Any())
            {
                _context.Doctors.Add(new Doctor { NombreCompleto = "Juan Manuel García", Especialidad = "Medico Cirujano", NumeroCredencial = 1000123456, HospitalDondeTrabaja = "Fundacion Santa fe de Bogota" });
                _context.Doctors.Add(new Doctor { NombreCompleto = "Augusto Peñaranda", Especialidad = "Medico General", NumeroCredencial = 1000123457, HospitalDondeTrabaja = "Fundacion Santa fe de Bogota" });
                _context.Doctors.Add(new Doctor { NombreCompleto = "Diana Quijano", Especialidad = "Medico Cardiologo", NumeroCredencial = 1000123458, HospitalDondeTrabaja = "Fundacion Santa fe de Bogota" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckPacienteAsync()
        {
            if (!_context.Pacientes.Any())
            {
                _context.Pacientes.Add(new Paciente { NombreCompleto= "Gilma Aldana", NumeroSeguroSocial=500101, CodigoPostal=110111, TelefonoContacto="2334455 - 3102445566" });
                _context.Pacientes.Add(new Paciente { NombreCompleto = "Javier Ospina", NumeroSeguroSocial = 500199, CodigoPostal = 110221, TelefonoContacto = "2334499 - 3102445577" });
                _context.Pacientes.Add(new Paciente { NombreCompleto = "Andrea Lopez", NumeroSeguroSocial = 500339, CodigoPostal = 110231, TelefonoContacto = "2334478 - 3102445588" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCitasMedicasAsync()
        {
            var paciente = _context.Pacientes.FirstOrDefault();
            var doctor = _context.Doctors.FirstOrDefault();

            if(!_context.CitasMedicas.Any())
            {
                AddCitaMedica(DateTime.Now.AddDays(2), paciente, doctor, "La cita es a las 8am en el consultorio 201");

                await _context.SaveChangesAsync();
            }
        }

        private void AddCitaMedica(DateTime fechaCita, Paciente paciente, Doctor doctor, string notas)
        {
            _context.CitasMedicas.Add(new CitaMedica 
            { 
                FechaCita = fechaCita,
                Paciente = paciente,
                Doctor = doctor,
                Notas = notas
            });
        }
    }
}
