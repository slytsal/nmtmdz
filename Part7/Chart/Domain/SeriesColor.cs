using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace IMC.Wpf.CoverFlow.NMT.Chartcomponent.Domain
{
    /// <summary>
    /// Color de la serie
    /// </summary>
    public class SeriesColor
    {
        public Color color { get; set; }
        public string nombre { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="color"></param>
        /// <param name="nombre"></param>
        public SeriesColor(Color color, string nombre)
        {
            this.nombre = nombre;
            this.color = color;
        }
    }
}
