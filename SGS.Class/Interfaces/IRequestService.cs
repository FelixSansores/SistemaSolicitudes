using SGS.Class.Enums;
using SGS.Class.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Class.Interfaces
{
    /// <summary>
    /// Define las operaciones para la gestión de solicitudes
    /// dentro del sistema.
    /// </summary>
    public interface IRequestService
    {
        /// <summary>
        /// Obtiene la lista completa de solicitudes registradas.
        /// </summary>
        /// <returns>
        /// Lista de solicitudes.
        /// </returns>
        Task<List<RequestModel>> GetAllAsync();

        /// <summary>
        /// Obtiene una solicitud mediante su identificador.
        /// </summary>
        /// <param name="id">
        /// Identificador de la solicitud.
        /// </param>
        /// <returns>
        /// Solicitud encontrada o null si no existe.
        /// </returns>
        Task<RequestModel?> GetByIdAsync(int id);

        /// <summary>
        /// Registra una nueva solicitud en el sistema.
        /// </summary>
        /// <param name="request">
        /// Información de la solicitud a crear.
        /// </param>
        Task CreateAsync(RequestModel request);

        /// <summary>
        /// Actualiza la información de una solicitud existente.
        /// </summary>
        /// <param name="request">
        /// Solicitud con la información actualizada.
        /// </param>
        Task UpdateAsync(RequestModel request);

        /// <summary>
        /// Elimina una solicitud del sistema.
        /// </summary>
        /// <param name="id">
        /// Identificador de la solicitud a eliminar.
        /// </param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Obtiene una solicitud junto con sus comentarios asociados.
        /// </summary>
        /// <param name="id">
        /// Identificador de la solicitud.
        /// </param>
        /// <returns>
        /// Solicitud con comentarios incluidos.
        /// </returns>
        Task<RequestModel?> GetWithCommentsAsync(int id);

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
        Task<List<RequestModel>> GetFilteredAsync(
             RequestStatus? status,
             RequestPriority? priority,
             int? categoryId,
             string? assignedUserId);
    }
}