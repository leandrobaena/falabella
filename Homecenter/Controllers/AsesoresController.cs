﻿using Homecenter.Business;
using Homecenter.Entidades;
using System.Configuration;
using System.Web.Mvc;

namespace Homecenter.Controllers
{
    /// <summary>
    /// Controlador de la lista de asesores
    /// </summary>
    public class AsesoresController : Controller
    {
        #region Métodos
        /// <summary>
        /// Muestra el listado de asesores
        /// </summary>
        /// <returns>Listado de asesores</returns>
        public ActionResult Listado()
        {
            if (Session["usuario"] == null)
            {
                return new RedirectResult("~/Account/Login");
            }
            return View();
        }

        /// <summary>
        /// Trae el listado de asesores
        /// </summary>
        /// <param name="inicio">Registro inicial</param>
        /// <param name="numRegistros">Número de registros a traer</param>
        /// <returns>Listado de asesores</returns>
        public JsonResult ListarAsesores(int inicio, int numRegistros)
        {
            AsesorManager manager = new AsesorManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            var listado = manager.Listar(inicio, numRegistros);
            return new JsonResult() { Data = listado };
        }

        /// <summary>
        /// Trae el total de asesores
        /// </summary>
        /// <returns>Total de asesores</returns>
        public JsonResult ContarAsesores()
        {
            AsesorManager manager = new AsesorManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            int cantidad = manager.Contar();
            return new JsonResult() { Data = cantidad };
        }

        /// <summary>
        /// Inserta un asesor
        /// </summary>
        /// <param name="asesor">Asesor a insertar</param>
        /// <returns>Asesor insertado</returns>
        public JsonResult InsertarAsesor(Asesor asesor)
        {
            AsesorManager manager = new AsesorManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            asesor = manager.Insertar(asesor);
            UsuarioManager managerUsuario = new UsuarioManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            managerUsuario.InsertarAsesor(asesor);
            return new JsonResult() { Data = asesor };
        }

        /// <summary>
        /// Actualiza un asesor
        /// </summary>
        /// <param name="asesor">Asesor a actualizar</param>
        /// <returns>Asesor actualizado</returns>
        public JsonResult ActualizarAsesor(Asesor asesor)
        {
            AsesorManager manager = new AsesorManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            asesor = manager.Actualizar(asesor);
            return new JsonResult() { Data = asesor };
        }

        /// <summary>
        /// Elimina un asesor
        /// </summary>
        /// <param name="asesor">Asesor a eliminar</param>
        /// <returns>Asesor eliminado</returns>
        public JsonResult EliminarAsesor(Asesor asesor)
        {
            AsesorManager manager = new AsesorManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            asesor = manager.Eliminar(asesor);
            return new JsonResult() { Data = asesor };
        }

        /// <summary>
        /// Busca un asesor de acuerdo con su cédula
        /// </summary>
        /// <param name="cedula">Cédula por la que se busca el asesor</param>
        /// <returns>Asesor encontrado o asesor con datos por defecto si no lo encuentra</returns>
        public JsonResult BuscarXCedula(string cedula)
        {
            AsesorManager manager = new AsesorManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            Asesor asesor = manager.BuscarXCedula(cedula);
            return new JsonResult() { Data = asesor };
        }
        #endregion
    }
}