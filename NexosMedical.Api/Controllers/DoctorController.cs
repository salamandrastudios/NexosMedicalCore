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
    public class DoctorController : ControllerBase
    {
        private readonly DataContext _context;

        public DoctorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Doctor/getDoctores
        [HttpGet("[action]")]
        public async Task<IEnumerable<DoctoresViewModel>> getDoctores()
        {
            var listDoctor = await _context.Doctors.Where(d => d.NombreCompleto != string.Empty).ToListAsync();

            if (listDoctor.Count == 0)
            {
                return (IEnumerable<DoctoresViewModel>)NotFound();
            }

            return listDoctor.Select(x => new DoctoresViewModel
            {
                Id = x.Id,
                NombreCompleto = x.NombreCompleto,
                Especialidad = x.Especialidad,
                NumeroCredencial = x.NumeroCredencial,
                HospitalDondeTrabaja = x.HospitalDondeTrabaja
            });
        }

        // POST: api/Doctor/insertarDoctor
        [HttpPost("[action]")]
        public async Task<IActionResult> insertarDoctor([FromBody] InsertarDoctorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Doctor doctor = new Doctor
                {
                    NombreCompleto = model.NombreCompleto,
                    Especialidad = model.Especialidad,
                    NumeroCredencial = model.NumeroCredencial,
                    HospitalDondeTrabaja = model.HospitalDondeTrabaja
                };

                _context.Doctors.Add(doctor);
     
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok();
        }

        // POST: api/Doctor/asignarDoctor
        [HttpPost("[action]")]
        public async Task<IActionResult> asignarDoctor([FromBody] AsignarDoctorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                CitaMedica citaMedica = new CitaMedica
                {
                    FechaCita = DateTime.Now.AddDays(2),
                    //Paciente = model.IdPaciente, //TODO: Duda con esta asignacion no la pude resolver
                    //Doctor = model.IdDoctor, //TODO: Duda con esta asignacion no la pude resolver
                    Notas = "Ninguno"       
                };

                _context.CitasMedicas.Add(citaMedica);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok();
        }

        // DELETE: api/Doctor/eliminarDoctor/5
        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> eliminarDoctor([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = await _context.Doctors.FirstOrDefaultAsync(e => e.Id == Id);

            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok(doctor);
        }


    }
}
