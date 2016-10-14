using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SharePoint.Client;

namespace RetrieveListItems
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        ListItemCollection itemColl;

        private void ClientSuccess(object sender, ClientRequestSucceededEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                dgResult.DataContext = itemColl;
            });
        }

        private void ClientFailed(object sender,
                                   ClientRequestFailedEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Exception", MessageBoxButton.OK);
        }

        private void GetAllItems(string list)
        {
            using (ClientContext ctx = new ClientContext("http://sharepointserve/"))
            {
                try
                {

                    List lst = ctx.Web.Lists.GetByTitle(list);
                    CamlQuery q = new CamlQuery();
                    itemColl = lst.GetItems(q);
                    ctx.Load(itemColl, fields => fields.Include(
                        field => field["Title"]));
                    ctx.ExecuteQueryAsync(
                       new ClientRequestSucceededEventHandler(ClientSuccess),
                       new ClientRequestFailedEventHandler(ClientFailed));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK);
                }
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string list = txtList.Text;
            if (!String.IsNullOrEmpty(list))
            {
                GetAllItems(list);
            }
        }
    }
}
