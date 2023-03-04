using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Nombre del proyecto:  Clutch
/// </summary>
namespace Clutch
{
    public class Negocio
    {
        private ClutchDb bd;

        public Negocio()
        {
            bd = new ClutchDb();
        }
        //Empleado

        /// <summary>
        /// Crear un empleado en la base de datos
        /// </summary>
        /// <param name="empleado"></param>
        public void CrearEmpleado(Empleado empleado)
        {
            if (empleado != null)
            {
                empleado.id = SiguienteEmpleadoId();
                bd.Empleados.Add(empleado);
                bd.SaveChanges();
            }
        }

        /// <summary>
        /// Funcion para obtener un empleado por su id
        /// </summary>
        /// <param name="empleadoId"></param>
        /// <returns>Objeto de la clase Empleado</returns>
        public Empleado ObtenerEmpleado(int empleadoId)
        {
            return bd.Empleados.FirstOrDefault(x => x.id == empleadoId);
        }
        /// <summary>
        /// Obtiene el id del empleado que se va ha crear
        /// </summary>
        /// <returns>Id de empleado</returns>
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

        /// <summary>
        /// Da de baja un Empleado
        /// </summary>
        /// <param name="empleado"></param>
        public void DarDeBajaEmpleado(Empleado empleado)
        {
            if (empleado != null)
            {
                empleado.baja = DateTime.Today;
                bd.SaveChanges();
            }
        }
        /// <summary>
        /// Borra el empleado de la base de datos
        /// </summary>
        /// <param name="empleadoId"></param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Edita el empleado en la base de datos
        /// </summary>
        /// <param name="empleado"></param>
        public void EditarEmpleado(Empleado empleado)
        {
            Empleado nuevoEmpleado = ObtenerEmpleado(empleado.id);
            empleado = nuevoEmpleado;
            bd.SaveChanges();
        }
        /// <summary>
        /// Obtiene todos los empleados de la base de datos
        /// </summary>
        /// <returns>Lista de Empleados</returns>
        public List<Empleado> ObtenerEmpleados()
        {
            List<Empleado> query = bd.Empleados.ToList();
            return query;
        }
        /// <summary>
        /// Activa el modo vacaciones en el empleado
        /// </summary>
        /// <param name="empleado"></param>
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
        //Resetea las vacaciones cuando acaba el año laboral
        public void ResetearVacaciones(Empleado empleado)
        {
            Empleado emp = ObtenerEmpleado(empleado.id);
            emp.diasVacaciones = 30;
            bd.SaveChanges();
        }

        /// <summary>
        /// Crear un Repartidor en la base de datos
        /// </summary>
        /// <param name="empleado">Empleado que es repartidor</param>
        /// <param name="repartidor"></param>
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
        /// <summary>
        /// Obtiene el id del Repartidor que se va ha crear
        /// </summary>
        /// <returns>Id de repartidor</returns>
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
        /// <summary>
        /// Obtiene todos los repartidores de la base de datos
        /// </summary>
        /// <returns>Lista de Repartidores</returns>
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
        /// <summary>
        /// Obtiene un Repartidor de la base de datos a partir del id de empleado
        /// </summary>
        /// <param name="empleadoId"></param>
        /// <returns>Objeto de la clase Repartidor</returns>
        public Repartidor ObtenerRepartidor(int empleadoId)
        {
            Repartidor repartidor = bd.Repartidors.Where(x => x.idEmpleado.Equals(empleadoId)).FirstOrDefault();

            return repartidor;
        }

        /// <summary>
        /// Obtiene el repartidor asignado a una moto de la base de datos
        /// </summary>
        /// <param name="motoId"></param>
        /// <returns>Objeto de la clase repartidor</returns>
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
        /// <summary>
        /// Crear un cocinero en la base de datos
        /// </summary>
        /// <param name="empleado">Empleado que es cocinero</param>
        /// <param name="cocinero"></param>
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
        /// <summary>
        /// Obtiene un cocinero de la base de datos a partir del id de empleado
        /// </summary>
        /// <param name="empleadoId"></param>
        /// <returns>Objeto de la clase Cocinero</returns>
        public Cocina ObtenerCocinero(int empleadoId)
        {
            Cocina cocinero = bd.Cocinas.Where(x => x.idEmpleado.Equals(empleadoId)).FirstOrDefault();

            return cocinero;
        }
        /// <summary>
        /// Obtiene el id del Cocinero que se va ha crear
        /// </summary>
        /// <returns>id del cocinero</returns>
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
        /// <summary>
        /// Obtiene todos los cocineros de la base de datos
        /// </summary>
        /// <returns>Lista de objetos Cocina</returns>
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
        /// <summary>
        /// Obtiene un cocinero a partir de su id
        /// </summary>
        /// <param name="cocineroId"></param>
        /// <returns>Objeto de la clase cocinero</returns>
        public Cocina BucarCocinero(int cocineroId)
        {
            return bd.Cocinas.FirstOrDefault(x => x.id == cocineroId);
        }
        /// <summary>
        ///  Crear un encargado en la base de datos
        /// </summary>
        /// <param name="empleado">Empleado que es encargado</param>
        /// <param name="encargado"></param>
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
        /// <summary>
        /// Obtiene el id del encargado que se va ha crear
        /// </summary>
        /// <returns>id de encargado</returns>
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
        /// <summary>
        /// Obtiene un encargado de la base de datos a partir del id de empleado
        /// </summary>
        /// <param name="empleadoId"></param>
        /// <returns>Objeto de la clase Encargado</returns>
        public Encargado ObtenerEncargado(int empleadoId)
        {
            Encargado encargado = bd.Encargadoes.Where(x => x.idEmpleado.Equals(empleadoId)).FirstOrDefault();

            return encargado;
        }
        /// <summary>
        /// Obtiene todos los encargados de la base de datos
        /// </summary>
        /// <returns>Lista de la clase Encargado</returns>
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
        /// <summary>
        /// Obtiene un encargado a partir del id del encargado
        /// </summary>
        /// <param name="encargadoId"></param>
        /// <returns>Objeto de la clase encargado</returns>
        public Encargado BuscarEncargado(int encargadoId) => bd.Encargadoes.FirstOrDefault(x => x.id == encargadoId);

        //Jornadas
        /// <summary>
        /// Crea una jornada en la base de datos a partir del id de un empleado
        /// </summary>
        /// <param name="empleado">Empleado que crea la jornada</param>
        /// <param name="jornada"></param>
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
        /// <summary>
        /// Edita una jornada de la base de datos
        /// </summary>
        /// <param name="jornada"></param>
        public void EditarJornada(Jornada jornada)
        {
            Jornada nuevaJornada = ObtenerJornada(jornada.id);
            nuevaJornada = jornada;
            bd.SaveChanges();
        }
        /// <summary>
        /// Borra una Jornada de la base de datos a partir del id
        /// </summary>
        /// <param name="jornadaId"></param>
        /// <returns>Bool</returns>
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
        /// <summary>
        /// Obtiene el id de la jornada que se va ha crear
        /// </summary>
        /// <returns>id de la jornada</returns>
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
        /// <summary>
        /// Cierra la ultima jornada abierta por un emplado
        /// </summary>
        /// <param name="empleado"></param>
        public void Cerrarjornada(Empleado empleado)
        {
            if (empleado != null)
            {
                List<Jornada> JornadasEmpleado = bd.Jornadas.Where(x => x.idEmpleado.Equals(empleado.id)).ToList();
                Jornada jornadaHoy = JornadasEmpleado.Last();
                jornadaHoy.Salida = DateTime.Now;
                CalcularSueldo(jornadaHoy);
                bd.SaveChanges();
            }
        }
        /// <summary>
        /// Obtiene una jornada de la base de datos a partir del id
        /// </summary>
        /// <param name="JornadaId"></param>
        /// <returns>Objeto de la clase Jornada</returns>
        public Jornada ObtenerJornada(int JornadaId)
        {
            return bd.Jornadas.FirstOrDefault(x => x.id == JornadaId);
        }
        /// <summary>
        /// Obtiene todas las jornadas de la base de datos
        /// </summary>
        /// <returns>Lista de la clase Jornada</returns>
        public List<Jornada> ObtenerJornadas()
        {
            List<Jornada> query = bd.Jornadas.ToList();
            return query;
        }
        /// <summary>
        /// Calcual el sueldo al acabar la 
        /// </summary>
        /// <param name="jornada"></param>
        public void CalcularSueldo(Jornada jornada)
        {
            if (jornada != null)
            {
                jornada.sueldoHoy = (double)((jornada.sueldo * jornada.horas) + jornada.pedidos);
                bd.SaveChanges();
            }
        }
        /// <summary>
        /// Suma un pedido a la jornada del repartidor que lo entrega
        /// </summary>
        /// <param name="jornada"></param>
        public void SumarPedido(Jornada jornada)
        {
            if (jornada != null)
            {
                jornada.pedidos = jornada.pedidos + 1;
                bd.SaveChanges();
            }
        }

        //Incidencias
        /// <summary>
        /// Crea una Incidencia en la base de datos
        /// </summary>
        /// <param name="empleado"Empleado que crea la incidencia></param>
        /// <param name="incidencia"></param>
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

        /// <summary>
        /// Obtiene las jornadas que no estan cerradas
        /// </summary>
        /// <returns>Lista de la clase Jornada</returns>
        public List<Jornada> ObtenerJornadasAbiertas()
        {
            List<Jornada> jornadas = new List<Jornada>();
            
            foreach (Jornada item in ObtenerJornadas())
            {
                if (item.Salida == null)
                {
                    jornadas.Add(item);
                }
            }
            return jornadas;
        }

        /// <summary>
        /// Obtiene la jornada abierta a parti de un empleado
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns>Objeto de la clase jornada</returns>
        public Jornada ObtenerJornadaAbierta(Empleado empleado)
        {
            List<Jornada> jornadas = new List<Jornada>();
            jornadas = ObtenerJornadas();
            foreach (Jornada item in jornadas)
            {
                if (item.idEmpleado.Equals(empleado.id))
                {
                    if (item.Salida == null)
                    {
                        return item;
                    }
                }
            }
            return null;
        }
        /// <summary>
        ///  Obtiene el id de la incidencia que se va ha crear
        /// </summary>
        /// <returns>id de la jornada</returns>
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
        /// <summary>
        /// Cierra la incidecnia
        /// </summary>
        /// <param name="incidencia"></param>
        public void CerrarIncidencia(incidencia incidencia)
        {
            if (incidencia != null)
            {
                incidencia.resuelta = true;
                bd.SaveChanges();
            }
        }
        /// <summary>
        /// Obtiene una incidencia de la base de datos a partir del id
        /// </summary>
        /// <param name="incidenciaId"></param>
        /// <returns>Objeto de la clase Uncidencia</returns>
        public incidencia BuscarIncidencia(int incidenciaId)
        {
            return bd.incidencias.FirstOrDefault(x => x.id == incidenciaId);
        }
        /// <summary>
        /// Obtiene las incidencias de la base de datos
        /// </summary>
        /// <returns>lista de la clase incidencia</returns>
        public List<incidencia> ObtenerIncidencias()
        {
            List<incidencia> query = bd.incidencias.ToList();
            return query;
        }
        /// <summary>
        /// Edita una incidencia de la base de datos
        /// </summary>
        /// <param name="incidencia"></param>
        public void EditarIncidencia(incidencia incidencia)
        {
            incidencia nuevaIncidencia = BuscarIncidencia(incidencia.id);
            incidencia = nuevaIncidencia;
            bd.SaveChanges();
        }
        /// <summary>
        /// Obtiene una incidencia de la base de datos a partir del id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto de la clase incidencia</returns>
        public incidencia ObtenerIncidencia(int id)
        {
            return bd.incidencias.FirstOrDefault(x => x.id == id);
        }
        /// <summary>
        /// Borra una incidencia de la base de datos a partir del id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Bool</returns>
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
        /// <summary>
        /// Crea un pedido en la base de datos
        /// </summary>
        /// <param name="pedido"></param>
        public void CrearPedido(Pedido pedido)
        {
            if (pedido != null)
            {                               
                pedido.id = SiguientePedidoId();                     
                bd.Pedidoes.Add(pedido);
                bd.SaveChanges();             
            }
        }

        /// <summary>
        /// Edita un pedido de la base de datos
        /// </summary>
        /// <param name="pedido"></param>
        public void EditarPedido(Pedido pedido)
        {
            Pedido nuevoPedido = BuscarPedido(pedido.id);
            pedido = nuevoPedido;
            bd.SaveChanges();
        }
        /// <summary>
        /// Borra un pedido de la base de datos a partir del id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Obtiene el id del pedido que se va ha crear
        /// </summary>
        /// <returns>id del pedido</returns>
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
        /// <summary>
        /// Se asigna el pedido al repartidor
        /// </summary>
        /// <param name="pedido">Pedido asignado al repartidor</param>
        /// <param name="repartidor">repartidor asignado al pedido</param>
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
        /// <summary>
        /// Entrega el pedido le suma a la jornada del repartidor
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="jornada"></param>
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
        /// <summary>
        /// Obtiene un pedido de la base de datos a partir del id
        /// </summary>
        /// <param name="pedidoId"></param>
        /// <returns></returns>
        public Pedido BuscarPedido(int pedidoId)
        {
            return bd.Pedidoes.FirstOrDefault(x => x.id == pedidoId);
        }
        /// <summary>
        /// Obtiene todos los pedidos de la base de datos
        /// </summary>
        /// <returns>Lista de la clase pedido</returns>
        public List<Pedido> ObtenerPedidos()
        {
            List<Pedido> query = bd.Pedidoes.ToList();
            return query;
        }

        //Motos
        /// <summary>
        /// Crea una moto en la base de datos
        /// </summary>
        /// <param name="moto"></param>
        public void CrearMoto(Moto moto)
        {
            if (moto != null)
            {
                moto.id = SiguienteMotoId();
                bd.Motoes.Add(moto);
                bd.SaveChanges();
            }
        }
        /// <summary>
        /// Obtiene el id de la moto que se va ha crear
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Edita una moto de la base de datos
        /// </summary>
        /// <param name="moto"></param>
        public void EditarMoto(Moto moto)
        {
            Moto nuevaMoto = BuscarMoto(moto.id);
            moto = nuevaMoto;
            bd.SaveChanges();
        }

        /// <summary>
        /// Borra una moto de la base de datos
        /// </summary>
        /// <param name="moto"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Obtiene todas las motos de la base de datos
        /// </summary>
        /// <returns>Lista de la clase moto</returns>
        public List<Moto> ObtenerMotos()
        {
            List<Moto> query = bd.Motoes.ToList();
            return query;
        }
        /// <summary>
        /// Obtiene una moto de la base de datos a partir del id
        /// </summary>
        /// <param name="motoId"></param>
        /// <returns>Objeto de la clase moto</returns>
        public Moto BuscarMoto(int motoId)
        {
            return bd.Motoes.FirstOrDefault(x => x.id == motoId);
        }
        /// <summary>
        /// Asigna una moto a un repartidor
        /// </summary>
        /// <param name="repartidor"></param>
        /// <param name="moto"></param>
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

