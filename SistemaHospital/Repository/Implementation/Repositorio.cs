using Microsoft.EntityFrameworkCore;
using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;
using System.Linq.Expressions;

namespace SistemaHospital.Repository.Implementation
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        // DbContext
        private readonly BdHospitalContext _context;
        // Entidad a manejar
        internal DbSet<T> dbSet;

        // Inyección
        public Repositorio(BdHospitalContext context)
        {
            _context = context;
            dbSet = _context.Set<T>(); // Establecemos el tipo que manejará DbSet<> (Entidad)
        }
        public async Task Agregar(T entidad) => await dbSet.AddAsync(entidad);


        public async Task<T> ObtenerPorId(int id)
        {
            var entidad = await dbSet.FindAsync(id);
            return entidad ?? throw new KeyNotFoundException($"No se encontró una entidad con ID {id}"); // Aplicación de operador de coalescencia
        }

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>>? filtro, string? incluirPropiedades, bool isTracking)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro); // Select * from table where
            }

            if (incluirPropiedades != null)
            {
                foreach (var item in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item.Trim()); // Obtener propiedades que vengan de otras tablas relacionadas
                }
            }

            if (!isTracking) // Determinar si el contexto de Entity Framework debe rastrear los cambios realizados a la entidad retornada
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>>? ordenarPor = null, string? incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro); // Select * from table where
            }

            if (incluirPropiedades != null)
            {
                foreach (var item in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item.Trim()); // Obtener propiedades que vengan de otras tablas relacionadas
                }
            }

            if (ordenarPor != null)
            {
                query = ordenarPor(query); // Aplicar ordenamiento
            }

            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public void Remover(T entidad)
        {
            dbSet.Remove(entidad);
        }

        public void RemoverRango(IEnumerable<T> entidades)
        {
            dbSet.RemoveRange(entidades);
        }
    }
}
