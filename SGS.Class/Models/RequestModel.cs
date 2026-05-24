using SGS.Class.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Class.Models
{
    /// <summary>
    /// Representa una solicitud dentro del sistema de gestión de solicitudes.
    /// Contiene información general, estado, prioridad, categoría y usuarios relacionados.
    /// </summary>
    public class RequestModel : IEntity
    {
        /// <summary>
        /// Identificador único de la solicitud.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Título breve de la solicitud.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// Descripción detallada del problema o solicitud.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Ubicación donde ocurre el incidente o solicitud.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Fecha y hora en que se creó la solicitud.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Fecha y hora en que la solicitud fue cerrada.
        /// </summary>
        public DateTime? ClosedAt { get; set; }

        /// <summary>
        /// Estado actual de la solicitud.
        /// </summary>
        public RequestStatus Status { get; set; }

        /// <summary>
        /// Nivel de prioridad asignado a la solicitud.
        /// </summary>
        public RequestPriority Priority { get; set; }

        /// <summary>
        /// Identificador de la categoría asociada a la solicitud.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Categoría relacionada con la solicitud.
        /// </summary>
        public CategoryModel? Category { get; set; }

        /// <summary>
        /// Lista de comentarios asociados a la solicitud.
        /// </summary>
        public ICollection<CommentModel> Comments { get; set; } = new List<CommentModel>();

        /// <summary>
        /// Identificador del usuario asignado a resolver la solicitud.
        /// </summary>
        public string? AssignedUserId { get; set; }

        /// <summary>
        /// Usuario responsable asignado a la solicitud.
        /// </summary>
        public ApplicationUser? AssignedUser { get; set; }

        /// <summary>
        /// Identificador del usuario que creó la solicitud.
        /// </summary>
        public string? CreatedByUserId { get; set; }

        /// <summary>
        /// Usuario que creó la solicitud.
        /// </summary>
        public ApplicationUser? CreatedByUser { get; set; }
    }
}