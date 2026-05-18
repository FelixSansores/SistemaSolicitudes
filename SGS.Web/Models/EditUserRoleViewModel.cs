using System.ComponentModel.DataAnnotations;

namespace SGS.Web.Models
{
    public class EditUserRoleViewModel
    {
        public string UserId { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;
    }
}