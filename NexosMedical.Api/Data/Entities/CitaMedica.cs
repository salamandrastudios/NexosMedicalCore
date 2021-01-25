using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexosMedical.Api.Data.Entities
{
    public class CitaMedica
    {
        public int Id { get; set; }

        public DateTime FechaCita { get; set; }

        public Paciente Paciente { get; set; }

        public Doctor Doctor { get; set; }

        public string Notas { get; set; }      
    }
}
