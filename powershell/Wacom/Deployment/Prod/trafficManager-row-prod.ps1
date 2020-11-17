$rgName = "WUS-WCM-CDN-ROW-PROD"
$profileName = "WUS-WCM-TrafficManager-ROW"

$location = "Global"
$endpointVzName = "WUS-WCM-CDN-Endpoint"
$endpointSmName = "WUS-WCM-CDN-Secondary-Endpoint"

$cdnVzUrl = "https://wcm-cdn-vz-dev.azureedge.net"
$cdnSmUrl = "https://wcm-cdn-sm-dev.azureedge.net"

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
# Priority profile
#

$dnsName = $profileName.ToLower()

# Create Profile
$tmProfile = New-AzTrafficManagerProfile -Name $profileName -ResourceGroupName $rgName -TrafficRoutingMethod Priority -RelativeDnsName $dnsName -Ttl 30 -MonitorProtocol HTTP -MonitorPort 80 -MonitorPath "/"

# Create Endpoints for VZ and SM CDNs
Add-AzTrafficManagerEndpointConfig -EndpointName $endpointVzName -TrafficManagerProfile $tmProfile -Type ExternalEndpoints -Target $cdnVzUrl -Priority 1 -EndpointStatus Enabled -WhatIf
Add-AzTrafficManagerEndpointConfig -EndpointName $endpointSmName -TrafficManagerProfile $tmProfile -Type ExternalEndpoints -Target $cdnSmUrl -Priority 2 -EndpointStatus Enabled -WhatIf
Set-AzTrafficManagerProfile -TrafficManagerProfile $tmProfile -WhatIf

# Check Profile
Get-AzTrafficManagerProfile -Name $profileName

