using System;

namespace Homecenter.Entidades
{
    /// <summary>
    /// Mestro de garantías extendidas
    /// </summary>
    public class Garantia
    {
        #region Constructores
        /// <summary>
        /// Constructor por defecto, inicializa con valores por defecto
        /// </summary>
        public Garantia()
        {
            this.GarantiaId = 0;
            this.Categoria = "";
            this.SKU = "";
            this.Descripcion = "";
            this.PrecioSinIva = 0;
            this.PrecioConIva = 0;
            this.PorcentajeComision = 0;
        }

        /// <summary>
        /// Inicializa con valores por defecto y con el identificador determinado
        /// </summary>
        /// <param name="id">Identificador de la venta</param>
        public Garantia(int id) : this()
        {
            this.GarantiaId = id;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Identificador del maestro de garantías extendidas
        /// </summary>
        public int GarantiaId { get; set; }

        /// <summary>
        /// Categoría de la garantía
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// SKU de la garantía
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// Descripción de la garantía
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Precio de la garantía sin IVA
        /// </summary>
        public decimal PrecioSinIva { get; set; }

        /// <summary>
        /// Precio de la garantía con IVA
        /// </summary>
        public decimal PrecioConIva { get; set; }

        /// <summary>
        /// Porcentaje de comisión del asesor
        /// </summary>
        public decimal PorcentajeComision { get; set; }
        #endregion
    }
}
