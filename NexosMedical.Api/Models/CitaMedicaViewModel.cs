using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexosMedical.Api.Models
{
    public class CitaMedicaViewModel
    {
        public int Id { get; set; }

        public DateTime FechaCita { get; set; }

        public int Paciente { get; set; }

        public int Doctor { get; set; }

        public string Notas { get; set; }
    }
}
