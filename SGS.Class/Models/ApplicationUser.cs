using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SGS.Class.Models
{
    /// <summary>
    /// Representa un usuario del sistema.
    /// Hereda las propiedades y funcionalidades de IdentityUser.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Nombre completo del usuario.
        /// </summary>
        [Required]
        public string FullName { get; set; }
    }
}