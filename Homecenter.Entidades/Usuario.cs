namespace Homecenter.Entidades
{
    /// <summary>
    /// Usuario de la aplicación
    /// </summary>
    public class Usuario
    {
        #region Constructores
        /// <summary>
        /// Constructor por defecto, inicializa con valores por defecto
        /// </summary>
        public Usuario()
        {
            this.UsuarioId = 0;
            this.Login = "";
            this.Password = "";
        }

        /// <summary>
        /// Inicializa con valores por defecto y con el identificador determinado
        /// </summary>
        /// <param name="id">Identificador del usuario</param>
        public Usuario(int id) : this()
        {
            this.UsuarioId = id;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Identificador del usuario
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Login del usuario
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password del usuario
        /// </summary>
        public string Password { get; set; }
        #endregion
    }
}
