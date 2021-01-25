using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NexosMedical.Api.Data.Entities
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreCompleto { get; set; }

        [Required]
        [MaxLength(50)]
        public string Especialidad { get; set; }

        [Required]
        [MaxLength(25)]
        public int NumeroCredencial { get; set; }

        [Required]
        [MaxLength(150)]
        public string HospitalDondeTrabaja { get; set; }
    
    
        public ICollection<CitaMedica> CitasMedicas { get; set; }
    }
}
