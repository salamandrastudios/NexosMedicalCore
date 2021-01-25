using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexosMedical.Api.Models
{
    public class InsertarDoctorViewModel
    {
        public string NombreCompleto { get; set; }

        public string Especialidad { get; set; }

        public int NumeroCredencial { get; set; }

        public string HospitalDondeTrabaja { get; set; }
    }
}
