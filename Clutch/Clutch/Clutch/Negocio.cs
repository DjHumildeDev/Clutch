﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clutch
{
    public class Negocio
    {
        private ClutchDbEntities bd;
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
            bd = new ClutchDbEntities();
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
            return bd.Empleados.Count();
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
                empleado.vacaciones = true;
                bd.SaveChanges();
            }
        }

        public void ResetearVacaciones(Empleado empleado)
        {

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
            return bd.Repartidors.Count();
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
            return bd.Cocinas.Count();
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
            return bd.Encargadoes.Count();
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
                    jornada.pedidos = 0;
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

        public int SiguienteJornadaId()
        {
            return bd.Jornadas.Count();
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
                jornada.pedidos++;
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
        public int SiguienteIncidenciaId()
        {
            return siguienteIncidenciaId++;
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

        //Pedidos
        public void CrearPedido(Pedido pedido, Repartidor repartidor)
        {
            if (pedido != null)
            {
                if (repartidor != null)
                {
                    pedido.id = SiguientePedidoId();
                    pedido.idRepartidor = repartidor.id;
                    bd.Pedidoes.Add(pedido);
                    bd.SaveChanges();
                }
            }
        }
        public int SiguientePedidoId()
        {
            return siguientePedidoId++;
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
                    //pedido.entregado = true
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
            return siguienteMotoId++;
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
        public void AsignarMoto(Repartidor repartidor, Moto moto)
        {
            if (repartidor != null)
            {
                if (moto != null)
                {
                    repartidor.moto = moto.id;
                    bd.SaveChanges();
                }
            }
        }

    }
}

