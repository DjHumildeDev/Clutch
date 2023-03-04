using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clutch
{
    public class Negocio
    {
        private ClutchDb bd;
        private static int siguienteEmpleadoId = 0;
        private static int siguienteCocineroId = 0;
        private static int siguienteRepartidorId = 0;
        private static int siguienteEncargadoId = 0;
        private static int siguienteJornadaId = 0;
        private static int siguienteIncidenciaId = 0;
        private static int siguienteMotoId = 0;
        private static int siguientePedidoId = 0;

        public Negocio()
        {
            bd = new ClutchDb();
        }
        //Empleado
        public void CrearEmpleado(Empleado empleado)
        {
            if (empleado != null)
            {
                empleado.id = SiguienteEmpleadoId();
                bd.Empleados.Add(empleado);
                bd.SaveChanges();
            }
        }
        public Empleado ObtenerEmpleado(int empleadoId)
        {
            return bd.Empleados.FirstOrDefault(x => x.id == empleadoId);
        }
        public int SiguienteEmpleadoId()
        {
            var result = (from Empleado in bd.Empleados orderby Empleado.id descending select Empleado).FirstOrDefault();

            if (result != null)
            {
                return result.id + 1;
            }
            else
            {
                return 0;
            }
        }
        public void DarDeBajaEmpleado(Empleado empleado)
        {
            if (empleado != null)
            {
                empleado.baja = DateTime.Today;
                bd.SaveChanges();
            }
        }
        public bool BorrarEmpleado(int empleadoId)
        {
            Empleado borrar = ObtenerEmpleado(empleadoId);
            if (borrar != null)
            {
                bd.Empleados.Remove(borrar);
                bd.SaveChanges();
                return true;
            }
            return false;
        }
        public void EditarEmpleado(Empleado empleado)
        {
            Empleado nuevoEmpleado = ObtenerEmpleado(empleado.id);
            empleado = nuevoEmpleado;
            bd.SaveChanges();
        }
        public List<Empleado> ObtenerEmpleados()
        {
            List<Empleado> query = bd.Empleados.ToList();
            return query;
        }
        public void Vacaciones(Empleado empleado)
        {
            if (empleado != null)
            {
                if (empleado.vacaciones == false)
                {
                    empleado.vacaciones = true;
                    bd.SaveChanges();
                }
                else
                {
                    empleado.vacaciones = false;
                    bd.SaveChanges();
                }
               
            }
        }

        public void ResetearVacaciones(Empleado empleado)
        {
            Empleado emp = ObtenerEmpleado(empleado.id);
            emp.diasVacaciones = 30;
            bd.SaveChanges();
        }
        public void CrearRepartidor(Empleado empleado, Repartidor repartidor)
        {
            if (empleado != null)
            {
                CrearEmpleado(empleado);
                if (repartidor != null)
                {
                    repartidor.idEmpleado = empleado.id;
                    repartidor.id = SiguienteRepartidorId();
                    bd.Repartidors.Add(repartidor);
                    bd.SaveChanges();
                }
            }
        }
        public int SiguienteRepartidorId()
        {
            var result = (from Repartidor in bd.Repartidors orderby Repartidor.id descending select Repartidor).FirstOrDefault();

            if (result != null)
            {
                return result.id + 1;
            }
            else
            {
                return 0;
            }
        }
        public List<Repartidor> ObtenerRepartidores()
        {
            List<Repartidor> query = bd.Repartidors.ToList();

            if (query != null)
            {
                return query;
            }
            else{
                return new List<Repartidor>();
            }
            
        }

        public Repartidor ObtenerRepartidor(int empleadoId)
        {
            Repartidor repartidor = bd.Repartidors.Where(x => x.idEmpleado.Equals(empleadoId)).FirstOrDefault();

            return repartidor;
        }

        public Repartidor ObtenerRepartidor_Moto(int motoId)
        {
            int moto = Convert.ToInt32(motoId);
            List<Repartidor> repartidores = bd.Repartidors.ToList();
            Repartidor repar = null;
            foreach (Repartidor r in repartidores)
            {
                if (r.moto == motoId)
                {
                    repar = r;
                    return repar;
                }
            } 
            return null;
                   
        }
        public void CrearCocinero(Empleado empleado, Cocina cocinero)
        {
            if (empleado != null)
            {
                CrearEmpleado(empleado);
                if (cocinero != null)
                {
                    cocinero.idEmpleado = empleado.id;
                    cocinero.id = SiguienteCocineroId();
                    bd.Cocinas.Add(cocinero);
                    bd.SaveChanges();
                }
            }
        }
        public Cocina ObtenerCocinero(int empleadoId)
        {
            Cocina cocinero = bd.Cocinas.Where(x => x.idEmpleado.Equals(empleadoId)).FirstOrDefault();

            return cocinero;
        }
        public int SiguienteCocineroId()
        {
            var result = (from Cocina in bd.Cocinas orderby Cocina.id descending select Cocina).FirstOrDefault();

            if (result != null)
            {
                return result.id + 1;
            }
            else
            {
                return 0;
            }
        }
        public List<Cocina> ObtenerCocineros()
        {
            List<Cocina> query = bd.Cocinas.ToList();
            if (query != null)
            {
                return query;
            }
            else{
                return new List<Cocina>();
            }
          
        }
        public Cocina BucarCocinero(int cocineroId)
        {
            return bd.Cocinas.FirstOrDefault(x => x.id == cocineroId);
        }
        public void CrearEncargado(Empleado empleado, Encargado encargado)
        {
            if (empleado != null)
            {
                CrearEmpleado(empleado);
                if (encargado != null)
                {
                    encargado.idEmpleado = empleado.id;
                    encargado.id = SiguienteEncargadoId();
                    bd.Encargadoes.Add(encargado);
                    bd.SaveChanges();
                }
            }
        }
        public int SiguienteEncargadoId()
        {
            var result = (from Encargado in bd.Encargadoes orderby Encargado.id descending select Encargado).FirstOrDefault();

            if (result != null)
            {
                return result.id + 1;
            }
            else
            {
                return 0;
            }
        }
        public Encargado ObtenerEncargado(int empleadoId)
        {
            Encargado encargado = bd.Encargadoes.Where(x => x.idEmpleado.Equals(empleadoId)).FirstOrDefault();

            return encargado;
        }

        public List<Encargado> ObtenerEncargados()
        {
            List<Encargado> query = bd.Encargadoes.ToList();

            if (query != null)
            {
                return query;
            }
            else
            {
                return new List<Encargado>();
            }

        }
        public Encargado BuscarEncargado(int encargadoId) => bd.Encargadoes.FirstOrDefault(x => x.id == encargadoId);

        //Jornadas
        public void CrearJornada(Empleado empleado, Jornada jornada)
        {
            if (empleado != null)
            {
                if (jornada != null)
                {
                    if (empleado.vacaciones == true)
                    {
                        empleado.vacaciones = false;
                    }
                    
                    jornada.idEmpleado = empleado.id;
                    jornada.id = SiguienteJornadaId();
                    jornada.sueldo = 12.5;
                    
                    bd.Jornadas.Add(jornada);
                    bd.SaveChanges();
                }
            }
        }
        public void EditarJornada(Jornada jornada)
        {
            Jornada nuevaJornada = ObtenerJornada(jornada.id);
            nuevaJornada = jornada;
            bd.SaveChanges();
        }

        public bool BorrarJornada(int jornadaId)
        {
            Jornada borrar = ObtenerJornada(jornadaId);
            if (borrar != null)
            {
                bd.Jornadas.Remove(borrar);
                bd.SaveChanges();
                return true;
            }
            return false;
        }

        public int SiguienteJornadaId()
        {
            var result = (from Jornada in bd.Jornadas orderby Jornada.id descending select Jornada).FirstOrDefault();
            if (result != null)
            {
                return result.id + 1;
            }
            else
            {
                return 0;
            }
            
        }
        public void Cerrarjornada(Empleado empleado)
        {
            if (empleado != null)
            {
                List<Jornada> JornadasEmpleado = bd.Jornadas.Where(x => x.idEmpleado.Equals(empleado.id)).ToList();
                Jornada jornadaHoy = JornadasEmpleado.Last();
                jornadaHoy.Salida = DateTime.Now;
                bd.SaveChanges();
            }
        }
        public Jornada ObtenerJornada(int JornadaId)
        {
            return bd.Jornadas.FirstOrDefault(x => x.id == JornadaId);
        }
        public List<Jornada> ObtenerJornadas()
        {
            List<Jornada> query = bd.Jornadas.ToList();
            return query;
        }
        public void CalcularSueldo(Jornada jornada)
        {
            if (jornada != null)
            {
                jornada.sueldoHoy = (double)((jornada.sueldo * jornada.horas) + jornada.pedidos);
                bd.SaveChanges();
            }
        }
        public void SumarPedido(Jornada jornada)
        {
            if (jornada != null)
            {
                jornada.pedidos = jornada.pedidos + 1;
                bd.SaveChanges();
            }
        }

        //Incidencias
        public void CrearIncidencia(Empleado empleado, incidencia incidencia)
        {
            if (empleado != null)
            {
                if (incidencia != null)
                {
                    incidencia.idEmpleado = empleado.id;
                    incidencia.id = SiguienteIncidenciaId();
                    bd.incidencias.Add(incidencia);
                    bd.SaveChanges();
                }
            }
        }

        public Jornada ObtenerJornadaAbierta(Empleado empleado)
        {
            List<Jornada> jornadas = new List<Jornada>();
            jornadas = ObtenerJornadas();
            

            foreach (Jornada item in jornadas)
            {
                if (item.idEmpleado.Equals(empleado.id))
                {
                    if(item.Salida == null)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        public int SiguienteIncidenciaId()
        {
            var result = (from incidencia in bd.incidencias orderby incidencia.id descending select incidencia).FirstOrDefault();
            if (result != null)
            {
                return result.id + 1;
            }
            else
            {
                return 0;
            }
        }
        public void CerrarIncidencia(incidencia incidencia)
        {
            if (incidencia != null)
            {
                incidencia.resuelta = true;
                bd.SaveChanges();
            }
        }
        public incidencia BuscarIncidencia(int incidenciaId)
        {
            return bd.incidencias.FirstOrDefault(x => x.id == incidenciaId);
        }
        public List<incidencia> ObtenerIncidencias()
        {
            List<incidencia> query = bd.incidencias.ToList();
            return query;
        }
        public void EditarIncidencia(incidencia incidencia)
        {
            incidencia nuevaIncidencia = BuscarIncidencia(incidencia.id);
            incidencia = nuevaIncidencia;
            bd.SaveChanges();
        }

        public incidencia ObtenerIncidencia(int id)
        {
            return bd.incidencias.FirstOrDefault(x => x.id == id);
        }

        public bool BorrarIncidencia(int id)
        {
            incidencia borrar = ObtenerIncidencia(id);
            if (borrar != null)
            {
                bd.incidencias.Remove(borrar);
                bd.SaveChanges();
                return true;
            }
            return false;
        }
        //Pedidos
        public void CrearPedido(Pedido pedido)
        {
            if (pedido != null)
            {                               
                pedido.id = SiguientePedidoId();                     
                bd.Pedidoes.Add(pedido);
                bd.SaveChanges();             
            }
        }
        public void EditarPedido(Pedido pedido)
        {
            Pedido nuevoPedido = BuscarPedido(pedido.id);
            pedido = nuevoPedido;
            bd.SaveChanges();
        }
        public bool BorrarPedido(int id)
        {
            Pedido borrar = BuscarPedido(id);
            if (borrar != null)
            {
                bd.Pedidoes.Remove(borrar);
                bd.SaveChanges();
                return true;
            }
            return false;
        }


        public int SiguientePedidoId()
        {
            var result = (from Pedido in bd.Pedidoes orderby Pedido.id descending select Pedido).FirstOrDefault();

            if (result != null)
            {
                return result.id + 1;
            }
            else
            {
                return 0;
            }
        }
        public void RecogerPedido(Pedido pedido, Repartidor repartidor)
        {
            if (pedido != null)
            {
                if (repartidor != null)
                {
                    pedido.idRepartidor = repartidor.id;
                    bd.SaveChanges();
                }
            }
        }
        public void EntregarPedido(Pedido pedido, Jornada jornada)
        {
            if (pedido != null)
            {
                if (jornada != null)
                {
                    SumarPedido(jornada);
                    pedido.Entregado = true;
                    bd.SaveChanges();
                }
            }
        }
        public Pedido BuscarPedido(int pedidoId)
        {
            return bd.Pedidoes.FirstOrDefault(x => x.id == pedidoId);
        }
        public List<Pedido> ObtenerPedidos()
        {
            List<Pedido> query = bd.Pedidoes.ToList();
            return query;
        }

        //Motos
        public void CrearMoto(Moto moto)
        {
            if (moto != null)
            {
                moto.id = SiguienteMotoId();
                bd.Motoes.Add(moto);
                bd.SaveChanges();
            }
        }
        public int SiguienteMotoId()
        {
            var result = (from Moto in bd.Motoes orderby Moto.id descending select Moto).FirstOrDefault();

            if (result != null)
            {
                return result.id + 1;
            }
            else
            {
                return 0;
            }
        }
        public void EditarMoto(Moto moto)
        {
            Moto nuevaMoto = BuscarMoto(moto.id);
            moto = nuevaMoto;
            bd.SaveChanges();
        }
        public bool BorrarMoto(Moto moto)
        {
            if (moto != null)
            {
                Moto borrar = BuscarMoto(moto.id);
                if (borrar != null)
                {
                    bd.Motoes.Remove(borrar);
                    bd.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }
        public List<Moto> ObtenerMotos()
        {
            List<Moto> query = bd.Motoes.ToList();
            return query;
        }
        public Moto BuscarMoto(int motoId)
        {
            return bd.Motoes.FirstOrDefault(x => x.id == motoId);
        }
        public void AsignarMoto (Repartidor repartidor, Moto moto)
        {
            if (repartidor != null)
            {
                if (moto != null)
                {
                    Repartidor repar = ObtenerRepartidor(repartidor.idEmpleado);
                    Moto motoActual = BuscarMoto(moto.id);
                    motoActual.estado = "Ocupada";
                    repar.moto = moto.id;
                    bd.SaveChanges();
                }
            }
        }

    }
}

