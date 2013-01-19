using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using System.Windows;
using FluidKit.NMT.GlassWindow;

namespace NMT_ESC_BLL
{
    public class RunFromClientAppWithEventsCS
    {
        private static RunFromClientAppWithEventsCS instance;
        public MyEventListener eventListener { get; set; }
        public ConfigGlassWindow parentWin { get; set; }
        /// <summary>
        ///  Patron de disenio solitario
        /// </summary>
        public static RunFromClientAppWithEventsCS Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RunFromClientAppWithEventsCS();
                }
                return instance;
            }
        }

        /// <summary>
        /// Metodo para lanzar las ETL
        /// </summary>
        /// <param name="pkgLocation"></param>
        public void lanzarETL(string pkgLocation, ConfigGlassWindow parent)
        {            
            Package pkg;
            Microsoft.SqlServer.Dts.Runtime.Application app;
            DTSExecResult pkgResults;
            parentWin = parent;
            eventListener = new MyEventListener(parentWin);
            //pkgLocation =
            //  @"C:\Program Files\Microsoft SQL Server\100\Samples\Integration Services" +
            //  @"\Package Samples\CalculatedColumns Sample\CalculatedColumns\CalculatedColumns.dtsx";
            app = new Microsoft.SqlServer.Dts.Runtime.Application();
            pkg = app.LoadPackage(pkgLocation, eventListener);
            //pkg.Variables.Add("strNombreServidorLocal", false, "", "ASINE1159\\SQLEXPRESS");
            //pkg.Variables["strNombreServidorLocal"].Value = "ASINE1159\\SQLEXPRESS";
            pkgResults = pkg.Execute(null, pkg.Variables, eventListener, null, null);
            //errores.Add(pkgResults.ToString());
            //Console.WriteLine(pkgResults.ToString());
            //Console.ReadKey();
        }

        public class MyEventListener : DefaultEvents
        {
            public List<string> errores { get; set; }
            public ConfigGlassWindow parentWin { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public MyEventListener(ConfigGlassWindow parent)
            {
                parentWin = parent;
                errores = new List<string>();
            }

           
            /// <summary>
            /// 
            /// </summary>
            /// <param name="taskHost"></param>
            /// <param name="progressDescription"></param>
            /// <param name="percentComplete"></param>
            /// <param name="progressCountLow"></param>
            /// <param name="progressCountHigh"></param>
            /// <param name="subComponent"></param>
            /// <param name="fireAgain"></param>
            public override void OnProgress(TaskHost taskHost, string progressDescription, int percentComplete, int progressCountLow, int progressCountHigh, string subComponent, ref bool fireAgain)
            {
                string inCadena = subComponent;
                errores.Add(progressDescription);
                parentWin.addText(progressDescription+"\n");
            }

           
            /// <summary>
            /// 
            /// </summary>
            /// <param name="source"></param>
            /// <param name="errorCode"></param>
            /// <param name="subComponent"></param>
            /// <param name="description"></param>
            /// <param name="helpFile"></param>
            /// <param name="helpContext"></param>
            /// <param name="idofInterfaceWithError"></param>
            /// <returns></returns>
            public override bool OnError(DtsObject source, int errorCode, string subComponent,
              string description, string helpFile, int helpContext, string idofInterfaceWithError)
            {
                // Add application-specific diagnostics here.
                string error = string.Format("Error in {0}/{1} : {2}", source, subComponent, description);
                errores.Add(error);
                return false;
            }
        }

    }
}
