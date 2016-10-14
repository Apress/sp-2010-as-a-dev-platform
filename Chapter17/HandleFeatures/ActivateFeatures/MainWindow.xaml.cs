using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;

namespace Apress.SP2010.ActivateFeatures
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SPSite site;
        SPWeb web;

        public MainWindow()
        {
            InitializeComponent();
            txtUrl.Text = "http://sharepointserve";
        }

        private void ReadSource()
        {
            if (!String.IsNullOrEmpty(txtUrl.Text.Trim()))
            {
                try
                {
                    site = new SPSite(txtUrl.Text);
                    web = site.RootWeb;
                    var features = from f in site.Features
                                   where f.Definition.Hidden == checkBox1.IsChecked
                                   select f.Definition;
                    lstSite.ItemsSource = features;
                    var farmdefs = from f in SPFarm.Local.FeatureDefinitions
                                   where (f.Scope == SPFeatureScope.Web || f.Scope == SPFeatureScope.Site)
                                   && !features.Contains(f)
                                   select f;
                    lstFarm.ItemsSource = farmdefs;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                }
            }
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            ReadSource();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (lstFarm.SelectedIndex != -1)
            {
                SPFeatureDefinition definition = lstFarm.SelectedItem as SPFeatureDefinition;
                if (definition.Scope == SPFeatureScope.Site)
                {
                    site.Features.Add(definition.Id);
                }
                if (definition.Scope == SPFeatureScope.Web)
                {
                    web.Features.Add(definition.Id);
                }
                ReadSource();
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lstSite.SelectedIndex != -1)
            {
                SPFeatureDefinition definition = lstSite.SelectedItem as SPFeatureDefinition;
                site.Features.Remove(definition.Id);
                ReadSource();
            }
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            ReadSource();
        }

    }
}
