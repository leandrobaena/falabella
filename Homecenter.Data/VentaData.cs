using Homecenter.Entidades;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using System.Globalization;
using System.Data.SqlClient;

namespace Homecenter.Data
{
    /// <summary>
    /// Administrador de persistencia para las ventas
    /// </summary>
    public class VentaData : DbContext
    {
        #region Constructores
        /// <summary>
        /// Crea un nuevo administrador de persistencia de las ventas
        /// </summary>
        /// <param name="cadenaConexion">Cadena de conexión a la base de datos</param>
        public VentaData(string cadenaConexion) : base(cadenaConexion)
        {
            this.ventas = this.Set<Venta>();
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Al crear el modelo de EF se asocia la tabla de ventas
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo de EF que se está generando</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Venta>().ToTable("Ventas");
        }

        /// <summary>
        /// Trae el listado de ventas de forma paginada
        /// </summary>
        /// <param name="inicio">Registro inicial a traer</param>
        /// <param name="numRecords">Número de regstros a traer</param>
        /// <returns></returns>
        public List<Venta> Listar(int inicio, int numRecords)
        {
            return ventas.OrderBy(x => x.AsesorId).Skip(inicio).Take(numRecords).ToList();
        }

        /// <summary>
        /// Crea una nueva venta en la base de datos
        /// </summary>
        /// <param name="asesorId">Identificador del asesor que realiza la venta</param>
        /// <param name="sku">Código de la garantía vendida</param>
        /// <returns>Venta insertada</returns>
        public Venta Insertar(int asesorId, string sku, string cedulaCliente, string skuElectrodomestico, decimal valorComisionado)
        {
            Venta venta = new Venta()
            {
                AsesorId = asesorId,
                SKU = sku,
                FechaRegistro = DateTime.Now,
                CedulaCliente = cedulaCliente,
                SKUElectrodomestico = skuElectrodomestico,
                ValorComision = valorComisionado
            };
            this.ventas.Add(venta);
            this.SaveChanges();
            return venta;
        }

        /// <summary>
        /// Reporte de ventas
        /// </summary>
        /// <param name="fechaInicio">Fecha desde la cual se genera el reporte</param>
        /// <param name="fechaFin">Fecha hasta la cual se genera el reporte</param>
        /// <returns>Ventas del reporte</returns>
        public List<ReporteVenta> DataReporte(string fechaInicio, string fechaFin)
        {
            DateTime fecIni = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime fecFin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture).AddDays(1);

            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
            parameters.Add(new SqlParameter("@fechaFin", fechaFin));

            List<ReporteVenta> reporte = this.Database.SqlQuery<ReporteVenta>("reporte_ventas @fechaInicio, @fechaFin", parameters.ToArray()).ToList();

            return reporte;
        }
        #endregion

        #region Atributos
        /// <summary>
        /// Entidad que representa la tabla en la base de datos
        /// </summary>
        public DbSet<Venta> ventas;
        #endregion
    }
}
