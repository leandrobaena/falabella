using System;

namespace Homecenter.Entidades
{
    /// <summary>
    /// Mestro de garantías extendidas
    /// </summary>
    public class MaestroGEXT
    {
        #region Constructores
        /// <summary>
        /// Constructor por defecto, inicializa con valores por defecto
        /// </summary>
        public MaestroGEXT()
        {
            this.MaestroGEXTId = 0;
            this.Categoria = "";
            this.SKU = "";
            this.Descripcion = "";
            this.PrecionSinIva = 0;
            this.PrecionConIva = 0;
            this.PorcentajeComision = 0;
        }

        /// <summary>
        /// Inicializa con valores por defecto y con el identificador determinado
        /// </summary>
        /// <param name="id">Identificador de la venta</param>
        public MaestroGEXT(int id) : this()
        {
            this.MaestroGEXTId = id;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Identificador del maestro de garantías extendidas
        /// </summary>
        public int MaestroGEXTId { get; set; }

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
        public float PrecionSinIva { get; set; }

        /// <summary>
        /// Precio de la garantía con IVA
        /// </summary>
        public float PrecionConIva { get; set; }

        /// <summary>
        /// Porcentaje de comisión del asesor
        /// </summary>
        public float PorcentajeComision { get; set; }
        #endregion
    }
}
