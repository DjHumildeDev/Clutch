//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clutch
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pedido
    {
        public int id { get; set; }
        public Nullable<int> idRepartidor { get; set; }
        public string pedido1 { get; set; }
        public double precio { get; set; }
        public string direccion { get; set; }
        public string tlfCliente { get; set; }
        public bool Entregado { get; set; }
        public System.DateTime Fecha { get; set; }
    }
}
