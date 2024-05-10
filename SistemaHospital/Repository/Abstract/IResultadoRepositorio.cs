using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaHospital.Models;

namespace SistemaHospital.Repository.Abstract
{
    public interface IResultadoRepositorio : IRepositorio<Resultado>
    {
        void Actualizar(Resultado resultado);
    }
}