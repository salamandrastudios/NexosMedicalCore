﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexosMedical.Api.Models
{
    public class PacientesViewModel
    {
        public int Id { get; set; }

        public string NombreCompleto { get; set; }

        public int NumeroSeguroSocial { get; set; }

        public int CodigoPostal { get; set; }

        public string TelefonoContacto { get; set; }
    }
}

