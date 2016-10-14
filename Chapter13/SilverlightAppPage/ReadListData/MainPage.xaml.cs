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

namespace ReadListData
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }
        Web web;
        ListItemCollection allItems;

        public class BoundItem
        {
            public string Modified { get; set; }
            public string Name { get; set; }
            public int ID { get; set; }
        }

        private void ClientSuccessWeb(object sender,
                                       ClientRequestSucceededEventArgs e)
        {
            Dispatcher.BeginInvoke(() => txtWebName.Text = web.Title);
        }

        private void ClientSuccessFiles(object sender,
                                        ClientRequestSucceededEventArgs e)
        {
            try
            {

                List<BoundItem> items = new List<BoundItem>();
                foreach (ListItem item in allItems)
                {
                    items.Add(new BoundItem()
                    {
                        Modified = item["Modified"].ToString(),
                        Name = item.DisplayName,
                        ID = item.Id
                    });
                }
                Dispatcher.BeginInvoke(() => txtWebName.Text =
                           String.Format("{0} Entries", items.Count()));
                Dispatcher.BeginInvoke(() => dataGridXAPFiles.ItemsSource = items);
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(() => txtWebName.Text = ex.Message);
            }
        }

        private void ClientSaveFiles(object sender,
                                     ClientRequestSucceededEventArgs e)
        {
            Dispatcher.BeginInvoke(() => txtWebName.Text = success);
        }


        private void ClientFailed(object sender, ClientRequestFailedEventArgs e)
        {
            string msg = e.Exception.Message;
            if (String.IsNullOrEmpty(msg) && e.Exception.InnerException != null)
            {
                msg = e.Exception.InnerException.Message;
            }
            Dispatcher.BeginInvoke(() => txtWebName.Text = "Error: "

                                                         + msg);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            using (ClientContext ctx = new ClientContext("http://sharepointserve/"))
            {
                try
                {
                    web = ctx.Web;
                    ctx.Load(web);
                    ctx.ExecuteQueryAsync(
                       new ClientRequestSucceededEventHandler(ClientSuccessWeb),
                       new ClientRequestFailedEventHandler(ClientFailed));

                    xapList = web.Lists.GetByTitle("XAPFiles");
                    CamlQuery caml = new CamlQuery();
                    allItems = xapList.GetItems(caml);
                    ctx.Load(allItems,
                        files => files.Include(
                            file => file.Id,
                            file => file.DisplayName,
                            file => file["Modified"])
                            );
                    ctx.ExecuteQueryAsync(
                       new ClientRequestSucceededEventHandler(ClientSuccessFiles),
                       new ClientRequestFailedEventHandler(ClientFailed));
                }
                catch (Exception ex)
                {
                    txtWebName.Text = "Execution error: " + ex.Message;
                }
            }
        }

        List xapList;
        string success;

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // Save
            using (ClientContext ctx =
                  new ClientContext("http://sharepointserve/"))
            {
                try
                {
                    xapList = ctx.Web.Lists.GetByTitle("XAPFiles");
                    foreach (ListItem item in allItems)
                    {
                        int id = item.Id;
                        ListItem serverItem = xapList.GetItemById(item.Id);
                        serverItem["Title"] = "Modified at "
                                            + DateTime.Now.ToLongTimeString();
                        serverItem.Update();
                    }
                    xapList.Update();
                    success = "Saved";
                    ctx.ExecuteQueryAsync(
                      new ClientRequestSucceededEventHandler(ClientSaveFiles),
                      new ClientRequestFailedEventHandler(ClientFailed));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }

        }

    }
}
