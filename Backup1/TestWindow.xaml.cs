using System.Windows;
using System.Windows.Input;
using System;
using System.IO;
using System.Collections.Generic;
namespace Ded.Tutorial.Wpf.CoverFlow.Part7
{
    public partial class TestWindow : Window
    {
        private class FileInfoComparer : IComparer<FileInfo>
        {
            #region IComparer<FileInfo> Membres
            public int Compare(FileInfo x, FileInfo y)
            {
                return string.Compare(x.FullName, y.FullName);
            }
            #endregion
        }

        #region Handlers
        private void DoKeyDown(Key key)
        {
            switch (key)
            {
                case Key.Right:
                    flow.GoToNext();
                    break;
                case Key.Left:
                    flow.GoToPrevious();
                    break;
                case Key.PageUp:
                    flow.GoToNextPage();
                    break;
                case Key.PageDown:
                    flow.GoToPreviousPage();
                    break;
            }
            if (flow.Index != Convert.ToInt32(slider.Value))
                slider.Value = flow.Index;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            DoKeyDown(e.Key);
        }
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            flow.Index = Convert.ToInt32(slider.Value);
        }
        #endregion
        #region Private stuff
        public void Load(string imagePath)
        {
            var imageDir = new DirectoryInfo(imagePath);
            var images = new List<FileInfo>(imageDir.GetFiles("*.jpg"));
            images.Sort(new FileInfoComparer());
            foreach (FileInfo f in images)
                flow.Add(Environment.MachineName, f.FullName);
        }
        #endregion
        public TestWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowState = WindowState.Maximized;
            
          

            flow.Cache = new ThumbnailManager();
            Load(@"c:\_covers");
            slider.Minimum = 0;
            slider.Maximum = flow.Count - 1;
        }
    }
}
