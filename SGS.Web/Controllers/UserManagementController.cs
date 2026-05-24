using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SGS.Class.Models;
using SGS.Web.Models;

namespace SGS.Web.Controllers
{
    /// <summary>
    /// Controlador encargado de la administración de usuarios y roles.
    /// Permite crear usuarios, visualizar usuarios registrados y modificar roles.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Inicializa una nueva instancia del controlador UserManagementController.
        /// </summary>
        /// <param name="userManager">
        /// Administrador de usuarios del sistema.
        /// </param>
        /// <param name="roleManager">
        /// Administrador de roles del sistema.
        /// </param>
        public UserManagementController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Muestra la lista de usuarios registrados junto con sus roles.
        /// </summary>
        /// <returns>
        /// Vista con la lista de usuarios y roles asignados.
        /// </returns>
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();

            var userList = new List<(ApplicationUser User, IList<string> Roles)>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userList.Add((user, roles));
            }

            return View(userList);
        }

        /// <summary>
        /// Muestra el formulario para crear un nuevo usuario.
        /// </summary>
        /// <returns>
        /// Vista del formulario de creación de usuario.
        /// </returns>
        public IActionResult CreateUser()
        {
            ViewBag.Roles = _roleManager.Roles.ToList();

            return View();
        }

        /// <summary>
        /// Crea un nuevo usuario en el sistema y le asigna un rol.
        /// </summary>
        /// <param name="model">
        /// Información del usuario a registrar.
        /// </param>
        /// <returns>
        /// Redirección al listado de usuarios o regreso al formulario en caso de error.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _roleManager.Roles.ToList();

                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);

                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            ViewBag.Roles = _roleManager.Roles.ToList();

            return View(model);
        }

        /// <summary>
        /// Muestra el formulario para editar el rol de un usuario.
        /// </summary>
        /// <param name="id">
        /// Identificador del usuario.
        /// </param>
        /// <returns>
        /// Vista para modificar el rol del usuario.
        /// </returns>
        public async Task<IActionResult> EditRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);

            var model = new EditUserRoleViewModel
            {
                UserId = user.Id,
                Email = user.Email ?? "",
                FullName = user.FullName,
                Role = roles.FirstOrDefault() ?? ""
            };

            ViewBag.Roles = _roleManager.Roles.ToList();

            return View(model);
        }

        /// <summary>
        /// Actualiza el rol asignado a un usuario.
        /// </summary>
        /// <param name="model">
        /// Información del usuario y nuevo rol asignado.
        /// </param>
        /// <returns>
        /// Redirección al listado de usuarios.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> EditRole(EditUserRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _roleManager.Roles.ToList();

                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, model.Role);

            return RedirectToAction(nameof(Index));
        }
    }
}