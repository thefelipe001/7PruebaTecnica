using APP_Persona.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP_Persona.Core.Data
{
    public class PersonasDbContext : DbContext
    {
        public PersonasDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Persona> Persona { get; set; }
    }
}
