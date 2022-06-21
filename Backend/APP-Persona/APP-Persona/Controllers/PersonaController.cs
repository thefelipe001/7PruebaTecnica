using APP_Persona.Core.Data;
using APP_Persona.Core.Interfaces;
using APP_Persona.Core.Models;
using APP_Persona.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace APP_Persona.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PersonaController : Controller
    {
        private readonly IPersonaRepository _Personarepository;
        private readonly PersonasDbContext _context;
        public PersonaController(IPersonaRepository Personarepository, PersonasDbContext context)
        {
            _Personarepository = Personarepository;
            _context = context;
        }

        //Obtener por Todo
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaViewmodel>> List()
        {

            var people = await _Personarepository.GetAll();
            string fecha = Convert.ToString(people.Select(x=>x.FechaDeNacimiento));

            return people.OrderByDescending(x=>x.ID).Select(u => new PersonaViewmodel
            {
                ID = u.ID,
                Nombre = u.Nombre,
                FechaDeNacimiento =u.FechaDeNacimiento.ToString("yyyy-MM-dd HH:mm:ss") 
            }
            ) ;
        }
        //Metodo Obtener por Id
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetbyId([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Usuario con el ID:{id}, no existe.");
            }
            bool existo = true;
            try
            {
                var people = await _Personarepository.GetFirst(f => f.ID == id);
                if (people != null)
                {
                    string fecha = Convert.ToString(people.FechaDeNacimiento);
                    PersonaViewmodel model = new PersonaViewmodel
                    {
                        ID = people.ID,
                        Nombre = people.Nombre,
                        FechaDeNacimiento = people.FechaDeNacimiento.ToString("yyyy-MM-ddThh:mm")

                };
                    return Json(new { result = model });

                }
                else
                {
                    return Json(new { result = "No existe Registro con ese Id:" + " " + id });

                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Ha ocurrido un error {ex}");
            }


        }

        //Agregar Persona
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]  PersonaViewmodel model)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var persona = await _Personarepository.GetFirst
                                 (a => a.ID.ToString().ToLower() == model.ID.ToString()
                                 .ToLower());
            try
            {
                if (persona != null)
                {
                    return BadRequest("El persona ya existe");

                }

                Persona ps = new Persona
                {
                    Nombre = model.Nombre,
                    FechaDeNacimiento =Convert.ToDateTime(model.FechaDeNacimiento),

                };
                bool existo = true;

                existo = await _Personarepository.Create(ps);

                if (existo == true)
                {
                    return Json("Se agrego correctamente");

                }


            }
            catch (Exception ex)
            {

                throw new Exception($"Ha ocurrido un error {ex}");
            }
            return Ok();
        }

        //Eliminar Persona
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Remove([FromRoute] int? id)
        {
            if (id <= 0)
            {
                return BadRequest($"Usuario con el ID:{id}, no existe.");
            }
            bool existo = true;
            try
            {
                var people = await _Personarepository.GetFirst(f => f.ID == id);
                if (people==null)
                {
                    return NotFound("Persona no encontrada");

                }

                existo = await _Personarepository.Remove(people);
                if (existo == true)
                {
                    return Json("Se han eliminado correctamente");

                }
                else
                {
                    return Json("Ohh el registro no pudo ser eliminado correctamente");

                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Ha ocurrido un error {ex}");
            }


        }

        //Actualizar Persona
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] PersonaViewmodel modelo)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (modelo.ID <= 0)
            {
                return BadRequest();
            }

            var people = await _Personarepository.GetFirst(a => a.ID == modelo.ID);
            if (people!=null)
            {
                _context.Entry(people).State = EntityState.Detached;

            }

            if (people == null)
            {
                return NotFound("Persona no encontrada");
            }

            try
            {


                Persona current = new Persona
                {
                    ID = people.ID,
                    Nombre = modelo.Nombre,
                    FechaDeNacimiento =Convert.ToDateTime(modelo.FechaDeNacimiento),

                };
                bool existo = true;

                existo = await _Personarepository.Update(current);

                if (existo == true)
                {
                    return Json("Se Actualizo correctamente");

                }
                else
                {
                    return Json("No se Actualizo correctamente la persona "+modelo.Nombre);

                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Ha ocurrido un error {ex}");
            }

        }
    }
}
