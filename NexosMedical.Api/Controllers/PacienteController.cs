using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexosMedical.Api.Data;
using NexosMedical.Api.Data.Entities;
using NexosMedical.Api.Models;

namespace NexosMedical.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly DataContext _context;

        public PacienteController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Paciente/getPaciente/5
        [HttpGet("[action]/{Id}")]
        public async Task<IEnumerable<PacientesViewModel>> getPaciente([FromRoute] int Id)
        {
            var paciente = await _context.Pacientes.Where(p => p.Id == Id).ToListAsync();

            if (paciente.Count == 0)
            {
                return (IEnumerable<PacientesViewModel>)NotFound();
            }

            return paciente.Select(x => new PacientesViewModel
            {
                Id = x.Id,
                NombreCompleto = x.NombreCompleto,
                NumeroSeguroSocial = x.NumeroSeguroSocial,
                CodigoPostal = x.CodigoPostal,
                TelefonoContacto = x.TelefonoContacto
            });
        }

        // GET: api/Paciente/getPacientes
        [HttpGet("[action]")]
        public async Task<IEnumerable<PacientesViewModel>> getPacientes()
        {
            var listPaciente = await _context.Pacientes.Where(p => p.NombreCompleto != string.Empty).ToListAsync();

            if (listPaciente.Count == 0)
            {
                return (IEnumerable<PacientesViewModel>)NotFound();
            }

            return listPaciente.Select(x => new PacientesViewModel
            {
                Id = x.Id,
                NombreCompleto = x.NombreCompleto,
                NumeroSeguroSocial = x.NumeroSeguroSocial,
                CodigoPostal = x.CodigoPostal,
                TelefonoContacto = x.TelefonoContacto              
            });
        }

        // POST: api/Paciente/insertarPaciente
        [HttpPost("[action]")]
        public async Task<IActionResult> insertarPaciente([FromBody] InsertarPacienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Paciente paciente = new Paciente
                {
                    NombreCompleto = model.NombreCompleto,
                    NumeroSeguroSocial = model.NumeroSeguroSocial,
                    CodigoPostal = model.CodigoPostal,
                    TelefonoContacto = model.TelefonoContacto
                };

                _context.Pacientes.Add(paciente);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok();
        }


        // GET: api/Paciente/getDoctoresAsignados
        [HttpGet("[action]/{Id}")]
        public async Task<IEnumerable<DoctorAsignadoViewModel>> getDoctoresAsignados([FromRoute] int Id)
        {
            List<DoctorAsignadoViewModel> listDoctoresAsignados = new List<DoctorAsignadoViewModel>();
                 
            try
            {
                listDoctoresAsignados = await (from DOC in _context.Doctors
                                        join CME in _context.CitasMedicas on DOC.Id equals CME.Doctor.Id
                                        join PAC in _context.Pacientes on CME.Paciente.Id equals PAC.Id
                                        where PAC.Id == Id select new DoctorAsignadoViewModel
                                        {
                                            Id = DOC.Id,
                                            NombreCompleto = DOC.NombreCompleto
                                        }).ToListAsync();

                if (listDoctoresAsignados.Count == 0)
                {
                    return (IEnumerable<DoctorAsignadoViewModel>)NotFound();
                }
            }
            catch(Exception ex)
            {

            }

            return listDoctoresAsignados.Select(x => new DoctorAsignadoViewModel
            {
                Id = x.Id,
                NombreCompleto = x.NombreCompleto
            });
        }


        // PUT: api/Paciente/getDoctoresAsignados
        [HttpPut("[action]")]
        public async Task<IActionResult> actualizarPaciente([FromBody] PacientesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paciente = await _context.Pacientes.FirstOrDefaultAsync(e => e.Id == model.Id);

            if (paciente == null)
            {
                return NotFound();
            }

            paciente.NombreCompleto = model.NombreCompleto;
            paciente.NumeroSeguroSocial = model.NumeroSeguroSocial;
            paciente.CodigoPostal = model.CodigoPostal;
            paciente.TelefonoContacto = model.TelefonoContacto;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            return Ok();
        }


        // DELETE: api/Paciente/eliminarPaciente/5
        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> eliminarPaciente([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paciente = await _context.Pacientes.FirstOrDefaultAsync(e => e.Id == Id);

            if (paciente == null)
            {
                return NotFound();
            }

            _context.Pacientes.Remove(paciente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok(paciente);
        }


    }
}
