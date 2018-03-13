using Homecenter.Data;
using Homecenter.Entidades;
using System.Collections.Generic;
using System;

namespace Homecenter.Business
{
    /// <summary>
    /// Lógica de negocio para el manejo de las ventas
    /// </summary>
    public class VentaManager
    {
        #region Constructores
        /// <summary>
        /// Crea el objeto que maneja la lógica de negocio asociada a las ventas
        /// </summary>
        /// <param name="cadenaConexion">Cadena de conexión a la base de datos que persiste las ventas</param>
        public VentaManager(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Inserta una nueva venta en la base de datos
        /// </summary>
        /// <param name="venta">venta a insertar</param>
        /// <returns>Venta insertado</returns>
        public Venta Insertar(Venta venta)
        {
            VentaData data = new VentaData(this.cadenaConexion);
            return data.Insertar(venta.AsesorId, venta.SKU, venta.CedulaCliente, venta.SKUElectrodomestico, venta.ValorComision);
        }

        /// <summary>
        /// Lista las ventas
        /// </summary>
        /// <param name="inicio">Registro inicial</param>
        /// <param name="numRegistros">Número de registros a listar</param>
        /// <returns>Listado de ventas</returns>
        public List<Venta> Listar(int inicio, int numRegistros)
        {
            VentaData data = new VentaData(this.cadenaConexion);
            return data.Listar(inicio, numRegistros);
        }

        /// <summary>
        /// Reporte de ventas
        /// </summary>
        /// <param name="fechaInicio">Fecha desde la cual se genera el reporte</param>
        /// <param name="fechaFin">Fecha hasta la cual se genera el reporte</param>
        /// <returns>Ventas del reporte</returns>
        public List<ReporteVenta> GenerarReporte(string fechaInicio, string fechaFin)
        {
            VentaData data = new VentaData(this.cadenaConexion);
            return data.DataReporte(fechaInicio, fechaFin);
        }
        #endregion

        #region Atributos
        /// <summary>
        /// Cadena de conexión en donde persisten los asesores
        /// </summary>
        private string cadenaConexion;
        #endregion
    }
}
