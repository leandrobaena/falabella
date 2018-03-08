using Homecenter.Data;
using Homecenter.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homecenter.Business
{
    public class AsesorManager
    {
        public AsesorManager(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        /// <summary>
        /// Inserta un nuevo asesor en la base de datos
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="cedula"></param>
        /// <param name="codigo"></param>
        /// <param name="tienda"></param>
        /// <param name="cadenaConexion"></param>
        /// <returns></returns>
        public Asesor Insertar(Asesor asesor)
        {
            AsesorData data = new AsesorData(this.cadenaConexion);
            return data.Insertar(asesor.Nombre, asesor.Cedula, asesor.Codigo, asesor.Tienda);
        }

        public List<Asesor> Listar(int inicio, int numRegistros)
        {
            AsesorData data = new AsesorData(this.cadenaConexion);
            return data.Listar(inicio, numRegistros);
        }

        public Asesor Actualizar(Asesor asesor)
        {
            AsesorData data = new AsesorData(this.cadenaConexion);
            return data.Actualizar(asesor.AsesorId, asesor.Nombre, asesor.Cedula, asesor.Codigo, asesor.Tienda);
        }

        public Asesor Eliminar(Asesor asesor)
        {
            AsesorData data = new AsesorData(this.cadenaConexion);
            return data.Eliminar(asesor.AsesorId);
        }

        private string cadenaConexion;
    }
}
