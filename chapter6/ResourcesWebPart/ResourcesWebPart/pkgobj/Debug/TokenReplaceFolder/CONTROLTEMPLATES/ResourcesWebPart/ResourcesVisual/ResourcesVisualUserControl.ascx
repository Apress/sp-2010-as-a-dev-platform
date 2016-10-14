<%@ Assembly Name="ResourcesWebPart, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e781a26c9d8998cf" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResourcesVisualUserControl.ascx.cs" Inherits="ResourcesWebPart.ResourcesVisualUserControl" %>
This is a static resource text: 
<asp:Label runat="server" ID="lblStatic" Text="<%$ Resources:VisualGlobalResource,lblGlobalResource.Text %>" />
<br />
This is pulled from resource using Meta resources:
<asp:Label runat="server" ID="lblGlobal" meta:resourcekey="lblGlobalResource" />
<br />
This is read from the assembly:
<asp:Label runat="server" ID="lblEmbedded"  />

