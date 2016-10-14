<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RetrieveUsers.aspx.cs" Inherits="UserManagement.Layouts.UserManagement.RetrieveUsers" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
<h3>Current User</h3>
    <asp:Label runat="server" ID="lblCurrent" />

<h3>All Users</h3>
    <asp:GridView runat="server" ID="gvUsers" OnRowCommand="gvUsers_RowCommand" 
                  AutoGenerateColumns="false"
                  DataKeyNames="LoginName"  >
        <Columns>
            <asp:BoundField DataField="Name" />
            <asp:BoundField DataField="LoginName" />
            <asp:BoundField DataField="EMail" />
            <asp:ButtonField CommandName="Show Details" ButtonType="Link" Text="Show" />
        </Columns>
    </asp:GridView>
    <hr />
     <asp:GridView runat="server" ID="gvRoles" />
     <asp:GridView runat="server" ID="gvGroups" />
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Retrieve all Users
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Retrieve and analyze users
</asp:Content>
