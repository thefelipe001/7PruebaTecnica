using APP_Persona.Core.Actions;
using APP_Persona.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP_Persona.Core.Interfaces
{
    public interface IPersonaRepository: IReadRepository<Persona, int>,ICreateRepository<Persona>,IRemoveRepository<Persona>,IUpdateRepository<Persona>
    {

    }
}
