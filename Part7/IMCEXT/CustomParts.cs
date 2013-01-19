using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.IO;

namespace IMC.Wpf.CoverFlow.NMT.IMCEXT
{
    class CustomParts
    {
        public static CheckBox cb;        
        public static void AddSelectAll(int id,Controls.ComboWithCheckboxes cbN,StackPanel spFiltros,IndicadorWindow w)
        {
            cb= new CheckBox();
            if ((id==1 || id == 2 || id == 3 || id == 4 ) && (!cbN.Name.ToLower().Contains("code")))
            {                
                cb.Content = "Seleccionar todos";
                cb.Name = "cb0" + cbN.Name;   
                cb.Checked += new RoutedEventHandler(delegate(object sender, RoutedEventArgs er)
                {
                    valor = false;
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
                                //if (t.Name.Contains("ProductSubfamilyDescription"))
                                if (t.Name.Contains("Marca"))
                                {
                                    nameDependiente += "ProductSubfamilyCode";
                                    if (CustomParts.GetCWCFromStackPanel(w.spFiltros, nameDependiente) == null)
                                    {
                                        nameDependiente += "SubfamilyCode";
                                    }
                                }
                                //else if (t.Name.Contains("BrandDescription"))
                                else if (t.Name.Contains("Subfamilia"))
                                    nameDependiente += "BrandCode";
                                //else if (t.Name.Contains("BrandDescription"))
                                else if (t.Name.Contains("Subfamilia"))
                                    nameDependiente += "BrandCode";
                                else 
                                    nameDependiente = t.Name;

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
                                 if (valor != true)
                                     w.asignarTextoDependiente(t.Name, selAll);
                                
                               
                                string nameDependiente = "cbN_";
                                //if (t.Name.Contains("ProductSubfamilyDescription"))
                                if (t.Name.Contains("Marca"))
                                {
                                    nameDependiente += "ProductSubfamilyCode";
                                    if (CustomParts.GetCWCFromStackPanel(w.spFiltros, nameDependiente) == null)
                                    {
                                        nameDependiente += "SubfamilyCode";
                                    }
                                }
                                //else if (t.Name.Contains("BrandDescription"))
                                else if (t.Name.Contains("Subfamilia"))
                                    nameDependiente += "BrandCode";
                                //else if (t.Name.Contains("BrandDescription"))
                                else if (t.Name.Contains("Subfamilia"))
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
        }
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
        public static int GetDirState(string serverPath)
        {
            try
            {
                string[] res;
                res = Directory.GetFiles(serverPath);
            }
            catch (IOException ioEx)
            {
                return 1;
            }
            catch (UnauthorizedAccessException auEx)
            {
                return 2;
            }
            catch (Exception ex)
            {
                return 3;
            }
            return 0;
        }
        public static bool valor = false;
        public void ValorCheck(bool p)
        {
            valor = p;
            if (p == true)
                cb.IsChecked = false;

        }
    }
}
