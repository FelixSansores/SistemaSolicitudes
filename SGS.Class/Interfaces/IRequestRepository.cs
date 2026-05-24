using Azure.Core;
using SGS.Class;
using SGS.Class.Enums;
using SGS.Class.Interfaces;
using SGS.Class.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Class.Interfaces
{
    /// <summary>
    /// Define las operaciones de acceso a datos para las solicitudes
    /// del sistema.
    /// </summary>
    public interface IRequestRepository
    {
        /// <summary>
        /// Obtiene todas las solicitudes registradas.
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
        Task<RequestModel?> GetbyIdAsync(int id);

        /// <summary>
        /// Agrega una nueva solicitud a la base de datos.
        /// </summary>
        /// <param name="request">
        /// Solicitud a registrar.
        /// </param>
        Task AddAsync(RequestModel request);

        /// <summary>
        /// Actualiza la información de una solicitud existente.
        /// </summary>
        /// <param name="request">
        /// Solicitud con información actualizada.
        /// </param>
        Task updateAsync(RequestModel request);

        /// <summary>
        /// Elimina una solicitud de la base de datos.
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