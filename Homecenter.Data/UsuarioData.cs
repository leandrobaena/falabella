using Homecenter.Entidades;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using System.Data.SqlClient;

namespace Homecenter.Data
{
    /// <summary>
    /// Administrador de persistencia para los usuarios
    /// </summary>
    public class UsuarioData : DbContext
    {
        #region Constructores
        /// <summary>
        /// Crea un nuevo administrador de persistencia de los usuarios
        /// </summary>
        /// <param name="cadenaConexion">Cadena de conexion a la base de datos</param>
        public UsuarioData(string cadenaConexion) : base(cadenaConexion)
        {
            this.usuarios = this.Set<Usuario>();
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Al crear el modelo de EF se asocia la tabla de usuarios
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo de EF que se está generando</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asesor>().ToTable("Usuarios");
        }

        /// <summary>
        /// Busca un asesor por el número de cédula
        /// </summary>
        /// <param name="cedula">Cédula por la que se quiere buscar</param>
        /// <returns>Retorna el asesor encontrado o un asesor con datos por defecto si no lo encuentra</returns>
        public Usuario Login(string login, string password)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@login", login));
            parameters.Add(new SqlParameter("@password", password));

            Usuario usuario = this.Database.SqlQuery<Usuario>("login_usuario @login, @password", parameters.ToArray()).FirstOrDefault();

            return usuario;
        }

        /// <summary>
        /// Trae los perfiles a los que está asociado el usuario
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario a consultar</param>
        /// <returns></returns>
        public List<Perfil> ListarPerfiles(int usuarioId)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@usuarioId", usuarioId));

            List<Perfil> perfiles = this.Database.SqlQuery<Perfil>("perfiles_usuario @usuarioId", parameters.ToArray()).ToList();

            return perfiles;
        }

        /// <summary>
        /// Crea un nuevo usuario en la base de datos
        /// </summary>
        /// <param name="login">Login del usuario</param>
        /// <param name="cedula">Password del usuario</param>
        /// <returns>Usuario insertado</returns>
        public void Insertar(string login, string password)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@login", login));
            parameters.Add(new SqlParameter("@password", password));

            Usuario u = this.Database.SqlQuery<Usuario>("crear_usuario @login, @password", parameters.ToArray()).FirstOrDefault();

            this.Database.ExecuteSqlCommand("INSERT INTO usuariosperfiles (UsuarioId, PerfilId) VALUES (" + u.UsuarioId + ", 2)");
        }
        #endregion

        #region Atributos
        /// <summary>
        /// Entidad que representa la tabla en la base de datos
        /// </summary>
        public DbSet<Usuario> usuarios;
        #endregion
    }
}
