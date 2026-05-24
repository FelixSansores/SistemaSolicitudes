using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Class.Models
{
    /// <summary>
    /// Representa un comentario realizado dentro de una solicitud.
    /// Permite registrar información adicional o seguimiento.
    /// </summary>
    public class CommentModel : IEntity
    {
        /// <summary>
        /// Identificador único del comentario.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Contenido o mensaje del comentario.
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// Fecha y hora en que se creó el comentario.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Identificador de la solicitud asociada al comentario.
        /// </summary>
        public int RequestId { get; set; }

        /// <summary>
        /// Solicitud relacionada con el comentario.
        /// </summary>
        public RequestModel Request { get; set; }
    }
}