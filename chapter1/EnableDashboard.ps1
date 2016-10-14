$level="OnDemand"
[void][System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SharePoint")
[void][System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SharePoint.Administration")
$contentSvc=[Microsoft.SharePoint.Administration.SPWebService]::ContentService
$contentSvc.DeveloperDashboardSettings.DisplayLevel=([Enum]::Parse([Microsoft.SharePoint.Administration.SPDeveloperDashboardLevel],$level))
$contentSvc.DeveloperDashboardSettings.Update()
Write-Host("Current Level: " + $contentSvc.DeveloperDashboardSettings.DisplayLevel)