<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupEditor.aspx.cs" Inherits="VisualWebPartEditor.Layouts.VisualWebPartEditor.PopupEditor"
    MasterPageFile="~/_layouts/dialog.master" %>

<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderAdditionalPageHead">
    <script language="javascript" type="text/javascript">

    var isOkay = false;
    var oldValue = "";

    function addOnLoadEvent(func) {           
        var oldonload = window.onload;   
        if (typeof window.onload != 'function') {
            window.onload = func;
        } else {
            window.onload = function () {
                if (oldonload) {   
                    oldonload();
                }
                func();
            }   
        }   
     }

     function addOnUnLoadEvent(func) {
         var oldonunload = window.onunload;
         if (typeof window.onunload != 'function') {
             window.onunload = func;
         } else {
             window.onunload = function () {
                 if (oldonunload) {
                     oldonunload();
                 }
                 func();
             }
         }
     }

     addOnLoadEvent(function () {
         var input = window.dialogArguments;
         var field = document.getElementById("<%= urlField.ClientID %>");
         oldValue = input;
         field.value = input;
     });

     addOnUnLoadEvent(function () {
         var field = document.getElementById("<%= urlField.ClientID %>");
         window.returnValue = (isOkay) ? field.value : oldValue;
     });

    function CloseOkButton() {
        isOkay = true;
        doCancel();
    }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderDialogDescription">
    Edit the URL field.
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderDialogBodyMainSection">
    Please enter a valid URL:
   <asp:TextBox runat="server" ID="urlField" /> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderDialogBodyFooterMainSection">
</asp:Content>