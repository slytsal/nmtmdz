using System.Windows.Media;
namespace Ded.Tutorial.Wpf.CoverFlow.Part7.FlowComponent
{
    public interface IThumbnailManager
    {
        ImageSource GetThumbnail(string host, string path);
    }
}
