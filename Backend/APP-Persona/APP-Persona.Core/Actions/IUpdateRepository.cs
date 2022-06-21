using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APP_Persona.Core.Actions
{
    public interface IUpdateRepository<T> where T : class
    {
        Task<bool> Update(T t);
    }
}
