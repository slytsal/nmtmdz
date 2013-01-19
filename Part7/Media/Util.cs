using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading;

namespace IMC.Wpf.CoverFlow.NMT.Media
{
    public class Util
    {
        private static Util instance;
        private string sound { get; set; }

        /// <summary>
        ///  Patron de disenio solitario
        /// </summary>
        public static Util Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Util();
                }
                return instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sound"></param>
        //public void playSound(string sound)
        //{
        //    this.sound = sound;
        //    ThreadPool.QueueUserWorkItem(new WaitCallback(updateThreadProc), this);
            
        //}
        public void playSound(string sound)

        {

            this.sound = sound;

            string sysSetupFolder=System.Configuration.ConfigurationManager.AppSettings["CarpetaInstalacion"].ToString();

            if (String.IsNullOrEmpty(sysSetupFolder))

            {

                sysSetupFolder = @"C:\Programs\NMT\";

            }

            else

            {

                sysSetupFolder += @"\";

            }

            this.sound = sysSetupFolder + sound;

            ThreadPool.QueueUserWorkItem(new WaitCallback(updateThreadProc), this);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        static void updateThreadProc(object sender)
        {
            try
            {
                Util s = (Util)sender;
                SoundPlayer sp = new SoundPlayer(s.sound);
                sp.Play();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}
