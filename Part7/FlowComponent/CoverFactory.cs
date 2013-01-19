using System.Windows.Media.Media3D;
namespace IMC.Wpf.CoverFlow.NMT
{
    internal class CoverFactory : ICoverFactory
    {
        private readonly ModelVisual3D visualModel;
        public CoverFactory(ModelVisual3D visualModel)
        {
            this.visualModel = visualModel;
        }
        #region ICoverFactory Members
        public ICover NewCover(string host, string path, int coverPos, int currentPos, string about, string desc, int ID, int LAST_INDEX, string LAST_FILTER)
        {
            return new Cover(new ImageInfo(host, path,about,desc,ID,LAST_INDEX,LAST_FILTER), coverPos, currentPos, visualModel);
        }
        #endregion
    }
}
