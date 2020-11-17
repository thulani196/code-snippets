$rgName = "WUS-WCM-CDN-ROW-PROD"
$profileName = "WUS-WCM-TrafficManager-Global"
$rowEndpointName = "WUS-WCM-TrafficManager-ROW-Endpoint"
$chinaEndpointName = "WUS-WCM-TrafficManager-China-Endpoint"

$location = "Global"
$rowTargetUrl = "wus-wcm-trafficmanager-row.trafficmanager.net"
$chinaTargetUrl = "wcn-ws-tm-china-dev.trafficmanager.cn"

$dnsName = "wus-wcm-trafficmanager-global"

$chinaGeoMapping = @("CN","HK","MO","TW")

# Set-AzContext -Subscription 'WCM-WS-CDN-ROW'

#
# Resource Group
#

# get resource group
$rg = (Get-AzResourceGroup -Name $rgName)

# create resource group if not exists
if ($null -eq $rg) {
  $rg = New-AzResourceGroup -Name $rgName -Location $location -WhatIf
}

#
# Traffic Manager
# Geographic profile
#

$dnsName = $profileName.ToLower()

# Create Profile
$tmProfile = New-AzTrafficManagerProfile -Name $profileName -ResourceGroupName $rgName -TrafficRoutingMethod Geographic -RelativeDnsName $dnsName -Ttl 30 -MonitorProtocol HTTP -MonitorPort 80 -MonitorPath "/" -WhatIf

# Create Endpoints (China and ROW)
Add-AzTrafficManagerEndpointConfig -EndpointName $rowEndpointName -TrafficManagerProfile $tmProfile -Type ExternalEndpoints -Target $rowTargetUrl -Priority 1 -EndpointStatus Enabled -WhatIf
Add-AzTrafficManagerEndpointConfig -EndpointName $chinaEndpointName -TrafficManagerProfile $tmProfile -Type ExternalEndpoints -Target $chinaTargetUrl -Priority 2 -GeoMapping $chinaGeoMapping -EndpointStatus Enabled -WhatIf
Set-AzTrafficManagerProfile -TrafficManagerProfile $tmProfile

# Check Profile
Get-AzTrafficManagerProfile -Name $profileName
