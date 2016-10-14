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
using System.Xml.Linq;

namespace CreateList
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        class NewField
        {
            public string Name { get; set; } 
            public bool Integer { get; set; }
        }

        List<NewField> newFields;

        private void CreateList()
        {
            using (ClientContext ctx = new ClientContext("http://sharepointserve/"))
            {
                try
                {
                    Web web = ctx.Web;

                    ListCreationInformation listCreationInfo = new ListCreationInformation();
                    listCreationInfo.Title = txtName.Text;
                    listCreationInfo.TemplateType = (int)ListTemplateType.GenericList;

                    List oList = web.Lists.Add(listCreationInfo);
                    ctx.Load(oList);

                    foreach (NewField newField in newFields)
                    {
                        XElement fld = new XElement("Field", 
                                        new XAttribute("Authors", newField.Name),
                                        new XAttribute("Type", newField.Integer ? "Currency" : "Text"));
                        oList.Fields.AddFieldAsXml(fld.ToString(), true, AddFieldOptions.DefaultValue);
                    }

                    //string fldAuthorsXml = "<Field DisplayName='Authors' Type='Text' />";
                    //string fldPublisherXml = "<Field DisplayName='Publisher' Type='Text' />";
                    //string fldPriceXml = "<Field DisplayName='Price' Type='Currency' />";

                    //oList.Fields.AddFieldAsXml(fldAuthorsXml, true, AddFieldOptions.DefaultValue);
                    //oList.Fields.AddFieldAsXml(fldPublisherXml, true, AddFieldOptions.DefaultValue);
                    //oList.Fields.AddFieldAsXml(fldPriceXml, true, AddFieldOptions.DefaultValue);
                    //oList.Update();

                    ctx.ExecuteQuery();
                    MessageBox.Show("List successfully creates", "Done", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK);
                }
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateList();
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int fields = Convert.ToInt32(slider1.Value);
            newFields = new List<NewField>(fields);
            for (int i = 0; i < fields; i++)
            {
                newFields.Add(new NewField() { Name = "[Type Name]" });
            }
            dgFields.DataContext = newFields;
        }

    }
}
