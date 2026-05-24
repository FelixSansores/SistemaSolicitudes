using SGS.Class.Interfaces;
using SGS.Class.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGS.Class.Enums;

namespace SGS.Class.Services
{
    /// <summary>
    /// Servicio encargado de la lógica de negocio relacionada
    /// con las solicitudes del sistema.
    /// </summary>
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _repository;

        /// <summary>
        /// Inicializa una nueva instancia del servicio RequestService.
        /// </summary>
        /// <param name="repository">
        /// Repositorio de solicitudes.
        /// </param>
        public RequestService(IRequestRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todas las solicitudes registradas.
        /// </summary>
        /// <returns>
        /// Lista de solicitudes.
        /// </returns>
        public async Task<List<RequestModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
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
        public async Task<RequestModel?> GetByIdAsync(int id)
        {
            return await _repository.GetbyIdAsync(id);
        }

        /// <summary>
        /// Registra una nueva solicitud en el sistema.
        /// </summary>
        /// <param name="request">
        /// Información de la solicitud a registrar.
        /// </param>
        public async Task CreateAsync(RequestModel request)
        {
            request.CreatedAt = DateTime.Now;

            await _repository.AddAsync(request);
        }

        /// <summary>
        /// Actualiza la información de una solicitud existente.
        /// </summary>
        /// <param name="request">
        /// Solicitud con la información actualizada.
        /// </param>
        public async Task UpdateAsync(RequestModel request)
        {
            await _repository.updateAsync(request);
        }

        /// <summary>
        /// Elimina una solicitud del sistema.
        /// </summary>
        /// <param name="id">
        /// Identificador de la solicitud a eliminar.
        /// </param>
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        /// <summary>
        /// Obtiene una solicitud junto con sus comentarios asociados.
        /// </summary>
        /// <param name="id">
        /// Identificador de la solicitud.
        /// </param>
        /// <returns>
        /// Solicitud con comentarios incluidos.
        /// </returns>
        public async Task<RequestModel?> GetWithCommentsAsync(int id)
        {
            return await _repository.GetWithCommentsAsync(id);
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
            return await _repository.GetFilteredAsync(
                status,
                priority,
                categoryId,
                assignedUserId);
        }
    }
}