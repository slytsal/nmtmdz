﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visifire.Charts;
using NMT_ESC_BLL.Domain;
using IMC.Wpf.CoverFlow.NMT.ChartComponent.Domain;

namespace IMC.Wpf.CoverFlow.NMT.ChartComponent
{
    /// <summary>
    /// Interfaz para los reportes
    /// </summary>
    public interface IChart
    {
        List<Filtro> ObtenerFiltros();
        Chart Draw(params string[] filtros);
        List<String> ObtenerCiclos();       
    }
   
}
