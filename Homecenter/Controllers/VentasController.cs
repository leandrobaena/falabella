using DocumentFormat.OpenXml;
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
            return View();
        }

        /// <summary>
        /// Página para la solicitud del reporte de ventas
        /// </summary>
        /// <returns>Página de solicitud de reporte</returns>
        public ActionResult Reporte()
        {
            return View();
        }

        /// <summary>
        /// Crea un reporte en un archivo excel
        /// </summary>
        /// <param name="fechaInicio">Fecha inicio desde donde se genera el reporte</param>
        /// <param name="fechaFin">Fecha final hasta donde se genera el reporte</param>
        /// <returns>Archivo excel con el reporte para descarga</returns>
        public void GenerarReporte(string fechaInicio, string fechaFin)
        {
            string path = Server.MapPath("/reportes");
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
                CellValue = new CellValue(venta.SKU),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue(venta.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss")),
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
                CellValue = new CellValue("Cédula"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("Código"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("Tienda"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("SKU"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            }, new Cell()
            {
                CellValue = new CellValue("Fecha"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            });
            return row;
        }
        #endregion
    }
}