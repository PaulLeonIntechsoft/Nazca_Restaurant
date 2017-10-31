using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nazca_Restaurant.Models;
using System.Globalization;

namespace Nazca_Restaurant.Controllers
{

    [Authorize]
    public class CajaController : Controller
    {

        NazResEntities2 _databaseManager = new NazResEntities2();

        public ActionResult Apertura()
        {

            List<sp_listarAperturaCaja_Result> listaBase = new List<sp_listarAperturaCaja_Result>();
            listaBase = this._databaseManager.sp_listarAperturaCaja().ToList();

            return View(listaBase);
        }

        [HttpPost]
        public ActionResult Apertura(string dtmFecAper, string numSoles, string numDolares)
        {
            List<sp_listarAperturaCaja_Result> listaBase = new List<sp_listarAperturaCaja_Result>();
            try
            {
                if (!dtmFecAper.Equals("") || !numSoles.Equals("") || !numDolares.Equals(""))
                {
                    var fechaBase = DateTime.ParseExact(dtmFecAper, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    string fecha = fechaBase.ToString("yyyy-MM-dd");
                    int n = this._databaseManager.sp_aperturarCaja(fecha,Decimal.Parse(numSoles), Decimal.Parse(numDolares),"ADMIN");
                    if (n == 1)
                    {
                        listaBase = this._databaseManager.sp_listarAperturaCaja().ToList();
                        ViewBag.Success = "Actualizacion de Apertura realizado exitosamente.";
                        return View(listaBase);
                    }else
                    {
                        listaBase = this._databaseManager.sp_listarAperturaCaja().ToList();
                        return View(listaBase);
                    }
                }else
                {
                    ModelState.AddModelError(string.Empty, "No se permiten campos vacíos.");
                    listaBase = this._databaseManager.sp_listarAperturaCaja().ToList();
                    return View(listaBase);
                }

            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Verifique los campos ingresados.");
                listaBase = this._databaseManager.sp_listarAperturaCaja().ToList();
                return View(listaBase);
                throw;
            }
        }

        public ActionResult TipoCambio()
        {
            List<sp_listarTiposDeCambio_Result> listaBase = new List<Models.sp_listarTiposDeCambio_Result>();
            listaBase = this._databaseManager.sp_listarTiposDeCambio().ToList();
            return View(listaBase);
        }

        [HttpPost]
        public ActionResult TipoCambio(string dtmTipCam, string numTipCam)
        {
            List<sp_listarTiposDeCambio_Result> listaBase = new List<Models.sp_listarTiposDeCambio_Result>();
            try
            {
                if (!dtmTipCam.Equals("") || !numTipCam.Equals(""))
                {
                    var fechaBase = DateTime.ParseExact(dtmTipCam, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    String fecha = fechaBase.ToString("yyyy-MM-dd");
                    int n = this._databaseManager.sp_agregarTipoDeCambio(fecha,Decimal.Parse(numTipCam));
                    if (n == 1)
                    {
                        listaBase = this._databaseManager.sp_listarTiposDeCambio().ToList();
                        ViewBag.Success = "Actualizacion del Tipo de Cambio exitoso.";
                        return View(listaBase);
                    }
                    else
                    {
                        listaBase = this._databaseManager.sp_listarTiposDeCambio().ToList();
                        return View(listaBase);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No se permiten campos vacíos.");
                    listaBase = this._databaseManager.sp_listarTiposDeCambio().ToList();
                    return View(listaBase);
                }

            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Verifique los campos ingresados.");
                listaBase = this._databaseManager.sp_listarTiposDeCambio().ToList();
                return View(listaBase);
                throw;
            }
        }

    }
}