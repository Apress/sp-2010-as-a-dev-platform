<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EstimationTask.aspx.cs" Inherits="EstimationTaskContentType.Layouts.ProjectApprovalForms.EstimationTask" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
<div>
Task Title:
<asp:Label ID="lblTitle" runat="server"></asp:Label>
<br />
<br />

Please estimate the project costs:
<asp:TextBox runat="server" ID="EstimatedCosts"></asp:TextBox>
<asp:RequiredFieldValidator id="valRequired" runat="server" ControlToValidate="EstimatedCosts"
    ErrorMessage="* You must enter Number" Display="dynamic" type="Integer">
</asp:RequiredFieldValidator>

<asp:radiobuttonlist id="Outcome" runat="server">
<asp:listitem id="Approve" runat="server" value="Approve" />
<asp:listitem id="Deny" runat="server" value="Deny" />
</asp:radiobuttonlist>

<br />
<br />
<asp:Label runat="server" ID="lblDebug"></asp:Label>

<asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" />
</div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Estimation
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Estimation
</asp:Content>
