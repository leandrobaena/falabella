using System;

namespace Homecenter.Entidades
{
    /// <summary>
    /// Entidad asociada al reporte de ventas de un asesor de una garantía extendida
    /// </summary>
    public class ReporteVenta
    {
        #region Propiedades
        /// <summary>
        /// Identificador de la venta
        /// </summary>
        public int VentaId { get; set; }

        /// <summary>
        /// Nombre del asesor
        /// </summary>
        public string Asesor { get; set; }

        /// <summary>
        /// Cédula del asesor
        /// </summary>
        public string Cedula { get; set; }

        /// <summary>
        /// Código del asesor
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Tienda asociada al asesor
        /// </summary>
        public string Tienda { get; set; }

        /// <summary>
        /// Código SKU del producto vendido
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// Fecha de la venta
        /// </summary>
        public DateTime FechaRegistro { get; set; }
        #endregion
    }
}
