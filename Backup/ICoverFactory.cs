namespace Ded.Tutorial.Wpf.CoverFlow.Part7.FlowComponent
{
    public interface ICoverFactory
    {
        ICover NewCover(string host, string path, int coverPos, int currentPos);
    }
}
