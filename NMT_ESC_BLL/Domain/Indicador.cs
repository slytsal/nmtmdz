using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NMT_ESC_BLL.Domain
{
    /// <summary>
    /// Indicador
    /// </summary>
    public class Indicador
    {
        public int Id { get; set; }
        public string About { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }

        /// <summary>
        ///  constructor empty
        /// </summary>
        public Indicador() 
        {         
        }
    }
}
