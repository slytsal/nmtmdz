using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMC.Wpf.CoverFlow.NMT.ChartComponent.Domain
{
    /// <summary>
    /// Item relacionado con el filtro
    /// </summary>
    public class Item
    {
        public string value { get; set; }
        public string desc { get; set; }
        public string valorDependencia { get; set; }
       

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value"></param>
        /// <param name="desc"></param>
        public Item(string value, string desc,string valorDependencia)
        {
            this.value = value;
            this.desc = desc;
            this.valorDependencia = valorDependencia;
        }
    }
}
