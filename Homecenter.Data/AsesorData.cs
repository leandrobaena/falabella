using Homecenter.Entidades;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace Homecenter.Data
{
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
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asesor>().ToTable("Asesores");
        }

        /// <summary>
        /// Trae el litado de asesores de forma paginada
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="numRecords"></param>
        /// <returns></returns>
        public List<Asesor> Listar(int offset, int numRecords)
        {
            return asesores.ToList();
        }

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

        public Asesor Eliminar(int asesorId)
        {
            Asesor asesor = this.asesores.Where(x => x.AsesorId == asesorId).FirstOrDefault();
            this.asesores.Remove(asesor);
            this.SaveChanges();
            return asesor;
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
