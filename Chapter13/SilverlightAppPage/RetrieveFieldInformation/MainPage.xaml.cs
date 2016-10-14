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

namespace RetrieveFieldInformation
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        FieldCollection fieldColl;

        private void ClientSuccess(object sender,
                                    ClientRequestSucceededEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                lbAllFields.DataContext = fieldColl;
            });
        }

        private void ClientFailed(object sender,
                                   ClientRequestFailedEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Exception", MessageBoxButton.OK);
        }

        private void GetAllFields(string list)
        {
            using (ClientContext ctx = new ClientContext("http://sharepointserve/"))
            {
                try
                {
                    fieldColl = ctx.Web.Lists.GetByTitle(list).Fields;
                    ctx.Load(fieldColl, fields => fields.Include(field => field.InternalName, field => field.FieldTypeKind));
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

        private void btnRetrieve_Click(object sender, RoutedEventArgs e)
        {
            string list = txtList.Text;
            if (!String.IsNullOrEmpty(list))
            {
                GetAllFields(list);
            }
        }

    }
}
