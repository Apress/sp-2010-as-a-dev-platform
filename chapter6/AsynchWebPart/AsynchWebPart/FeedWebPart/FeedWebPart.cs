using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Web.UI.HtmlControls;

namespace AsynchWebPart
{
    [ToolboxItemAttribute(false)]
    public class FeedWebPart : WebPart
    {
        public FeedWebPart()
        {
            currentState = State.Undefined;
        }

        private enum State
        {
            Undefined,
            Loading,
            Loaded,
            Timeout,
            NoAsync
        }

        private WebRequest rssRequest;
        private WebResponse rssResponse;
        private XDocument xml;
        private State currentState;

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (String.IsNullOrEmpty(FeedUrl))
                return;
            if (WebPartManager.DisplayMode == WebPartManager.DisplayModes["Design"])
                return;
            Page.AsyncTimeout = TimeSpan.FromSeconds(10); // 10 sec
            Page.RegisterAsyncTask(
                new PageAsyncTask(
                    new System.Web.BeginEventHandler(BeginRSSRead),
                    new System.Web.EndEventHandler(EndRSSRead),
                    new System.Web.EndEventHandler(TimeOutRSSRead),
                    null,
                    true));
        }

        private IAsyncResult BeginRSSRead(object sender, EventArgs e, AsyncCallback cb, object state)
        {
            currentState = State.Loading;
            rssRequest = HttpWebRequest.Create(FeedUrl);
            return rssRequest.BeginGetResponse(cb, state);
        }

        private void EndRSSRead(IAsyncResult ar)
        {
            rssResponse = rssRequest.EndGetResponse(ar);
            Stream response = rssResponse.GetResponseStream();
            XmlReader reader = XmlReader.Create(response);
            xml = XDocument.Load(reader);
            currentState = State.Loaded;

            WriteControls();
        }

        private void TimeOutRSSRead(IAsyncResult ar)
        {
            currentState = State.Timeout;
            WriteControls();
        }

        private void WriteControls()
        {
            switch (currentState)
            {
                case State.Loaded:
                    HtmlGenericControl ctrl = new HtmlGenericControl("pre");
                    ctrl.InnerText = xml.ToString();
                    Controls.Add(ctrl);
                    break;
                case State.Timeout:
                    Label lt = new Label();
                    lt.Text = "RSS Feed timed out";
                    lt.ForeColor = System.Drawing.Color.Red;
                    Controls.Add(lt);
                    break;
                case State.NoAsync:
                    Label nl = new Label();
                    nl.Text = "Asynch not supported.";
                    Controls.Add(nl);
                    break;
                default:
                    Label ll = new Label();
                    ll.Text = "Loading...";
                    Controls.Add(ll);
                    break;
            }
        }

        [Personalizable(true)]
        [WebBrowsable(true)]
        [WebDescription("RSS Feed URL")]
        [WebDisplayName("Feed URL")]
        [Category("Feed Properties")]
        public string FeedUrl
        {
            get;
            set;

        }

    }
}
