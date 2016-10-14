<%@ Assembly Name="WebPartPageProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=705eb4c80c166132" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI.WebControls.WebParts" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebPartPage.aspx.cs" Inherits="WebPartPageProject.Layouts.WebPartPageProject.WebPartPage"
    DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <h1>
        Webpart Introduction</h1>
    <%--<SharePoint:SPWebPartManager runat="server" ID="spWebPartManager" />--%>
    <table width="100%" border="1">
        <tr>
            <td colspan="2">
                <asp:WebPartZone runat="server" ID="headerZone" HeaderText="Header Zone">
                </asp:WebPartZone>
            </td>
        </tr>
        <tr>
            <td style="width:50%">
                <asp:WebPartZone runat="server" ID="leftZone" HeaderText="Left Zone">
                    <ZoneTemplate>
                        <a href="http://www.computacenter.com">Computacenter</a> 
                        <a href="http://www.microsoft.com">
                            Microsoft</a>
                            <a href="http://www.apress.com">Apress</a>
                    </ZoneTemplate>
                </asp:WebPartZone>
            </td>
            <td style="width:50%">
                <asp:WebPartZone runat="server" ID="rightZone" HeaderText="Right Zone">                   
                    <ZoneTemplate>                       
                        <asp:Label runat="server" ID="lbl1" Text="Right Label"></asp:Label>
                    </ZoneTemplate>
                </asp:WebPartZone>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:CatalogZone runat="server" ID="catZone">
                    <ZoneTemplate>
                        <asp:PageCatalogPart runat="server" ID="catalogZonePart" Title="Page Parts">
                        </asp:PageCatalogPart>
                        <asp:DeclarativeCatalogPart runat="server" ID="declarativeZonePart" Title="Catalogue">
                            <WebPartsTemplate>
                                <My:customwebpart runat="Server" id="custom1" />
                            </WebPartsTemplate>
                        </asp:DeclarativeCatalogPart>
                    </ZoneTemplate>
                </asp:CatalogZone>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:LinkButton runat="server" ID="lnkMode" Text="Browse Mode" OnClick="lnkMode_Click"></asp:LinkButton>
                <asp:LinkButton runat="server" ID="lnkCatM" Text="Catalog Mode" OnClick="lnkCatMode_Click"></asp:LinkButton>
            </td>
        </tr>
         <tr>
            <td colspan="2">
                <asp:Button runat="server" ID="btnAddWebpart" Text="Add Web Part" OnClick="btnAddWebpart_Click" />
            </td>
        </tr>
    </table>
                               <SharePoint:ListViewWebPart runat="server" id="listView1" Title="List 1"  />
 </asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Application Page
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    My WebPart Page
</asp:Content>
