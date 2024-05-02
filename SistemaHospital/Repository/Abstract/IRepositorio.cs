using System.Linq.Expressions;

namespace SistemaHospital.Repository.Abstract
{
    public interface IRepositorio<T> where T : class
    {
        Task<T> ObtenerPorId(int id); // Buscar un registro por su id

        Task<IEnumerable<T>> ObtenerTodos(
            Expression<Func<T, bool>>? filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? ordenarPor = null,
            string? incluirPropiedades = null,
            bool isTracking = true // Obtener datos y modificarlos también
        );

        Task<T> ObtenerPrimero(
            Expression<Func<T, bool>>? filtro = null,
            string? incluirPropiedades = null,
            bool isTracking = true // Obtener datos y modificarlos también
        );

        Task Agregar(T entidad);

        // Los métodos para eliminar serán síncronos
        void Remover(T entidad);

        void RemoverRango(IEnumerable<T> entidades);

    }
}
