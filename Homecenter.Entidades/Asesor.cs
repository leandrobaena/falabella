using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homecenter.Entidades
{
    public class Asesor
    {
        #region Constructores
        /// <summary>
        /// Constructor por defecto, inicializa con valores por defecto
        /// </summary>
        public Asesor()
        {
            this.AsesorId = 0;
            this.Nombre = "";
            this.Cedula = "";
            this.Codigo = "";
            this.Tienda = "";
        }

        /// <summary>
        /// Inicializa con valores por defecto y con el identificador determinado
        /// </summary>
        /// <param name="id">Identificador del asesor</param>
        public Asesor(int id) : this()
        {
            this.AsesorId = id;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Identificador del asesor
        /// </summary>
        public int AsesorId { get; set; }

        /// <summary>
        /// Nombre del asesor
        /// </summary>
        public string Nombre { get; set; }

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
        #endregion
    }
}
