using Homecenter.Entidades;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace Homecenter.Data
{
    /// <summary>
    /// Administrador de persistencia para los asesores
    /// </summary>
    public class AsesorData : DbContext
    {
        #region Constructores
        /// <summary>
        /// Crea un nuevo administrador de persistencia de los asesores
        /// </summary>
        /// <param name="cadenaConexion">Cadena de conexion a la base de datos</param>
        public AsesorData(string cadenaConexion) : base(cadenaConexion)
        {
            this.asesores = this.Set<Asesor>();
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Al crear el modelo de EF se asocia la tabla de asesores
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo de EF que se está generando</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asesor>().ToTable("Asesores");
        }

        /// <summary>
        /// Trae el listado de asesores de forma paginada
        /// </summary>
        /// <param name="inicio">Registro inicial a traer</param>
        /// <param name="numRecords">Número de regstros a traer</param>
        /// <returns>Listado de asesores</returns>
        public List<Asesor> Listar(int inicio, int numRecords)
        {
            return asesores.OrderBy(x => x.AsesorId).Skip(inicio).Take(numRecords).ToList();
        }

        /// <summary>
        /// Actualiza un asesor
        /// </summary>
        /// <param name="asesorId">Identificador del asesor</param>
        /// <param name="nombre">Nombre del asesor</param>
        /// <param name="cedula">Cédula del asesor</param>
        /// <param name="codigo">Código del asesor</param>
        /// <param name="tienda">Tienda del asesor</param>
        /// <returns>Asesor actualizado</returns>
        public Asesor Actualizar(int asesorId, string nombre, string cedula, string codigo, string tienda)
        {
            Asesor asesor = this.asesores.Where(x => x.AsesorId == asesorId).FirstOrDefault();
            asesor.Nombre = nombre;
            asesor.Cedula = cedula;
            asesor.Codigo = codigo;
            asesor.Tienda = tienda;
            this.SaveChanges();
            return asesor;
        }

        /// <summary>
        /// Elimina un asesor de la base de datos
        /// </summary>
        /// <param name="asesorId">Identificador del asesor</param>
        /// <returns>Asesor eliminado</returns>
        public Asesor Eliminar(int asesorId)
        {
            Asesor asesor = this.asesores.Where(x => x.AsesorId == asesorId).FirstOrDefault();
            this.asesores.Remove(asesor);
            this.SaveChanges();
            return asesor;
        }

        /// <summary>
        /// Busca un asesor por el número de cédula
        /// </summary>
        /// <param name="cedula">Cédula por la que se quiere buscar</param>
        /// <returns>Retorna el asesor encontrado o un asesor con datos por defecto si no lo encuentra</returns>
        public Asesor BuscarXCedula(string cedula)
        {
            return asesores.Where(x => x.Cedula.Equals(cedula)).FirstOrDefault();
        }

        /// <summary>
        /// Crea un nuevo asesor en la base de datos
        /// </summary>
        /// <param name="nombre">Nombre del asesor</param>
        /// <param name="cedula">Cédula del asesor</param>
        /// <param name="codigo">Código del asesor</param>
        /// <param name="tienda">Tienda asociada al asesor</param>
        /// <returns>Asesor insertado</returns>
        public Asesor Insertar(string nombre, string cedula, string codigo, string tienda)
        {
            Asesor asesor = new Asesor()
            {
                Nombre = nombre,
                Cedula = cedula,
                Codigo = codigo,
                Tienda = tienda
            };
            this.asesores.Add(asesor);
            this.SaveChanges();
            return asesor;
        }
        #endregion

        #region Atributos
        /// <summary>
        /// Entidad que representa la tabla en la base de datos
        /// </summary>
        public DbSet<Asesor> asesores;
        #endregion
    }
}
