using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pre_entrega.Models
{
    public class ProductoVendido : Producto
    {
        public int Id { get; set; }
        public double Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }



        public ProductoVendido()
        {
            Id = 0;
            Stock = 0;
            IdProducto = 0;
            IdVenta = 0;
        }
        public ProductoVendido(int Id, int Stock, int IdProducto, int IdVenta, int Idp, string Descripciones) : base(Idp, Descripciones)
        {
            this.Id = 0;
            this.Stock = 0;
            this.IdProducto = 0;
            this.IdVenta = 0;

        }


    }
}
