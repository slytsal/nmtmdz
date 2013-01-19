using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NMT_ESC_BLL.Domain
{
    /// <summary>
    /// Grupo de indicadores
    /// </summary>
    public class GrupoIndicador
    {
        public int Id{get; set;}
        public string About { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }
        public int LAST_INDEX { get; set; }
        public string LAST_FILTER { get; set; }
        public List<Indicador> Indicadores { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GrupoIndicador()
        {
        }
    }
}
