﻿using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Homecenter.Business;
using Homecenter.Entidades;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System;

namespace Homecenter.Controllers
{
    /// <summary>
    /// Controlador de las ventas
    /// </summary>
    public class VentasController : Controller
    {
        #region Métodos
        /// <summary>
        /// Muestra el listado de asesores
        /// </summary>
        /// <returns>Listado de asesores</returns>
        public ActionResult Listado()
        {
            if (Session["usuario"] == null)
            {
                return new RedirectResult("~/Account/Login");
            }
            return View();
        }

        /// <summary>
        /// Inserta una venta
        /// </summary>
        /// <param name="venta">Venta a insertar</param>
        /// <returns>Venta insertado</returns>
        public JsonResult InsertarVenta(Venta venta)
        {
            VentaManager manager = new VentaManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            venta = manager.Insertar(venta);
            return new JsonResult() { Data = venta };
        }

        /// <summary>
        /// Página para el registro de una venta
        /// </summary>
        /// <returns>Página de registro de venta</returns>
        public ActionResult Venta()
        {
            if (Session["usuario"] == null)
            {
                return new RedirectResult("~/Account/Login");
            }
            return View();
        }

        /// <summary>
        /// Página para la solicitud del reporte de ventas
        /// </summary>
        /// <returns>Página de solicitud de reporte</returns>
        public ActionResult Reporte()
        {
            if (Session["usuario"] == null)
            {
                return new RedirectResult("~/Account/Login");
            }
            return View();
        }

        /// <summary>
        /// Muestra el formulario para listar la comisiones de un usuario
        /// </summary>
        /// <returns>Formulario para listar la comisiones de un usuario</returns>
        public ActionResult ReporteComisiones()
        {
            if (Session["usuario"] == null)
            {
                return new RedirectResult("~/Account/Login");
            }
            return View();
        }

        /// <summary>
        /// Trae el total de comisiones
        /// </summary>
        /// <param name="fechaInicio">Fecha desde la cual se genera el reporte</param>
        /// <param name="fechaFin">Fecha hasta la cual se genera el reporte</param>
        /// <returns>Total de comisiones</returns>
        public JsonResult ContarComisiones(string fechaInicio, string fechaFin)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            AsesorManager asesorManager = new AsesorManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            Asesor asesor = asesorManager.BuscarXCedula(usuario.Login);
            if (asesor != null && asesor.AsesorId != 0)
            {
                VentaManager manager = new VentaManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
                int listado = manager.ContarComisiones(fechaInicio, fechaFin, asesor.AsesorId);
                return new JsonResult() { Data = listado };
            }
            else
            {
                return new JsonResult() { Data = 0 };
            }
        }

        /// <summary>
        /// Listado de comisiones del asesor logueado
        /// </summary>
        /// <param name="fechaInicio">Fecha inicio desde donde se genera el reporte</param>
        /// <param name="fechaFin">Fecha final hasta donde se genera el reporte</param>
        /// <param name="inicio">Registro inicial</param>
        /// <param name="numRegitros">Número de registros a mostrar</param>
        /// <returns>Listado de comisiones del usuario logueado</returns>
        public JsonResult ListarComisiones(string fechaInicio, string fechaFin, int inicio, int numRegitros)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            AsesorManager asesorManager = new AsesorManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            Asesor asesor = asesorManager.BuscarXCedula(usuario.Login);
            if(asesor != null && asesor.AsesorId != 0)
            {
                VentaManager manager = new VentaManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
                var listado = manager.ListarComisiones(fechaInicio, fechaFin, asesor.AsesorId, inicio, numRegitros);
                return new JsonResult() { Data = listado };
            } else
            {
                return new JsonResult() { Data = null };
            }
        }

        /// <summary>
        /// Crea un reporte en un archivo excel
        /// </summary>
        /// <param name="fechaInicio">Fecha inicio desde donde se genera el reporte</param>
        /// <param name="fechaFin">Fecha final hasta donde se genera el reporte</param>
        /// <returns>Archivo excel con el reporte para descarga</returns>
        public void GenerarReporte(string fechaInicio, string fechaFin)
        {
            string path = Server.MapPath("~/reportes");
            string filepath = path + "/reporte.xlsx";

            SpreadsheetDocument archivoExcel = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);

            WorkbookPart workbookpart = archivoExcel.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            Sheets sheets = archivoExcel.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            Sheet sheet = new Sheet()
            {
                Id = archivoExcel.WorkbookPart.
                GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "reporte"
            };

            sheets.Append(sheet);

            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            sheetData.AppendChild(Encabezado());

            //Puebla con los datos de las ventas
            VentaManager manager = new VentaManager(ConfigurationManager.ConnectionStrings["homecenter"].ConnectionString);
            List<ReporteVenta> ventas = manager.GenerarReporte(fechaInicio, fechaFin);

            int index = 2;
            foreach (ReporteVenta venta in ventas)
            {
                sheetData.AppendChild(CrearFilaReporte(venta, index));
                index++;
            }

            workbookpart.Workbook.Save();

            archivoExcel.Close();

            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            string fileName = "reporte.xlsx";
        }

        private Row CrearFilaReporte(ReporteVenta venta, int index)
        {
            Row row = new Row();
            row.RowIndex = (UInt32)index;
            row.Append(new Cell()
            {
                CellValue = new CellValue("" + venta.VentaId),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("" + venta.Asesor),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("" + venta.Cedula),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("" + venta.Codigo),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("" + venta.Tienda),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue(venta.SKUElectrodomestico),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue(venta.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss")),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue(venta.CedulaCliente),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue(venta.SKU),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue(venta.ValorComision.ToString()),
                DataType = new EnumValue<CellValues>(CellValues.String)
            });
            return row;
        }

        /// <summary>
        /// Crea la fila de encabezados
        /// </summary>
        /// <returns></returns>
        private Row Encabezado()
        {
            Row row = new Row();
            row.RowIndex = 1;
            row.Append(new Cell()
            {
                CellValue = new CellValue("VentaID"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("Asesor"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("Cédula Asesor"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("Código Asesor"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("Tienda"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("SKU Electrodoméstico"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("Fecha"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("Cédula cliente"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("SKU Garantía"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("Valor comisión"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            });
            return row;
        }
        #endregion
    }
}