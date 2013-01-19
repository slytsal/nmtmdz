namespace IMC.Wpf.CoverFlow.NMT
{
    public interface ICoverFactory
    {
        ICover NewCover(string host, string path, int coverPos, int currentPos, string about, string desc, int ID, int LAST_INDEX, string LAST_FILTER);
    }
}
