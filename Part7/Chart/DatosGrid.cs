using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMC.Wpf.CoverFlow.NMT.ChartComponent
{
    public class DatosGrid
    {
        private string POS;
        private string NombrePos;
        private string ws;
        private double totalBudi;
        private double cpw;

        public string POS1
        {
            get { return POS; }
            set { POS = value; }
        }
        

        public string NombrePos1
        {
            get { return NombrePos; }
            set { NombrePos = value; }
        }
        

        public string Ws
        {
            get { return ws; }
            set { ws = value; }
        }
        

        public double TotalBudi
        {
            get { return totalBudi; }
            set { totalBudi = value; }
        }
        

        public double Cpw
        {
            get { return cpw; }
            set { cpw = value; }
        }
    }
}
