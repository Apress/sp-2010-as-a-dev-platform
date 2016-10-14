using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.SharePoint.Client;

namespace RetrieveAllLists
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            GetAllLists();
        }

        ListCollection listColl;

        private void ClientSuccess(object sender, ClientRequestSucceededEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                lbAllLists.DataContext = listColl;
            });
        }

        private void ClientFailed(object sender, ClientRequestFailedEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Exception", MessageBoxButton.OK);
        }

        private void GetAllLists()
        {
            using (ClientContext ctx = new ClientContext("http://sharepointserve/"))
            {
                try
                {
                    listColl = ctx.Web.Lists;
                    ctx.Load(listColl, lists => lists.Include(list => list.Title, list => list.Description));
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

    }
}
