﻿using IMC.Wpf.CoverFlow.NMT.ChartComponent;
using IMC.Wpf.CoverFlow.NMT.ChartComponent.Reportes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para ChartGraficaAreaTest y se pretende que
    ///contenga todas las pruebas unitarias ChartGraficaAreaTest.
    ///</summary>
    [TestClass()]
    public class ChartGraficaAreaTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de la prueba que proporciona
        ///la información y funcionalidad para la ejecución de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales
        // 
        //Puede utilizar los siguientes atributos adicionales mientras escribe sus pruebas:
        //
        //Use ClassInitialize para ejecutar código antes de ejecutar la primera prueba en la clase 
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup para ejecutar código después de haber ejecutado todas las pruebas en una clase
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize para ejecutar código antes de ejecutar cada prueba
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup para ejecutar código después de que se hayan ejecutado todas las pruebas
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Una prueba de datosGrid
        ///</summary>
        [TestMethod()]
        public void datosGridTest()
        {
           // ChartGraficaArea target = new ChartGraficaArea(); // TODO: Inicializar en un valor adecuado
            IChart chart = new Clientes(); // TODO: Inicializar en un valor adecuado
        //    target.datosGrid(chart);
            Assert.Inconclusive("Un método que no devuelve ningún valor no se puede comprobar.");
        }
    }
}
