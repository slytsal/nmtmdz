using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMC.Wpf.CoverFlow.NMT.ChartComponent.Domain
{
    /// <summary>
    /// Filtro del reporte
    /// </summary>
    public class Filtro
    {
        public string label { get; set; }
        public List<Item> values { get; set; }
        public Item selectedvalue { get; set; }
        public Boolean multiselection { get; set; }
    }
}
