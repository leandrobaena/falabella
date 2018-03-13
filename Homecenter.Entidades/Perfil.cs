namespace Homecenter.Entidades
{
    /// <summary>
    /// Perfil de los usuarios de la aplicación
    /// </summary>
    public class Perfil
    {
        #region Constructores
        /// <summary>
        /// Constructor por defecto, inicializa con valores por defecto
        /// </summary>
        public Perfil()
        {
            this.PerfilId = 0;
            this.Nombre = "";
        }

        /// <summary>
        /// Inicializa con valores por defecto y con el identificador determinado
        /// </summary>
        /// <param name="id">Identificador del perfil</param>
        public Perfil(int id) : this()
        {
            this.PerfilId = id;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Identificador del perfil
        /// </summary>
        public int PerfilId { get; set; }

        /// <summary>
        /// Nombre del perfil
        /// </summary>
        public string Nombre { get; set; }
        #endregion
    }
}
