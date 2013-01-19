using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows;
using FluidKit.NMT.GlassWindow;
using IMC.Wpf.CoverFlow.NMT.Media;
namespace IMC.Wpf.CoverFlow.NMT
{
    public partial class FlowControl : UserControl
    {
        #region Fields
        public const int HalfRealizedCount = 7;
        public const int PageSize = HalfRealizedCount;
        private ICoverFactory coverFactory;
        private readonly Dictionary<int, ImageInfo> imageList = new Dictionary<int, ImageInfo>();
        private readonly Dictionary<string, int> labelIndex = new Dictionary<string, int>();
        private readonly Dictionary<int, string> indexLabel = new Dictionary<int, string>();
        private readonly Dictionary<int, ICover> coverList = new Dictionary<int, ICover>();
        private int index;
        private int firstRealized = -1;
        private int lastRealized = -1;
        public Window parentWin {get; set;}
        
        #endregion
        #region Private stuff

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="animate"></param>
        private void RotateCover(int pos, bool animate)
        {
            if (coverList.ContainsKey(pos))
                coverList[pos].Animate(index, animate);
        }

        /// <summary>
        /// Actualiza el elemento seleccionado
        /// </summary>
        /// <param name="newIndex"></param>
        public void UpdateIndex(int newIndex)
        {
            
            if (index != newIndex)
            {
                bool animate = Math.Abs(newIndex - index) < PageSize;
                UpdateRange(newIndex);
                int oldIndex = index;
                index = newIndex;
                if (index > oldIndex)
                {
                    if (oldIndex < firstRealized)
                        oldIndex = firstRealized;
                    for (int i = oldIndex; i <= index; i++)
                        RotateCover(i, animate);
                }
                else
                {
                    if (oldIndex > lastRealized)
                        oldIndex = lastRealized;
                    for (int i = oldIndex; i >= index; i--)
                        RotateCover(i, animate);
                }
                camera.Position = new Point3D(Cover.CoverStep * index, camera.Position.Y, camera.Position.Z);
                if (parentWin.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.PrincipalWindow")
                {
                    ((PrincipalWindow)parentWin).asignarGrupo();
                }
                else if (parentWin.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.IndicadorWindow")
                {
                    ((IndicadorWindow)parentWin).initfilters();
                    ((IndicadorWindow)parentWin).draw();
                    ((IndicadorWindow)parentWin).tbAboutIndicador.Text = this.getActual().About;
                    ((IndicadorWindow)parentWin).tbDescripcion.Text = this.getActual().Desc;
                }
                else if (parentWin.GetType().Name == "ConfigGlassWindow")
                {
                    ((ConfigGlassWindow)parentWin).changeStyle(getActual().ID.ToString());
                }
            }
            else //Selecciono 
            {
                if (parentWin.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.PrincipalWindow")
                {
                    ((PrincipalWindow)parentWin).seleccionarGrupo();
                }
                else if (parentWin.GetType().FullName == "IMC.Wpf.CoverFlow.NMT.IndicadorWindow")
                {
                    ((IndicadorWindow)parentWin).initfilters();
                    ((IndicadorWindow)parentWin).draw();
                    ((IndicadorWindow)parentWin).tbAboutIndicador.Text = this.getActual().About;
                }
                else if (parentWin.GetType().Name == "ConfigGlassWindow")
                {
                    ((ConfigGlassWindow)parentWin).changeStyle(getActual().ID.ToString());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewPort_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Util.Instance.playSound("Media/Sounds/click.wav");
            var rayMeshResult = (RayMeshGeometry3DHitTestResult)VisualTreeHelper.HitTest(viewPort, e.GetPosition(viewPort));
            if (rayMeshResult != null)
            {
                foreach (int i in coverList.Keys)
                {
                    if (!coverList.ContainsKey(i))
                        continue;
                    if (coverList[i].Matches(rayMeshResult.MeshHit))
                    {
                        UpdateIndex(i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        private void RemoveCover(int pos)
        {
            if (!coverList.ContainsKey(pos))
                return;
            coverList[pos].Destroy();
            coverList.Remove(pos);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newIndex"></param>
        private void UpdateRange(int newIndex)
        {
            int newFirstRealized = Math.Max(newIndex - HalfRealizedCount, 0);
            int newLastRealized = Math.Min(newIndex + HalfRealizedCount, imageList.Count - 1);
            if (lastRealized < newFirstRealized || firstRealized > newLastRealized)
            {
                visualModel.Children.Clear();
                coverList.Clear();
            }
            else if (firstRealized < newFirstRealized)
            {
                for (int i = firstRealized; i < newFirstRealized; i++)
                    RemoveCover(i);
            }
            else if (newLastRealized < lastRealized)
            {
                for (int i = lastRealized; i > newLastRealized; i--)
                    RemoveCover(i);
            }
            for (int i = newFirstRealized; i <= newLastRealized; i++)
            {
                if (!coverList.ContainsKey(i))
                {
                    ICover cover = coverFactory.NewCover(imageList[i].Host, imageList[i].Path, i, newIndex, imageList[i].About, imageList[i].Desc, imageList[i].ID, imageList[i].LAST_INDEX, imageList[i].LAST_FILTER);
                    coverList.Add(i, cover);
                }
            }
            firstRealized = newFirstRealized;
            lastRealized = newLastRealized;
        }

        /// <summary>
        /// 
        /// </summary>
        protected int FirstRealizedIndex
        {
            get { return firstRealized; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected int LastRealizedIndex
        {
            get { return lastRealized; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        private void Add(ImageInfo info)
        {
            imageList.Add(imageList.Count, info);
            UpdateRange(index);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public FlowControl()
        {
            InitializeComponent();
            coverFactory = new CoverFactory(visualModel);
        }

        /// <summary>
        /// 
        /// </summary>
        public void reinit()        
        {
            InitializeComponent();
            coverFactory = new CoverFactory(visualModel);
        }

        /// <summary>
        /// 
        /// </summary>
        public IThumbnailManager Cache
        {
            set { Cover.Cache = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void GoToNext()
        {
            if (index != imageList.Count-1)
                UpdateIndex(Math.Min(index + 1, imageList.Count - 1));
        }

        /// <summary>
        /// 
        /// </summary>
        public void GoToPrevious()
        {
            if (index != 0)
            UpdateIndex(Math.Max(index - 1, 0));
        }

        /// <summary>
        /// 
        /// </summary>
        public void GoToNextPage()
        {
            UpdateIndex(Math.Min(index + PageSize, imageList.Count - 1));
        }

        /// <summary>
        /// 
        /// </summary>
        public void GoToPreviousPage()
        {
            UpdateIndex(Math.Max(index - PageSize, 0));
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return imageList.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Index
        {
            get { return index; }
            set { UpdateIndex(value); }
        }

        /// <summary>
        /// Adicionar una imagen
        /// </summary>
        /// <param name="host"></param>
        /// <param name="imagePath"></param>
        /// <param name="about"></param>
        /// <param name="desc"></param>
        public void Add(string host, string imagePath, string about, string desc, int ID, int LAST_INDEX, string LAST_FILTER)
        {
            Add(new ImageInfo(host, imagePath,about,desc,ID,LAST_INDEX,LAST_FILTER));
        }

        /// <summary>
        /// Obtener la imagen actual
        /// </summary>
        /// <returns></returns>
        public ImageInfo getActual()
        {
            return imageList[index];
        }

        /// <summary>
        /// Obtener la imagen actual
        /// </summary>
        /// <returns></returns>
        public void actualizarColores()
        {
            ImageInfo im = imageList[index];
            //imageList.Remove(index);

        }

        /// <summary>
        /// Operacion para eliminar todas las imagenes
        /// </summary>
        public void clear()
        {
            imageList.Clear();
            UpdateRange(index);
            this.index = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int getTamanio()
        {
            return imageList.Count;
        }

    }
}
