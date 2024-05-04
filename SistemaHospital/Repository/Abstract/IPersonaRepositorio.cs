using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaHospital.Models;

namespace SistemaHospital.Repository.Abstract
{
    public interface IPersonaRepositorio : IRepositorio<Persona>
    {
        void Actualizar(Persona persona);
    }
}