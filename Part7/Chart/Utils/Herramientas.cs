using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMC.Wpf.CoverFlow.NMT.ChartComponent.Domain;
using System.Data;

namespace IMC.Wpf.CoverFlow.NMT.ChartComponent.Utils
{
    class Herramientas
    {

        private static Herramientas instance;

        /// <summary>
        /// Patron de disenio solitario
        /// </summary>
        public static Herramientas Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Herramientas();
                }
                return instance;
            }
        }

       /// <summary>
       /// Devuelve el filtro con sus respectivos Items
       /// </summary>
       /// <param name="filtro1"></param>
       /// <param name="label"></param>
       /// <param name="valueColumn"></param>
       /// <param name="descColumn"></param>
       /// <param name="multiselect"></param>
       /// <returns></returns>
        public Filtro obtenerFiltro(DataTable filtro1, string label, string valueColumn, string descColumn,bool multiselect)
        {

            
            Filtro retorno = new Filtro();
            retorno.label = label;
            List<Item> values = new List<Item>();
            List<Item> valuesOrdered = new List<Item>();
            Item it = null;
            //if (!multiselect)
            //{
            //    it = new Item("Seleccione uno(a)", "Seleccione uno(a)");
            //    values.Add(it);
            //}
            foreach (DataRow g in filtro1.Rows)
            {
                if (g[valueColumn] != DBNull.Value)
                {
                    string dep = "";
                    string val = (string)g[valueColumn];
                    if (val.Contains("|"))
                    {
                        string[] vals = val.Split('|');                        
                        if(vals.Length>1)
                            dep = vals[1];
                    }
                    string des = "";
                    if (g[descColumn] != DBNull.Value)
                    {
                        des = (string)g[descColumn];
                        if (des.Contains("|"))
                        {
                            string[] vals = val.Split('|');
                            if (vals.Length > 0)
                                des = vals[0];                          
                        }
                    }
                    it = new Item(val, des, dep);
                    if (!val.ToUpper().Contains("TODO"))
                        values.Add(it);
                    else
                        valuesOrdered.Add(it);
                         
                
                }
            }
            foreach (Item i in valuesOrdered)
            {
                values.Insert(0, i);
            }
            if (!multiselect)
            {
                //it = new Item("Todo(a)s", "Todo(a)s");
                //values.Add(it);
                retorno.multiselection = false;
            }
            else
            {
                retorno.multiselection = true;
            }
            retorno.values = values;
            return retorno;
        }

        /// <summary>
        /// Devuelve la cadena separada por comas y con comillas sencillas
        /// </summary>
        /// <param name="init"></param>
        /// <returns></returns>
        public string obtenerMultiseleccionText(string init)
        {
            string retorno = "";
            if (init.Length > 0)
            {
                retorno = "'" + retorno.Replace(",","','")+"'";
            }
            return retorno;
        }
    }
}
