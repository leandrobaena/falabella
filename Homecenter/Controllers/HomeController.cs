using System.Web.Mvc;

namespace Homecenter.Controllers
{
    /// <summary>
    /// Página de inicio
    /// </summary>
    public class HomeController : Controller
    {
        #region Métodos
        /// <summary>
        /// Llamado de la página de inicio
        /// </summary>
        /// <returns>Página de inicio</returns>
        public ActionResult Index()
        {
            if (Session["usuario"] == null)
            {
                return new RedirectResult("/Account/Login");
            }
            return View();
        }
        #endregion
    }
}