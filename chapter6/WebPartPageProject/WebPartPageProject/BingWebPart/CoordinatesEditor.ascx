<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CoordinatesEditor.ascx.cs"
    Inherits="WebPartPageProject.BingWebPart.CoordinatesEditor" %>
<fieldset>
    <legend>
        <asp:Label runat="server" ID="lblControl" Font-Bold="true"></asp:Label></legend>
    <fieldset title="Longitude">
        <legend>Longitude </legend>
        <asp:TextBox ID="TextBoxDegLng" runat="server" Width="50px"></asp:TextBox>
        &deg;
        <asp:TextBox ID="TextBoxMinLng" runat="server" Width="50px"></asp:TextBox>"
        <asp:TextBox ID="TextBoxSecLng" runat="server" Width="50px"></asp:TextBox>'
    </fieldset>
    <fieldset title="Latitude">
        <legend>Latitude </legend>
        <asp:TextBox ID="TextBoxDegLat" runat="server" Width="50px"></asp:TextBox>
        &deg;
        <asp:TextBox ID="TextBoxMinLat" runat="server" Width="50px"></asp:TextBox>"
        <asp:TextBox ID="TextBoxSecLat" runat="server" Width="50px"></asp:TextBox>'
    </fieldset>
</fieldset>
