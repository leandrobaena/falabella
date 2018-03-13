using Homecenter.Business;
using Homecenter.Entidades;
using System.Configuration;
using System.Web.Mvc;

namespace Homecenter.Controllers
{
    /// <summary>
    /// Página de inicio de sesión
    /// </summary>
    public class AccountController : Controller
    {
        #region Métodos
        /// <summary>
        /// Página de inicio de sesión
        /// </summary>
        /// <returns>Página de inicio de sesión</returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Valida si el login y contrseña de un usuario son correctos
        /// </summary>
        /// <param name="usuario">Datos del usuario</param>
        /// <returns>redirige a la página de principal si el usuario es válido o muestra un error en caso contrario</returns>
        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            UsuarioManager manager = new UsuarioManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            Usuario usuarioLogin = manager.Login(usuario.Login, usuario.Password);
            if (usuarioLogin != null && usuarioLogin.UsuarioId != 0)
            {
                Session["usuario"] = usuarioLogin;
                Session["perfiles"] = manager.ListarPerfiles(usuarioLogin);
                return new RedirectResult("~/Home/Index");
            }
            else
            {
                Session["usuario"] = null;
                Session["perfiles"] = null;
                ViewData["error"] = "Datos inválidos";
                return View();
            }
        }

        /// <summary>
        /// Cierra sesión
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session["usuario"] = null;
            Session["perfiles"] = null;
            return new RedirectResult("~/Account/Login");
        }
        #endregion
    }
}