using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using pre_entrega.Models;

namespace pre_entrega
{
    class Program
    {
        static void Main(string[] args)
        {


            SqlConnectionStringBuilder connectionbuilder = new();
            connectionbuilder.DataSource = "DESKTOP-TA5P0R7";
            connectionbuilder.InitialCatalog = "SistemaGestion";
            connectionbuilder.IntegratedSecurity = true;
            var connectionString = connectionbuilder.ConnectionString;

            // -------- MENU -------------
            while (true)
            {
                Console.WriteLine("Ingrese una opcion: ");
                Console.WriteLine("A) Traer usuario.");
                Console.WriteLine("B) Traer producto.");
                Console.WriteLine("C) Traer productos vendidos.");
                Console.WriteLine("D) Traer ventas.");
                Console.WriteLine("E) Iniciar sesion.");
                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "a" or "A":
                        {
                            Console.WriteLine("ingrese nombre de usuario: ");
                            string nombUsu = Console.ReadLine();
                            TraerUsuario(nombUsu);
                            break;
                        }
                    case "b" or "B":
                        {
                            Console.WriteLine("ingrese ID de usuario: ");
                            int idUsu = int.Parse(Console.ReadLine());
                            TraerProducto(idUsu);
                            break;
                        }

                    case "c" or "C":
                        {
                            Console.WriteLine("ingrese ID de usuario: ");
                            int idUs = int.Parse(Console.ReadLine());
                            TraerProductosVendidos(idUs);
                            break;
                        }

                    case "d" or "D":
                        {
                            Console.WriteLine("ingrese ID de usuario: ");
                            int idUsua = int.Parse(Console.ReadLine());
                            TraerVentas(idUsua);
                            break;
                        }
                    case "e" or "E":
                        {
                            Console.WriteLine("ingrese Nombre de usuario: ");
                            string nombreIni = Console.ReadLine();
                            Console.WriteLine("ingrese Contraseña: ");
                            string usuContra = Console.ReadLine();
                            IniciarSesion(nombreIni, usuContra);
                            break;
                        }
                }
            }
        }
        public static void IniciarSesion(string nombreIni, string usuContra)
        {
            SqlConnectionStringBuilder connectionbuilder = new();
            connectionbuilder.DataSource = "DESKTOP-TA5P0R7";
            connectionbuilder.InitialCatalog = "SistemaGestion";
            connectionbuilder.IntegratedSecurity = true;
            var connectionString = connectionbuilder.ConnectionString;
            string query4 = "select * from Usuario where NombreUsuario = @usu and Contraseña = @cont";
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(query4, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("usu", nombreIni));
                    cmd.Parameters.Add(new SqlParameter("cont", usuContra));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        while (true)
                        {
                            Usuario us = new Usuario();
                            us.Id = Convert.ToInt32(reader.GetValue(0));
                            us.Nombre = reader.GetString(1);
                            us.Apellido = reader.GetString(2);
                            us.NombreUsuario = reader.GetString(3);
                            us.Contraseña = reader.GetString(4);
                            us.Mail = reader.GetString(5);


                            if (us.NombreUsuario == nombreIni & us.Contraseña == usuContra)
                            {
                                Console.WriteLine("-----------Ingreso Exitoso---------");
                                Console.WriteLine("ID Usuario: " + us.Id);
                                Console.WriteLine("Nombre: " + us.Nombre);
                                Console.WriteLine("Apellido: " + us.Apellido);
                                Console.WriteLine("Nombre de Usuario: " + us.NombreUsuario);
                                Console.WriteLine("Contraseña: " + us.Contraseña);
                                Console.WriteLine("Mail: " + us.Mail);
                                Console.WriteLine("-----------------------------------");

                                break;
                            }
                        }
                    }
                    else
                    {

                        Usuario nullo = new Usuario();
                        Console.WriteLine("---------Ingreso fallido.----------");
                        Console.WriteLine("ID Usuario: " + nullo.Id);
                        Console.WriteLine("Nombre: " + nullo.Nombre);
                        Console.WriteLine("Apellido: " + nullo.Apellido);
                        Console.WriteLine("Nombre de Usuario: " + nullo.NombreUsuario);
                        Console.WriteLine("Contraseña: " + nullo.Contraseña);
                        Console.WriteLine("Mail: " + nullo.Mail);
                        Console.WriteLine("-----------------------------------");

                    }

                }

            }

        }
        public static void TraerVentas(int idUsua)
        {
            SqlConnectionStringBuilder connectionbuilder = new();
            connectionbuilder.DataSource = "DESKTOP-TA5P0R7";
            connectionbuilder.InitialCatalog = "SistemaGestion";
            connectionbuilder.IntegratedSecurity = true;
            var connectionString = connectionbuilder.ConnectionString;
            string query3 = "Select * from Venta where IdUsuario = @IdUsu";
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(query3, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@IdUsu", idUsua));
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Venta ve = new Venta();
                        ve.Id = Convert.ToInt32(reader.GetValue(0));
                        ve.Comentarios = reader.GetString(1);
                        ve.IdUsuario = Convert.ToInt32(reader.GetValue(2));
                        var listaVentas = new List<Venta>();
                        listaVentas.Add(ve);

                        Console.WriteLine("--------RESULTADO--------");
                        Console.WriteLine("Venta numero: " + ve.Id);
                        Console.WriteLine("Comentarios: " + ve.Comentarios);
                        Console.WriteLine("ID del usuario que realizo la venta: " + ve.IdUsuario);
                        Console.WriteLine("----------------------");

                    }
                }
            }

        }
        public static void TraerProductosVendidos(int idUs)
        {
            SqlConnectionStringBuilder connectionbuilder = new();
            connectionbuilder.DataSource = "DESKTOP-TA5P0R7";
            connectionbuilder.InitialCatalog = "SistemaGestion";
            connectionbuilder.IntegratedSecurity = true;
            var connectionString = connectionbuilder.ConnectionString;

            string query2 = "select ProductoVendido.id,ProductoVendido.Stock,ProductoVendido.IdProducto,ProductoVendido.IdVenta,Producto.Id,Producto.Descripciones  from ProductoVendido inner join Producto on ProductoVendido.IdProducto=Producto.Id where IdUsuario=@idUs";
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(query2, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("idUs", idUs));
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductoVendido produc = new ProductoVendido();
                        produc.Id = Convert.ToInt32(reader.GetValue(0));
                        produc.Stock = Convert.ToDouble(reader.GetValue(1));
                        produc.IdProducto = Convert.ToInt32(reader.GetValue(2));
                        produc.IdVenta = Convert.ToInt32(reader.GetValue(3));
                        produc.Idp = Convert.ToInt32(reader.GetValue(4));
                        produc.Descripciones = reader.GetString(5);


                        Console.WriteLine("--------RESULTADO--------");
                        Console.WriteLine("Producto vendido id: " + produc.Id);
                        Console.WriteLine("Descripcion: " + produc.IdProducto);
                        Console.WriteLine("Stock: " + produc.Stock);
                        Console.WriteLine("id del usuario: " + produc.IdVenta);
                        Console.WriteLine("id producto: " + produc.Id);
                        Console.WriteLine("Descripcion: " + produc.Descripciones);
                        Console.WriteLine("----------------------");



                    }
                }



            }
        }
        public static void TraerProducto(int idUsu)
        {
            SqlConnectionStringBuilder connectionbuilder = new();
            connectionbuilder.DataSource = "DESKTOP-TA5P0R7";
            connectionbuilder.InitialCatalog = "SistemaGestion";
            connectionbuilder.IntegratedSecurity = true;
            var connectionString = connectionbuilder.ConnectionString;
            string query1 = "Select * from Producto where IdUsuario = @IdUsuario";
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(query1, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@IdUsuario", idUsu));
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Producto p = new Producto();
                        p.Idp = Convert.ToInt32(reader.GetValue(0));
                        p.Descripciones = reader.GetString(1);
                        p.Costo = Convert.ToInt32(reader.GetValue(2));
                        p.PrecioVenta = Convert.ToInt32(reader.GetValue(3));
                        p.Stock = Convert.ToInt32(reader.GetValue(4));
                        p.IdUsuario = Convert.ToInt32(reader.GetValue(5)); 


                            Console.WriteLine("--------RESULTADO--------");
                            Console.WriteLine("id: " + p.Idp);
                            Console.WriteLine("Descripcion: " + p.Descripciones);
                            Console.WriteLine("Costo: " + p.Costo);
                            Console.WriteLine("Precio de venta: " + p.PrecioVenta);
                            Console.WriteLine("Stock: " + p.Stock);
                            Console.WriteLine("Id del usuario: " + p.IdUsuario);
                            Console.WriteLine("----------------------");
                        
                    }

                }


            }


        }
        public static void TraerUsuario(string nombUsu)
        {
            SqlConnectionStringBuilder connectionbuilder = new();
            connectionbuilder.DataSource = "DESKTOP-TA5P0R7";
            connectionbuilder.InitialCatalog = "SistemaGestion";
            connectionbuilder.IntegratedSecurity = true;
            var connectionString = connectionbuilder.ConnectionString;
            string query = "Select * from Usuario where NombreUsuario = @NombreUsuario";
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(query, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@NombreUsuario", nombUsu));
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader.GetValue(0));
                        usuario.Nombre = reader.GetString(1);
                        usuario.Apellido = reader.GetString(2);
                        usuario.NombreUsuario = reader.GetString(3);
                        usuario.Contraseña = reader.GetString(4);
                        usuario.Mail = reader.GetString(5);

                        Console.WriteLine("--------RESULTADO--------");
                        Console.WriteLine("id: " + usuario.Id);
                        Console.WriteLine("Nombre: " + usuario.Nombre);
                        Console.WriteLine("Apellido: " + usuario.Apellido);
                        Console.WriteLine("Nombre de Usuario: " + usuario.NombreUsuario);
                        Console.WriteLine("Contraseña: " + usuario.Contraseña);
                        Console.WriteLine("Mail: " + usuario.Mail);
                        Console.WriteLine("--------FIN--------");

                    }
                    sql.Close();
                }
            }
        }
    }
}
