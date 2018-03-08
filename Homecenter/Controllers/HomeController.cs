using Homecenter.Business;
using Homecenter.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homecenter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarAsesores()
        {
            AsesorManager manager = new AsesorManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            var listado = manager.Listar(0, 10);
            return new JsonResult() { Data = listado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult InsertarAsesor(Asesor asesor)
        {
            AsesorManager manager = new AsesorManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            asesor = manager.Insertar(asesor);
            return new JsonResult() { Data = asesor };
        }

        public JsonResult ActualizarAsesor(Asesor asesor)
        {
            AsesorManager manager = new AsesorManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            asesor = manager.Actualizar(asesor);
            return new JsonResult() { Data = asesor };
        }

        public JsonResult EliminarAsesor(Asesor asesor)
        {
            AsesorManager manager = new AsesorManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            asesor = manager.Eliminar(asesor);
            return new JsonResult() { Data = asesor };
        }
    }
}