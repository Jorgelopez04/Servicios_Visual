using Servicios_Entrega3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;


namespace Servicios_Entrega3.Controllers
{

    public class HomeController : Controller
    {

        Program program = Program.ObtenerInstancia();
        public ActionResult MenuPrincipal()
        {

            return View();
        }
        public ActionResult registro()
        {
            ViewBag.Notificacion = TempData["PacienteYaRegistrado"];
            return View();
        }

        public ActionResult estadisticas()
        {
            if (program.ListaDeClientes.Count > 0)
            {
                return View(program);
            }
            else
            {
                return RedirectToAction("seNecesitaRegistro");
            }

        }

        public ActionResult seNecesitaRegistro()
        {
            return View();
        }

        public ActionResult verificarParaEstrato()
        {
            int identificacion;
            int estrato;
            identificacion = Convert.ToInt32(Request.Form["idEstratoC"]);
            estrato = Convert.ToInt32(Request.Form["varEstratoC"]);

            if (!program.VerificarInfo.verificarExistenciaDeIdentidad(identificacion))
            {
                TempData["Notificacion"] = "El paciente no se encuentra en el sistema";
            }
            else if (program.verificarInfo.verificarsielEstratoeselMismo(identificacion, estrato))
            {
                TempData["Notificacion"] = "El paciente ya se encuentra en ests Estrato";
            }
            else
            {
                program.CambiarInfo.CambioEstrato(identificacion, estrato);
                string idMomentaneo = Convert.ToString(identificacion);
                TempData["idEstratoC"] = idMomentaneo;
                TempData.Keep("idEstratoC");
                return RedirectToAction("mostrarCambioEstrato");
            }

            return RedirectToAction("cambioEstrato");


        }
        public ActionResult cambioEstrato()
        {

            ViewBag.Notificacion = TempData["Notificacion"];
            return View();
        }

        public ActionResult mostrarCambioEstrato()
        {
            string dato = (string)TempData["idEstratoC"];
            TempData.Keep("idEstatoC");
            int datoEntero = Convert.ToInt32(dato);
            Persona paciente = program.obtenerClientePorId(datoEntero);
            return View(paciente);
        }

        public ActionResult mostrarRegistro()
        {

            int identificacion;
            string nombre;
            string apellidos;
            DateTime fechaNacimiento;
            int estrato;
            int metaahorroenergia;
            int consumoactualdeenregia;
            int promedioconsumodeaugua;
            int consumoactualagua;
            int consumogas;


            identificacion = Convert.ToInt32(Request.Form["id"]);
            nombre = Convert.ToString(Request.Form["nombre"]);
            apellidos = Convert.ToString(Request.Form["apellidos"]);
            fechaNacimiento = Convert.ToDateTime(Request.Form["fhNacimiento"]);
            estrato = Convert.ToInt32(Request.Form["estrato"]);
            metaahorroenergia = Convert.ToInt32(Request.Form["metaAhorroEnergia"]);
            consumoactualdeenregia = Convert.ToInt32(Request.Form["consumoActualEnergia"]);
            promedioconsumodeaugua = Convert.ToInt32(Request.Form["promedioConsumoAgua"]);
            consumoactualagua = Convert.ToInt32(Request.Form["consumoActualAgua"]);
            consumogas = Convert.ToInt32(Request.Form["consumoGas"]);



            if (program.VerificarInfo.verificarExistenciaDeIdentidad(identificacion))
            {
                TempData["PacienteYaRegistrado"] = "El cliente identificado con el numero " + identificacion + " ya se encuentra en el sistema";
                return RedirectToAction("registro");
            }
            Cliente cliente = new Cliente(identificacion, estrato, metaahorroenergia, consumoactualdeenregia, promedioconsumodeaugua, consumoactualagua, consumogas, consumoactualagua, consumogas);



            Persona persona = new Persona(identificacion, nombre, apellidos, fechaNacimiento, cliente);


            program.ingresarCliente(persona);
            return View(persona);


        }

        public ActionResult actualizarMetaAhorroEnergia()
        {
            ViewBag.Notificacion = TempData["NotificacionActualizarMetaAhorroEnergia"];
            return View();
        }

        public ActionResult verificarParaActualizarMetaAhorroEnergia()
        {
            int identificacion;
            identificacion = Convert.ToInt32(Request.Form["idMetaAhorroEnergia"]);

            if (program.VerificarInfo.verificarExistenciaDeIdentidad(identificacion) == false)
            {
                TempData["NotificacionActualizarMetaAhorroEnergia"] = "El cliente no se encuentra en el sistema";
            }
            else
            {
                string idMomentaneo = Convert.ToString(identificacion);
                TempData["idMomentaneoHistoria"] = idMomentaneo;
                TempData.Keep("idMomentaneoHistoria");
                return RedirectToAction("mostrarMetaAhorroEnergia");

            }
            return RedirectToAction("actualizarMetaAhorroEnergia");
        }

        public ActionResult mostrarActualizarMetaAhorroEnergia()
        {
            string dato = (string)TempData["idMomentaneoMetaAhorroEnergia"];
            TempData.Keep("idMomentaneoMetaAhorroEnergia");
            int datoEntero = Convert.ToInt32(dato);
            Persona paciente = program.obtenerClientePorId(datoEntero);
            ViewBag.Notificacion = TempData["cambioMetaAhorroEnergiaRealizado"];
            return View(paciente);
        }

        public ActionResult cambioMetaAhorroEnergia()
        {
            string dato = (string)TempData["idMomentaneoMetaAhorroEnergia"];
            int datoEntero = Convert.ToInt32(dato);
            int metaahorroenergia = Convert.ToInt32(Request.Form["nuevaMetaAhorroEnergia"]);
            Persona persona = program.obtenerClientePorId(datoEntero);
            program.CambiarInfo.cambiarMetadeAhorrodeEnergia(persona, metaahorroenergia);

            TempData["cambioMetaAhorroEnergiaRealizado"] = "La Meta de Ahorro de Energia del cliente se ha modificado con éxito";
            return RedirectToAction("mostrarMetaAhorroEnergia");

        }
        /**********************/

        public ActionResult actualizarConsumoActualEnergia()
        {
            ViewBag.Notificacion = TempData["NotificacionActualizarConsumoActualEnergia"];
            return View();
        }

        public ActionResult verificarParaActualizarConsumoActualEnergia()
        {
            int identificacion;
            identificacion = Convert.ToInt32(Request.Form["idConsumoActualEnergia"]);

            if (program.VerificarInfo.verificarExistenciaDeIdentidad(identificacion) == false)
            {
                TempData["NotificacionActualizarConsumoActualEnergia"] = "El cliente no se encuentra en el sistema";
            }
            else
            {
                string idMomentaneo = Convert.ToString(identificacion);
                TempData["idMomentaneoConsumoActualEnergia"] = idMomentaneo;
                TempData.Keep("idMomentaneoConsumoActualEnergia");
                return RedirectToAction("mostrarActualizarConsumoActualEnergia");

            }
            return RedirectToAction("actualizarConsumoActualEnergia");
        }

        public ActionResult mostrarActualizarConsumoActualEnergia()
        {
            string dato = (string)TempData["idMomentaneoConsumoActualEnergia"];
            TempData.Keep("idMomentaneoConsumoActualEnergia");
            int datoEntero = Convert.ToInt32(dato);
            Persona paciente = program.obtenerClientePorId(datoEntero);
            ViewBag.Notificacion = TempData["cambioConsumoActualEnergia"];
            return View(paciente);
        }

        public ActionResult cambioConsumoEnergia()
        {
            string dato = (string)TempData["idMomentaneoConsumoActualEnergia"];
            int datoEntero = Convert.ToInt32(dato);
            int consumoactualenergia = Convert.ToInt32(Request.Form["nuevoConsumoEnergia"]);
            Persona persona = program.obtenerClientePorId(datoEntero);
            program.CambiarInfo.cambiarConsumoActualdeEnergia(persona, consumoactualenergia);

            TempData["cambioConsumoActualEnergiaRealizado"] = "El consumo de energia del cliente se ha modificado con éxito";
            return RedirectToAction("mostrarActualizarConsumoActualEnergia");

        }
        /********************/
        public ActionResult actualizarPromedioConsumoAgua()
        {
            ViewBag.Notificacion = TempData["NotificacionActualizarPromedioConsumoAgua"];
            return View();
        }

        public ActionResult verificarParaActualizarPromedioConsumoAgua()
        {
            int identificacion;
            identificacion = Convert.ToInt32(Request.Form["idPromedioConsumoAgua"]);

            if (program.VerificarInfo.verificarExistenciaDeIdentidad(identificacion) == false)
            {
                TempData["NotificacionActualizarPromedioConsumoAgua"] = "El cliente no se encuentra en el sistema";
            }
            else
            {
                string idMomentaneo = Convert.ToString(identificacion);
                TempData["idMomentaneoPromedioConsumoAgua"] = idMomentaneo;
                TempData.Keep("idMomentaneoPromedioConsumoAgua");
                return RedirectToAction("mostrarActualizarPromedioConsumoAgua");

            }
            return RedirectToAction("actualizarPromedioConsumoAgua");
        }

        public ActionResult mostrarActualizarPromedioConsumoAgua()
        {
            string dato = (string)TempData["idMomentaneoPromedioConsumoAgua"];
            TempData.Keep("idMomentaneoPromedioConsumoAgua");
            int datoEntero = Convert.ToInt32(dato);
            Persona paciente = program.obtenerClientePorId(datoEntero);
            ViewBag.Notificacion = TempData["cambiarPromedioConsumoAgua"];
            return View(paciente);
        }

        public ActionResult cambioPromedioConsumoAgua()
        {
            string dato = (string)TempData["idMomentaneoPromedioConsumoAgua"];
            int datoEntero = Convert.ToInt32(dato);
            int promedioconsumodeagua = Convert.ToInt32(Request.Form["nuevoPromedioAgua"]);
            Persona persona = program.obtenerClientePorId(datoEntero);
            program.CambiarInfo.cambiarPromedioConsumoAgua(persona, promedioconsumodeagua);

            TempData["cambiarPromedioConsumoEnergiaRealizado"] = "El promedio de agua del cliente se ha modificado con éxito";
            return RedirectToAction("mostrarActualizarPromedioConsumoAgua");

        }
        /********************/
        public ActionResult actualizarConsumoActualAgua()
        {
            ViewBag.Notificacion = TempData["NotificacionActualizarConsumoActualAgua"];
            return View();
        }

        public ActionResult verificarParaActualizarConsumoActualAgua()
        {
            int identificacion;
            identificacion = Convert.ToInt32(Request.Form["idConsumoActualAgua"]);

            if (program.VerificarInfo.verificarExistenciaDeIdentidad(identificacion) == false)
            {
                TempData["NotificacionActualizarConsumoActualAgua"] = "El cliente no se encuentra en el sistema";
            }
            else
            {
                string idMomentaneo = Convert.ToString(identificacion);
                TempData["idMomentaneoConsumoActualAgua"] = idMomentaneo;
                TempData.Keep("idMomentaneoConsumoActualAgua");
                return RedirectToAction("mostrarActualizarConsumoActualAgua");

            }
            return RedirectToAction("actualizarConsumoActualAgua");
        }

        public ActionResult mostrarActualizarConsumoActualAgua()
        {
            string dato = (string)TempData["idMomentaneoConsumoActualAgua"];
            TempData.Keep("idMomentaneoConsumoActualAgua");
            int datoEntero = Convert.ToInt32(dato);
            Persona paciente = program.obtenerClientePorId(datoEntero);
            ViewBag.Notificacion = TempData["cambioConsumoActualAgua"];
            return View(paciente);
        }

        public ActionResult cambioConsumoActualAgua()
        {
            string dato = (string)TempData["idMomentaneoConsumoActualAgua"];
            int datoEntero = Convert.ToInt32(dato);
            int consumoactualagua = Convert.ToInt32(Request.Form["nuevoConsumoAgua"]);
            Persona persona = program.obtenerClientePorId(datoEntero);
            program.CambiarInfo.cambiarConsumoActualdeAgua(persona,consumoactualagua);

            TempData["cambioConsumoActualAguaRealizado"] = "El consumo de agua del cliente se ha modificado con éxito";
            return RedirectToAction("mostrarActualizarConsumoActualAgua");

        }
        /********************/
        public ActionResult actualizarConsumoGas()
        {
            ViewBag.Notificacion = TempData["NotificacionActualizarConsumoGas"];
            return View();
        }

        public ActionResult verificarParaActualizarConsumoGas()
        {
            int identificacion;
            identificacion = Convert.ToInt32(Request.Form["idConsumoActualGas"]);

            if (program.VerificarInfo.verificarExistenciaDeIdentidad(identificacion) == false)
            {
                TempData["NotificacionActualizarConsumoGas"] = "El cliente no se encuentra en el sistema";
            }
            else
            {
                string idMomentaneo = Convert.ToString(identificacion);
                TempData["idMomentaneoConsumoGas"] = idMomentaneo;
                TempData.Keep("idMomentaneoConsumoGas");
                return RedirectToAction("mostrarActualizarConsumoGas");

            }
            return RedirectToAction("actualizarConsumoGas");
        }

        public ActionResult mostrarActualizarConsumoGas()
        {
            string dato = (string)TempData["idMomentaneoConsumoGas"];
            TempData.Keep("idMomentaneoConsumoGas");
            int datoEntero = Convert.ToInt32(dato);
            Persona paciente = program.obtenerClientePorId(datoEntero);
            ViewBag.Notificacion = TempData["cambioConsumoGas"];
            return View(paciente);
        }

        public ActionResult cambioConsumoGas()
        {
            string dato = (string)TempData["idMomentaneoConsumoGas"];
            int datoEntero = Convert.ToInt32(dato);
            int promedioconsumodeagua = Convert.ToInt32(Request.Form["nuevoConsumoGas"]);
            Persona persona = program.obtenerClientePorId(datoEntero);
            program.CambiarInfo.cambiarConsumoGas(persona, promedioconsumodeagua);

            TempData["cambioConsumoActualAguaRealizado"] = "El consumo de gas del cliente se ha modificado con éxito";
            return RedirectToAction("mostrarActualizarConsumoGas");

        }

    }


}