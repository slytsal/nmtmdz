using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace IMC.Wpf.CoverFlow.NMT.IMCEXT
{
    class CustomParts
    {
        public static void AddSelectAll(int id,Controls.ComboWithCheckboxes cbN,StackPanel spFiltros,IndicadorWindow w)
        {
            if ((id == 9 || id == 3 || id == 2 || id == 15 || id == 12) && (!cbN.Name.ToLower().Contains("code")))
            {
                CheckBox cb = new CheckBox();
                cb.Content = "Seleccionar todos";
                cb.Name = "cb0" + cbN.Name;
                cb.Checked += new RoutedEventHandler(delegate(object sender, RoutedEventArgs er)
                {
                    foreach (Object o in spFiltros.Children)
                    {
                        if (o is Controls.ComboWithCheckboxes)
                        {
                            Controls.ComboWithCheckboxes t = ((Controls.ComboWithCheckboxes)o);

                            if (t.Name == ((CheckBox)sender).Name.Split('0')[1].ToString())
                            {
                                string selAll = "";

                                selAll = CustomParts.GetAllCWCString(t);
                                w.asignarTextoDependiente(t.Name, selAll);

                                string nameDependiente = "cbN_";
                                if (t.Name.Contains("ProductSubfamilyDescription"))
                                {
                                    nameDependiente += "ProductSubfamilyCode";
                                    if (CustomParts.GetCWCFromStackPanel(w.spFiltros, nameDependiente) == null)
                                    {
                                        nameDependiente += "SubfamilyCode";
                                    }
                                }
                                else if (t.Name.Contains("BrandDescription"))
                                    nameDependiente += "BrandCode";
                                else if (t.Name.Contains("BrandDescription"))
                                    nameDependiente += "BrandCode";

                                t = CustomParts.GetCWCFromStackPanel(w.spFiltros, nameDependiente);
                                selAll = CustomParts.GetAllCWCString(t);
                                w.asignarTextoDependiente(nameDependiente, selAll);
                            }
                        }
                    }
                });
                cb.Unchecked += new RoutedEventHandler(delegate(object sender, RoutedEventArgs er)
                {
                    foreach (Object o in spFiltros.Children)
                    {
                        if (o is Controls.ComboWithCheckboxes)
                        {
                            Controls.ComboWithCheckboxes t = ((Controls.ComboWithCheckboxes)o);

                            if (t.Name == ((CheckBox)sender).Name.Split('0')[1].ToString())
                            {
                                string selAll = "";
                                w.asignarTextoDependiente(t.Name, selAll);

                                string nameDependiente = "cbN_";
                                if (t.Name.Contains("ProductSubfamilyDescription"))
                                {
                                    nameDependiente += "ProductSubfamilyCode";
                                    if (CustomParts.GetCWCFromStackPanel(w.spFiltros, nameDependiente) == null)
                                    {
                                        nameDependiente += "SubfamilyCode";
                                    }
                                }
                                else if (t.Name.Contains("BrandDescription"))
                                    nameDependiente += "BrandCode";
                                else if (t.Name.Contains("BrandDescription"))
                                    nameDependiente += "BrandCode";
                                w.asignarTextoDependiente(nameDependiente, selAll);
                            }
                        }
                    }
                });
                spFiltros.Children.Add(cb);
            }
        }/////////////

        public static Controls.ComboWithCheckboxes GetCWCFromStackPanel(StackPanel sp,string name)
        {
            foreach (Object o in sp.Children)
            {
                if (o is Controls.ComboWithCheckboxes)
                {
                    if (((Controls.ComboWithCheckboxes)o).Name == name)
                    {
                        return ((Controls.ComboWithCheckboxes)o);
                    }
                }
            }
            return null;
        }//////////////

        public static string GetAllCWCString(Controls.ComboWithCheckboxes cc)
        {
            string selAll = "";
            foreach (Controls.Node node in ((Controls.ObservableNodeList)cc.ItemsSource))
            {
                selAll += node.Title + ",";
            }
            if (selAll.Length > 0)
            {
                selAll.Substring(0, selAll.Length - 1);
            }
            
            return selAll;
        }//////////////
    }
}
