using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pre_entrega.Models
{
    public class Producto
    {
        public int Idp { get; set; }
        public string Descripciones { get; set; }
        public double Costo { get; set; }
        public double PrecioVenta { get; set; }
        public double Stock { get; set; }
        public int IdUsuario { get; set; }

        public Producto()
        {
            Idp = 0;
            Descripciones = string.Empty;
            Costo = 0;
            PrecioVenta = 0;
            Stock = 0;
            IdUsuario = 0;

        }
        public Producto(int idp, string descripciones)
        {
            Idp = idp;
            Descripciones = descripciones;
        }
    }
}
