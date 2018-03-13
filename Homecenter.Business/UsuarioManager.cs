using Homecenter.Data;
using Homecenter.Entidades;
using System.Collections.Generic;
using System;

namespace Homecenter.Business
{
    /// <summary>
    /// Lógica de negocio para el manejo de los usuario
    /// </summary>
    public class UsuarioManager
    {
        #region Constructores
        /// <summary>
        /// Crea el objeto que maneja la lógica de negocio asociada a lo usuarios
        /// </summary>
        /// <param name="cadenaConexion">Cadena de conexión a la base de datos que persiste las ventas</param>
        public UsuarioManager(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Validar el login de un usuario
        /// </summary>
        /// <param name="login">Login del usuario</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <returns>Usuario validado</returns>
        public Usuario Login(string login, string password)
        {
            UsuarioData data = new UsuarioData(this.cadenaConexion);
            return data.Login(login, password);
        }

        /// <summary>
        /// Lista los perfiles asociados a un usuario
        /// </summary>
        /// <param name="usuarioLogin">Usuario al que se le consulta los perfiles</param>
        /// <returns></returns>
        public List<Perfil> ListarPerfiles(Usuario usuarioLogin)
        {
            UsuarioData data = new UsuarioData(this.cadenaConexion);
            return data.ListarPerfiles(usuarioLogin.UsuarioId);
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
