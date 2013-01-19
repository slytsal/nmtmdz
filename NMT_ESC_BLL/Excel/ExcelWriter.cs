using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using NMT_ESC_DAL;
using System.Data;
using System.Reflection;


namespace NMT_ESC_BLL.Excel
{
    /// <summary>
    /// Clase para escritura de Excel
    /// </summary>
    public class ExcelWriter
    {

        /// <summary>
        /// Generacion del excel del reporte
        /// </summary>
        /// <param name="path"></param>
        /// <param name="customerCode"></param>
        /// <param name="ProductSubfamilyDescription"></param>
        public void generarExcelSobreindexados(string path, params string[] filtros)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int fila = 4;
            int columna = 1;
            try
            {
                Range chartRange;
                xlWorkSheet.get_Range("a1", "k1").Merge(false);

                chartRange = xlWorkSheet.get_Range("a1", "k1");
                chartRange.FormulaR1C1 = "DETALLE SOBRE INDEXADOS";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                chartRange.Font.Bold = true;
                //Consulta a la BD
                System.Data.DataTable kpis;
                kpis = new System.Data.DataTable();
                Consultas consulta = new Consultas();
                kpis = consulta.SelectKPI_SobreindexadosResumen(filtros).Tables[0];
                consulta = null;
               
                if (kpis.Rows.Count>0)
                //Encabezado
                foreach (DataColumn col in kpis.Rows[0].Table.Columns)
                {
                   string valor = col.ColumnName;
                   xlWorkSheet.Cells[3, columna] = valor;
                   xlWorkSheet.Cells[3, columna].Style.Font.Name = "Calibri";
                   xlWorkSheet.Cells[3, columna].Style.Font.Bold = true;
                   xlWorkSheet.Cells[3, columna].BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic);
                   
                   columna++;
                   
                }
                chartRange = xlWorkSheet.get_Range("a3", "k3");
                chartRange.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic);
                chartRange.Interior.ColorIndex = 15;
                //chartRange.AutoFit();
                //Detalle
                foreach (DataRow g in kpis.Rows)
                {
                    columna = 1;
                    foreach (DataColumn col in g.Table.Columns)
                    {
                        string valor = "";
                        if (g[col.ColumnName] != null)
                        {
                            valor = g[col.ColumnName].ToString();
                        }
                        else valor = "";
                        xlWorkSheet.Cells[fila, columna] = valor;
                        xlWorkSheet.Cells[fila, columna].Style.Font.Name = "Calibri";
                        xlWorkSheet.Cells[fila, columna].BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic);
                        xlWorkSheet.Cells[fila, columna].Style.Font.Bold = false;
                        columna++;
                    }
                    fila++;                    
                }
                chartRange = xlWorkSheet.get_Range("a3", "k"+(fila-1));
                chartRange.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic);
                //Autoajustando tamanio
                try
                {
                    Range xlEntireColumn = null;
                    Range xlRange = null;
                    for (int i = 1; i < 12; i++)
                    {
                        xlRange = xlWorkSheet.Cells[1, i];
                        xlEntireColumn = xlRange.EntireColumn;
                        xlEntireColumn.AutoFit();
                    }                    
                }
                catch(Exception)
                {
                    //Excepted error
                }
                
                
            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }

            
            xlWorkBook.SaveAs(path, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //xlApp.Workbooks.Open(path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            xlApp = null;
            //Abrir excel            
            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(path, Missing.Value, true, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            excel.Visible = true;
            //excel.Quit();
            //releaseObject(excel);
            //excel.Quit();
        }

        /// <summary>
        /// liberacion de memoria
        /// </summary>
        /// <param name="obj"></param>
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                throw new Exception("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        public void generarExcelClientes(string path, string filtros)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int fila = 4;
            int columna = 1;
            try
            {
                Range chartRange;
                xlWorkSheet.get_Range("a1", "k1").Merge(false);
                string titulo;
                if (filtros != "Todos")
                    titulo = filtros;
                else
                    titulo = "";
                chartRange = xlWorkSheet.get_Range("a1", "k1");
                chartRange.FormulaR1C1 = "DETALLE SOBRE CLIENTES "+titulo;
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                chartRange.Font.Bold = true;
                //Consulta a la BD
                System.Data.DataTable kpis;
                kpis = new System.Data.DataTable();
                Consultas consulta = new Consultas();
                kpis = consulta.Select_ClientesDetallado(filtros).Tables[0];
                consulta = null;

                if (kpis.Rows.Count > 0)
                    //Encabezado
                    foreach (DataColumn col in kpis.Rows[0].Table.Columns)
                    {
                        string valor = col.ColumnName;
                        xlWorkSheet.Cells[3, columna] = valor;
                        xlWorkSheet.Cells[3, columna].Style.Font.Name = "Calibri";
                        xlWorkSheet.Cells[3, columna].Style.Font.Bold = true;
                        xlWorkSheet.Cells[3, columna].BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic);

                        columna++;

                    }
                chartRange = xlWorkSheet.get_Range("a3", "u3");
                chartRange.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic);
                chartRange.Interior.ColorIndex = 15;
                //chartRange.AutoFit();
                //Detalle
                foreach (DataRow g in kpis.Rows)
                {
                    columna = 1;
                    foreach (DataColumn col in g.Table.Columns)
                    {
                        string valor = "";
                        if (g[col.ColumnName] != null)
                        {
                            valor = g[col.ColumnName].ToString();
                        }
                        else valor = "";
                        xlWorkSheet.Cells[fila, columna] = valor;
                        xlWorkSheet.Cells[fila, columna].Style.Font.Name = "Calibri";
                        xlWorkSheet.Cells[fila, columna].BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic);
                        xlWorkSheet.Cells[fila, columna].Style.Font.Bold = false;
                        columna++;
                    }
                    fila++;
                }
                chartRange = xlWorkSheet.get_Range("a3", "k" + (fila - 1));
                chartRange.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic);
                //Autoajustando tamanio
                try
                {
                    Range xlEntireColumn = null;
                    Range xlRange = null;
                    for (int i = 1; i < 12; i++)
                    {
                        xlRange = xlWorkSheet.Cells[1, i];
                        xlEntireColumn = xlRange.EntireColumn;
                        xlEntireColumn.AutoFit();
                    }
                }
                catch (Exception)
                {
                    //Excepted error
                }


            }
            catch (Exception Error)
            {
                throw (new Exception(Error.ToString()));
            }


            xlWorkBook.SaveAs(path, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //xlApp.Workbooks.Open(path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();


            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            xlApp = null;
            //Abrir excel            
            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(path, Missing.Value, true, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            excel.Visible = true;
            //excel.Quit();
            //releaseObject(excel);
            //excel.Quit();
        }
    }
}
