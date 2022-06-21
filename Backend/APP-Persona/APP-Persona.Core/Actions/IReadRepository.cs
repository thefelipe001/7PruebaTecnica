using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace APP_Persona.Core.Actions
{
    public interface IReadRepository<T, Y> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        T Get(Y id);
        Task<T>  GetFirst(Expression<Func<T, bool>> query);
    }
}
