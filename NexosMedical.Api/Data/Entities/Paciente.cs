using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NexosMedical.Api.Data.Entities
{
    public class Paciente
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreCompleto { get; set; }

        [Required]
        [MaxLength(25)]
        public int NumeroSeguroSocial { get; set; }

        [MaxLength(10)]
        public int CodigoPostal { get; set; }

        [MaxLength(30)]
        public string TelefonoContacto { get; set; }

        public ICollection<CitaMedica> CitasMedicas { get; set; }
    }
}
