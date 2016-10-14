using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace AsynchWebPart
{
    [ToolboxItemAttribute(false)]
    public class XmlFileWebPart : WebPart
    {

        ReadFileAsync rfa;
        Action result;

        public XmlFileWebPart()
        {
        }

        [WebBrowsable()]
        [Personalizable()]
        public string FileName
        {
            get;
            set;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            result = new Action(GetResult);
            if (!String.IsNullOrEmpty(FileName))
            {
                rfa = new ReadFileAsync(FileName, result);
                Page.AsyncTimeout = TimeSpan.FromSeconds(10); // 10 sec
                Page.RegisterAsyncTask(
                    new PageAsyncTask(
                        new System.Web.BeginEventHandler(rfa.OnBegin),
                        new System.Web.EndEventHandler(rfa.OnEnd),
                        new System.Web.EndEventHandler(rfa.OnTimeout),
                        null,
                        true));
            }
        }

        private void GetResult()
        {

        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
        }
    }
}
