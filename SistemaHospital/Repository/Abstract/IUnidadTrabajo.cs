namespace SistemaHospital.Repository.Abstract
{
    public interface IUnidadTrabajo : IDisposable // Para liberar recursos
    {
        ICargoRepositorio Cargo { get; }
        Task Guardar();
    }
}
