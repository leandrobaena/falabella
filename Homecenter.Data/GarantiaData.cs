using Homecenter.Entidades;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Homecenter.Data
{
    /// <summary>
    /// Administrador de persistencia para el maestro de garantías extendidas
    /// </summary>
    public class GarantiaData : DbContext
    {
        #region Constructores
        /// <summary>
        /// Crea un nuevo administrador de persistencia del maestro de garantías extendidas
        /// </summary>
        /// <param name="cadenaConexion">Cadena de conexion a la base de datos</param>
        public GarantiaData(string cadenaConexion) : base(cadenaConexion)
        {
            this.garantias = this.Set<Garantia>();
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Al crear el modelo de EF se asocia la tabla de garantías
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo de EF que se está generando</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Garantia>().ToTable("Garantias");
        }

        /// <summary>
        /// Trae el total de garantías
        /// </summary>
        /// <returns>Total de garantías</returns>
        public int Contar()
        {
            return garantias.Count();
        }

        /// <summary>
        /// Trae el listado de garantías de forma paginada
        /// </summary>
        /// <param name="inicio">Registro inicial a traer</param>
        /// <param name="numRecords">Número de regstros a traer</param>
        /// <returns>Listado de garantías</returns>
        public List<Garantia> Listar(int inicio, int numRecords)
        {
            return garantias.OrderBy(x => x.GarantiaId).Skip(inicio).Take(numRecords).ToList();
        }

        /// <summary>
        /// Actualiza una garantía
        /// </summary>
        /// <param name="garantiaId">Identificador de la garantía</param>
        /// <param name="categoria">Categoría de la garantía</param>
        /// <param name="sku">SKU de la garantía</param>
        /// <param name="descripcion">Descripción de la garantía</param>
        /// <param name="precioSinIVA">Precio sin IVA</param>
        /// <param name="precioConIVA">Precio con IVA</param>
        /// <param name="porcentajeComision">Porcentaje de comisión</param>
        /// <returns>Garantía actualizada</returns>
        public Garantia Actualizar(int garantiaId, string categoria, string sku, string descripcion, decimal precioSinIVA, decimal precioConIVA, decimal porcentajeComision)
        {
            Garantia garantia = this.garantias.Where(x => x.GarantiaId == garantiaId).FirstOrDefault();
            garantia.Categoria = categoria;
            garantia.SKU = sku;
            garantia.Descripcion = descripcion;
            garantia.PrecioSinIva = precioSinIVA;
            garantia.PrecioConIva = precioConIVA;
            garantia.PorcentajeComision = porcentajeComision;
            this.SaveChanges();
            return garantia;
        }

        /// <summary>
        /// Elimina una garantía de la base de datos
        /// </summary>
        /// <param name="garantiaId">Identificador de la garantía</param>
        /// <returns>Garantía eliminada</returns>
        public Garantia Eliminar(int garantiaId)
        {
            Garantia garantia = this.garantias.Where(x => x.GarantiaId == garantiaId).FirstOrDefault();
            this.garantias.Remove(garantia);
            this.SaveChanges();
            return garantia;
        }

        /// <summary>
        /// Crea una nueva garantía en la base de datos
        /// </summary>
        /// <param name="categoria">Categoría de la garantía</param>
        /// <param name="sku">SKU de la garantía</param>
        /// <param name="descripcion">Descripción de la garantía</param>
        /// <param name="precioSinIVA">Precio sin IVA</param>
        /// <param name="precioConIVA">Precio con IVA</param>
        /// <param name="porcentajeComision">Porcentaje de comisión</param>
        /// <returns>Garantía insertada</returns>
        public Garantia Insertar(string categoria, string sku, string descripcion, decimal precioSinIVA, decimal precioConIVA, decimal porcentajeComision)
        {
            Garantia garantia = new Garantia()
            {
                Categoria = categoria,
                SKU = sku,
                Descripcion = descripcion,
                PrecioSinIva = precioSinIVA,
                PrecioConIva = precioConIVA,
                PorcentajeComision = porcentajeComision
            };
            this.garantias.Add(garantia);
            this.SaveChanges();
            return garantia;
        }

        /// <summary>
        /// Busca una garantía por el SKU
        /// </summary>
        /// <param name="sku">SKU por el que se quiere buscar</param>
        /// <returns>Retorna la garantía si la encuentra</returns>
        public Garantia BuscarXSKU(string sku)
        {
            return garantias.Where(x => x.SKU.Equals(sku)).FirstOrDefault();
        }
        #endregion

        #region Atributos
        /// <summary>
        /// Entidad que representa la tabla en la base de datos
        /// </summary>
        public DbSet<Garantia> garantias;
        #endregion
    }
}
