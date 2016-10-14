<%@ Assembly Name="WebPartPageProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=705eb4c80c166132" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BingWebPartUserControl.ascx.cs" Inherits="WebPartPageProject.BingWebPart.BingWebPartUserControl" %>
<script type="text/javascript" src="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2"></script>
<script type="text/javascript">

/// <reference path="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2" />
function GetMap() {
    var map = new VEMap('<% = spBingMap.ClientID %>');
    var lat = document.getElementById('<% = latField.ClientID  %>').value;
    var lng = document.getElementById('<% = lngField.ClientID  %>').value;
    var latlng = new VELatLong(lat, lng);
    map.LoadMap(latlng, 10, 'r', false);
}
window.onload = function () {
    GetMap();
}
</script>
<div id="spBingMap" runat="server" style="position:relative; width:400px; height:300px"></div>
<asp:HiddenField runat="server" ID="latField" Value="52.222" />
<asp:HiddenField runat="server" ID="lngField" Value="13.297" />
