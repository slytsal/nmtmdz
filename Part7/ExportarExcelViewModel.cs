using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace IMC.Wpf.CoverFlow.NMT
{
    public class ExportarExcelViewModel : INotifyPropertyChanged
    {
        private bool _IsDone;

        public bool IsDone
        {
            get { return _IsDone; }
            set {
                if (_IsDone != value)
                {
                    _IsDone = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("IsDone"));
                }
                
            }
        }

        private string _Mensaje;

        public string Mensaje
        {
            get { return _Mensaje; }
            set 
            {
                if (_Mensaje != value)
                {
                    _Mensaje = value;
                    if(PropertyChanged!=null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Mensaje"));
                }
            }
        }

        public ExportarExcelViewModel()
        {
            this._IsDone = false;
            this.Mensaje = "Espera a que el proceso termine";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
