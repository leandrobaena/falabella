using Homecenter.Data;
using Homecenter.Entidades;
using System.Collections.Generic;
using System;

namespace Homecenter.Business
{
    /// <summary>
    /// Lógica de negocio para el manejo de las garantía
    /// </summary>
    public class GarantiaManager
    {
        #region Constructores
        /// <summary>
        /// Crea el objeto que maneja la lógica de negocio asociada a las garantías
        /// </summary>
        /// <param name="cadenaConexion">Cadena de conexión a la base de datos que persiste las garantías</param>
        public GarantiaManager(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Inserta una nueva garantía en la base de datos
        /// </summary>
        /// <param name="garantia">Garantía a insertar</param>
        /// <returns>Garantía insertada</returns>
        public Garantia Insertar(Garantia garantia)
        {
            GarantiaData data = new GarantiaData(this.cadenaConexion);
            return data.Insertar(garantia.Categoria, garantia.SKU, garantia.Descripcion, garantia.PrecioSinIva, garantia.PrecioConIva, garantia.PorcentajeComision);
        }

        /// <summary>
        /// Cuenta cuántas garantías existen en total
        /// </summary>
        /// <returns>Cuántas garantías existen en total</returns>
        public int Contar()
        {
            GarantiaData data = new GarantiaData(this.cadenaConexion);
            return data.Contar();
        }
        /// <summary>
        /// Lista las garantías
        /// </summary>
        /// <param name="inicio">Registro inicial</param>
        /// <param name="numRegistros">Número de registros a listar</param>
        /// <returns>Listado de garantías</returns>
        public List<Garantia> Listar(int inicio, int numRegistros)
        {
            GarantiaData data = new GarantiaData(this.cadenaConexion);
            return data.Listar(inicio, numRegistros);
        }

        /// <summary>
        /// Actualiza una garantía
        /// </summary>
        /// <param name="garantia">Garantía a actualizar</param>
        /// <returns>Asesor actualizado</returns>
        public Garantia Actualizar(Garantia garantia)
        {
            GarantiaData data = new GarantiaData(this.cadenaConexion);
            return data.Actualizar(garantia.GarantiaId, garantia.Categoria, garantia.SKU, garantia.Descripcion, garantia.PrecioSinIva, garantia.PrecioConIva, garantia.PorcentajeComision);
        }

        /// <summary>
        /// Elimina una garantía
        /// </summary>
        /// <param name="garantia">Garantía a eliminar</param>
        /// <returns>Garantía eliminada</returns>
        public Garantia Eliminar(Garantia garantia)
        {
            GarantiaData data = new GarantiaData(this.cadenaConexion);
            return data.Eliminar(garantia.GarantiaId);
        }

        /// <summary>
        /// Busca una garantía de acuerdo a su SKU
        /// </summary>
        /// <param name="sku">SKU por la que se busca la garantía</param>
        /// <returns>Garantía encontrada</returns>
        public Garantia BuscarXSKU(string sku)
        {
            GarantiaData data = new GarantiaData(this.cadenaConexion);
            return data.BuscarXSKU(sku);
        }
        #endregion

        #region Atributos
        /// <summary>
        /// Cadena de conexión en donde persisten las garantías
        /// </summary>
        private string cadenaConexion;
        #endregion
    }
}
