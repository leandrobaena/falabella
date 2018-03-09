using System;

namespace Homecenter.Entidades
{
    /// <summary>
    /// Venta de un asesor de una garantía extendida
    /// </summary>
    public class Venta
    {
        #region Constructores
        /// <summary>
        /// Constructor por defecto, inicializa con valores por defecto
        /// </summary>
        public Venta()
        {
            this.VentaId = 0;
            this.AsesorId = 0;
            this.SKU = "";
            this.FechaRegistro = DateTime.MinValue;
        }

        /// <summary>
        /// Inicializa con valores por defecto y con el identificador determinado
        /// </summary>
        /// <param name="id">Identificador de la venta</param>
        public Venta(int id) : this()
        {
            this.VentaId = id;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Identificador de la venta
        /// </summary>
        public int VentaId { get; set; }

        /// <summary>
        /// Identificador del asesor que realiza la venta
        /// </summary>
        public int AsesorId { get; set; }

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
