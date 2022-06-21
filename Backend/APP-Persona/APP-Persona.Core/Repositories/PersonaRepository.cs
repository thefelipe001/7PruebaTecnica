using APP_Persona.Core.Data;
using APP_Persona.Core.Interfaces;
using APP_Persona.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace APP_Persona.Core.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly PersonasDbContext _dbContext;
        protected List<Expression<Func<Persona, object>>> Includes { get; } = new List<Expression<Func<Persona, object>>>();

        public PersonaRepository(PersonasDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Create(Persona persona)
        {
            bool existo = true;

            try
            {
                if (persona != null)
                {
                    await _dbContext.Persona.AddAsync(persona);
                    await _dbContext.SaveChangesAsync();

                }

            }
            catch (Exception ex)
            {
                existo = false;
                throw new Exception($"Error al insertar la Persona {ex}");
            }
            return existo;
        }

        public Persona Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Persona>> GetAll()
        {
            try
            {
                return await _dbContext.Persona.ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al Obtener datos"+ex);
            }
        }
        public IQueryable<Persona> ImplementIncludes(IQueryable<Persona> IncludedQuery)
        {
            IQueryable<Persona> currentQuery = IncludedQuery;
            foreach (var include in Includes)
                currentQuery = currentQuery.Include(include);

            return currentQuery;
        }
        public async Task<Persona> GetFirst(Expression<Func<Persona, bool>> query)
        {
            IQueryable<Persona> currentQuery = ImplementIncludes(_dbContext.Persona);
            return await currentQuery.FirstOrDefaultAsync(query);
        }

        public async Task<bool> Remove(Persona id)
        {
            bool existo = true;
            try
            {
                _dbContext.Persona.Remove(id);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                existo = false;
                throw new Exception($"Ha ocurrido un error {ex}");
            }
            return existo;
        }

        public async Task<bool> Update(Persona persona)
        {
            bool exito = true;

            try
            {
                _dbContext.Entry(persona).State = EntityState.Modified;
                _dbContext.Persona.Update(persona);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                exito = false;

                throw new Exception($"Ha ocurrido un error {ex}");
            }

            return exito;
        }
    }
}
