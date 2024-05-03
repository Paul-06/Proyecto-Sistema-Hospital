using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Repository.Implementation
{
    public class PersonaRepositorio : Repositorio<Persona>, IPersonaRepositorio
    {
        // Atributo para el DbContext
        private readonly BdHospitalContext _context;

        // Inyección en el constructor
        public PersonaRepositorio(BdHospitalContext context) : base(context) // Enviar el context al padre
        {
            _context = context;
        }

        public void Actualizar(Persona persona)
        {
            var registro = _context.Personas.FirstOrDefault(p => p.IdPersona == persona.IdPersona); // Buscar el registro a actualizar

            if (registro != null)
            {
                registro.Dni = persona.Dni;
                registro.ApellidoPaterno = persona.ApellidoPaterno;
                registro.ApellidoMaterno = persona.ApellidoMaterno;
                registro.Nombres = persona.Nombres;
                registro.FechaNacimiento = persona.FechaNacimiento;
                registro.Celular = persona.Celular;
                registro.Correo = persona.Correo;
                registro.Direccion = persona.Direccion;
            }
        }
    }
}