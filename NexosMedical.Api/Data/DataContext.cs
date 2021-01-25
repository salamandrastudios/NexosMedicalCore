using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexosMedical.Api.Data.Entities;

namespace NexosMedical.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Paciente> Pacientes { get; set; }

        public DbSet<CitaMedica> CitasMedicas { get; set; }

    }
}
