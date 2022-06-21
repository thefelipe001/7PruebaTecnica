using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APP_Persona.Core.Actions
{
    public interface IRemoveRepository<T>
    {
        Task<bool> Remove(T id);
    }
}
