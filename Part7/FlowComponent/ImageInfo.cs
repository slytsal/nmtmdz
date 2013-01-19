namespace IMC.Wpf.CoverFlow.NMT
{
    public class ImageInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="path"></param>
        /// <param name="about"></param>
        /// <param name="desc"></param>
        public ImageInfo(string host, string path, string about, string desc, int ID, int LAST_INDEX, string LAST_FILTER)
        {
            this.Host = host;
            this.Path = path;
            this.About = about;
            this.Desc = desc;
            this.ID = ID;
            this.LAST_INDEX = LAST_INDEX;
            this.LAST_FILTER = LAST_FILTER;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Host { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string Path { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string About { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string Desc { get; private set; }

        public int ID { get; private set; }

        public int LAST_INDEX { get; set; }

        public string LAST_FILTER { get; set; }
    }
}
