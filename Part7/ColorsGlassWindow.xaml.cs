﻿// -------------------------------------------------------------------------------
// 
// This file is part of the FluidKit project: http://www.codeplex.com/fluidkit
// 
// Copyright (c) 2008, The FluidKit community 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
// 
// * Redistributions of source code must retain the above copyright notice, this 
// list of conditions and the following disclaimer.
// 
// * Redistributions in binary form must reproduce the above copyright notice, this 
// list of conditions and the following disclaimer in the documentation and/or 
// other materials provided with the distribution.
// 
// * Neither the name of FluidKit nor the names of its contributors may be used to 
// endorse or promote products derived from this software without specific prior 
// written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR 
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES 
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON 
// ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT 
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
// -------------------------------------------------------------------------------
using System.Windows;
using System.Windows.Input;
using System;
using IMC.Wpf.CoverFlow.NMT;
using NMT_ESC_BLL;
using NMT_ESC_DAL;
using System.Data;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Collections.Specialized;
using FluidKit.Controls;
using System.Diagnostics;
using System.Windows.Media;
using System.Collections.Generic;
using OpenSourceControls;
using IMC.Wpf.CoverFlow.NMT.Chartcomponent.Domain;
namespace FluidKit.NMT.GlassWindow
{

    
	    
    /// <summary>
	/// 	Interaction logic for Window1.xaml
	/// </summary>
	public partial class ColorsGlassWindow
	{
        public Window parentWin { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        public ColorsGlassWindow(Window parent,int id)
		{
			InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;                     
            this.parentWin = parent;
            List<SeriesColor> series;
            series = ((IndicadorWindow)this.parentWin).chart.obtenerSeries();
            
            foreach(SeriesColor serTmp in series)
            {
                Label l = new Label();
                l.Content = serTmp.nombre;
                l.Foreground = new SolidColorBrush(Colors.White);
                spColors.Children.Add(l);
                SmallColorPicker sm = new SmallColorPicker();
                sm.Width = 50;
                sm.SelectedColor = serTmp.color;
                spColors.Children.Add(sm);
                
            }
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorPicker_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            //if (Changes!=null) Changes.Items.Add(ColorPicker.SelectedColor.ToString());
            //Debug.WriteLine("Changed to " + ColorPicker.SelectedColor.ToString());
        }

        /// <summary>
        /// Actualiza los colores de la grafica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            List<Color> colores = new List<Color>();
            List<Color> colores2 = new List<Color>();
            
            foreach (UIElement el in spColors.Children)
            {
                    if (el.GetType().Name == "SmallColorPicker")
                    {
                        Color c = ((SmallColorPicker)el).SelectedColor;
                        colores.Add(c);
                   }
                               
            }
            ((IndicadorWindow)this.parentWin).chagePaleta(colores);
        }
    
	}
}