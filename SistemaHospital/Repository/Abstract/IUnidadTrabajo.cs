namespace SistemaHospital.Repository.Abstract
{
    public interface IUnidadTrabajo : IDisposable // Para liberar recursos
    {
        ICargoRepositorio Cargo { get; }
        IEspecialidadRepositorio Especialidad { get; }
        ITipoEmpleadoRepositorio TipoEmpleado { get; }
        ITipoPagoRepositorio TipoPago { get; }
        IPacienteRepositorio Paciente { get; }
        ITipoExamenRepositorio TipoExamen { get; }
        ITipoTratamientoRepositorio TipoTratamiento { get; }
        Task GuardarCambios();
    }
}
