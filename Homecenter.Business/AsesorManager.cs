using Homecenter.Data;
using Homecenter.Entidades;
using System.Collections.Generic;
using System;

namespace Homecenter.Business
{
    /// <summary>
    /// Lógica de negocio para el manejo de los asesores
    /// </summary>
    public class AsesorManager
    {
        #region Constructores
        /// <summary>
        /// Crea el objeto que maneja la lógica de negocio asociada a los asesores
        /// </summary>
        /// <param name="cadenaConexion">Cadena de conexión a la base de datos que persiste los asesores</param>
        public AsesorManager(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Inserta un nuevo asesor en la base de datos
        /// </summary>
        /// <param name="asesor">Asesor a insertar</param>
        /// <returns>Asesor insertado</returns>
        public Asesor Insertar(Asesor asesor)
        {
            AsesorData data = new AsesorData(this.cadenaConexion);
            return data.Insertar(asesor.Nombre, asesor.Cedula, asesor.Codigo, asesor.Tienda);
        }

        /// <summary>
        /// Lista los asesores
        /// </summary>
        /// <param name="inicio">Registro inicial</param>
        /// <param name="numRegistros">Número de registros a listar</param>
        /// <returns>Listado de asesores</returns>
        public List<Asesor> Listar(int inicio, int numRegistros)
        {
            AsesorData data = new AsesorData(this.cadenaConexion);
            return data.Listar(inicio, numRegistros);
        }

        /// <summary>
        /// Cuenta cuántos asesores existen en total
        /// </summary>
        /// <returns>Cuántos asesores existen en total</returns>
        public int Contar()
        {
            AsesorData data = new AsesorData(this.cadenaConexion);
            return data.Contar();
        }

        /// <summary>
        /// Actualiza un asesor
        /// </summary>
        /// <param name="asesor">Asesor a actualizar</param>
        /// <returns>Asesor actualizado</returns>
        public Asesor Actualizar(Asesor asesor)
        {
            AsesorData data = new AsesorData(this.cadenaConexion);
            return data.Actualizar(asesor.AsesorId, asesor.Nombre, asesor.Cedula, asesor.Codigo, asesor.Tienda);
        }

        /// <summary>
        /// Elimina un asesor
        /// </summary>
        /// <param name="asesor">Asesor a eliminar</param>
        /// <returns>Asesor eliminado</returns>
        public Asesor Eliminar(Asesor asesor)
        {
            AsesorData data = new AsesorData(this.cadenaConexion);
            return data.Eliminar(asesor.AsesorId);
        }

        /// <summary>
        /// Busca un asesor de acuerdo a su cédula
        /// </summary>
        /// <param name="cedula">Cédula por la que se busca el asesor</param>
        /// <returns>Asesor encontrado</returns>
        public Asesor BuscarXCedula(string cedula)
        {
            AsesorData data = new AsesorData(this.cadenaConexion);
            return data.BuscarXCedula(cedula);
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
