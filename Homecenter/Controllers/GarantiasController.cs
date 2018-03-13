using Homecenter.Business;
using Homecenter.Entidades;
using System.Configuration;
using System.Web.Mvc;

namespace Homecenter.Controllers
{
    /// <summary>
    /// Controlador de la lista de garantías
    /// </summary>
    public class GarantiasController : Controller
    {
        #region Métodos
        /// <summary>
        /// Muestra el listado de garantias
        /// </summary>
        /// <returns>Listado de garantías</returns>
        public ActionResult Listado()
        {
            if (Session["usuario"] == null)
            {
                return new RedirectResult("/Account/Login");
            }
            return View();
        }

        /// <summary>
        /// Trae el listado de garantías
        /// </summary>
        /// <param name="inicio">Registro inicial</param>
        /// <param name="numRegistros">Número de registros a traer</param>
        /// <returns>Listado de garantías</returns>
        public JsonResult ListarGarantias(int inicio, int numRegistros)
        {
            GarantiaManager manager = new GarantiaManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            var listado = manager.Listar(inicio, numRegistros);
            return new JsonResult() { Data = listado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Inserta una garantia
        /// </summary>
        /// <param name="garantia">Garantía a insertar</param>
        /// <returns>Garantía insertada</returns>
        public JsonResult InsertarGarantia(Garantia garantia)
        {
            GarantiaManager manager = new GarantiaManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            garantia = manager.Insertar(garantia);
            return new JsonResult() { Data = garantia };
        }

        /// <summary>
        /// Actualiza una garantía
        /// </summary>
        /// <param name="garantia">Garantía a actualizar</param>
        /// <returns>Garantía actualizada</returns>
        public JsonResult ActualizarGarantia(Garantia garantia)
        {
            GarantiaManager manager = new GarantiaManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            garantia = manager.Actualizar(garantia);
            return new JsonResult() { Data = garantia };
        }

        /// <summary>
        /// Elimina una garantía
        /// </summary>
        /// <param name="garantia">Garantía a eliminar</param>
        /// <returns>Garantía eliminada</returns>
        public JsonResult EliminarGarantia(Garantia garantia)
        {
            GarantiaManager manager = new GarantiaManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            garantia = manager.Eliminar(garantia);
            return new JsonResult() { Data = garantia };
        }

        /// <summary>
        /// Busca una garantía de acuerdo con su SKU
        /// </summary>
        /// <param name="sku">SKU por el que se busca la garantía</param>
        /// <returns>Garantía encontrada si lo encuentra</returns>
        public JsonResult BuscarXSKU(string sku)
        {
            GarantiaManager manager = new GarantiaManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            Garantia garantia = manager.BuscarXSKU(sku);
            return new JsonResult() { Data = garantia };
        }

        #endregion
    }
}