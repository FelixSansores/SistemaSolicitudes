using Microsoft.EntityFrameworkCore;
using SGS.Class;
using SGS.Class.Enums;
using SGS.Class.Interfaces;
using SGS.Class.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Class.Repositories
{
    /// <summary>
    /// Repositorio encargado de gestionar el acceso a datos
    /// de las solicitudes del sistema.
    /// </summary>
    public class RequestRepository : IRequestRepository
    {
        private readonly SGSDb _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio RequestRepository.
        /// </summary>
        /// <param name="context">
        /// Contexto de base de datos del sistema.
        /// </param>
        public RequestRepository(SGSDb context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las solicitudes registradas junto con
        /// su categoría y usuario asignado.
        /// </summary>
        /// <returns>
        /// Lista de solicitudes.
        /// </returns>
        public async Task<List<RequestModel>> GetAllAsync()
        {
            return await _context.Requests
                .Include(r => r.Category)
                .Include(r => r.AssignedUser)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene una solicitud mediante su identificador.
        /// </summary>
        /// <param name="id">
        /// Identificador de la solicitud.
        /// </param>
        /// <returns>
        /// Solicitud encontrada o null si no existe.
        /// </returns>
        public async Task<RequestModel?> GetbyIdAsync(int id)
        {
            return await _context.Requests.FindAsync(id);
        }

        /// <summary>
        /// Agrega una nueva solicitud a la base de datos.
        /// </summary>
        /// <param name="request">
        /// Solicitud a registrar.
        /// </param>
        public async Task AddAsync(RequestModel request)
        {
            await _context.Requests.AddAsync(request);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Actualiza la información de una solicitud existente.
        /// </summary>
        /// <param name="request">
        /// Solicitud con información actualizada.
        /// </param>
        public async Task updateAsync(RequestModel request)
        {
            _context.Requests.Update(request);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina una solicitud de la base de datos.
        /// </summary>
        /// <param name="id">
        /// Identificador de la solicitud a eliminar.
        /// </param>
        public async Task DeleteAsync(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request != null)
            {
                _context.Requests.Remove(request);

                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Obtiene una solicitud junto con su categoría,
        /// usuario asignado y comentarios asociados.
        /// </summary>
        /// <param name="id">
        /// Identificador de la solicitud.
        /// </param>
        /// <returns>
        /// Solicitud con información relacionada incluida.
        /// </returns>
        public async Task<RequestModel?> GetWithCommentsAsync(int id)
        {
            return await _context.Requests
                .Include(r => r.Category)
                .Include(r => r.AssignedUser)
                .Include(r => r.Comments)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        /// <summary>
        /// Obtiene una lista de solicitudes aplicando filtros
        /// por estado, prioridad, categoría y usuario asignado.
        /// </summary>
        /// <param name="status">
        /// Estado de la solicitud.
        /// </param>
        /// <param name="priority">
        /// Prioridad de la solicitud.
        /// </param>
        /// <param name="categoryId">
        /// Identificador de la categoría.
        /// </param>
        /// <param name="assignedUserId">
        /// Identificador del usuario asignado.
        /// </param>
        /// <returns>
        /// Lista de solicitudes filtradas.
        /// </returns>
        public async Task<List<RequestModel>> GetFilteredAsync(
            RequestStatus? status,
            RequestPriority? priority,
            int? categoryId,
            string? assignedUserId)
        {
            var query = _context.Requests
                .Include(r => r.Category)
                .Include(r => r.AssignedUser)
                .AsQueryable();

            if (status.HasValue)
                query = query.Where(r => r.Status == status.Value);

            if (priority.HasValue)
                query = query.Where(r => r.Priority == priority.Value);

            if (categoryId.HasValue)
                query = query.Where(r => r.CategoryId == categoryId.Value);

            if (!string.IsNullOrEmpty(assignedUserId))
                query = query.Where(r => r.AssignedUserId == assignedUserId);

            return await query.ToListAsync();
        }
    }
}