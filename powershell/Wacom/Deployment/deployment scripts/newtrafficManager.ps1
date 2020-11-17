$rgName = "WUS-WCM-CDN-ROW"
$resourceName = "WUS-WCM-TrafficManager-TT1Test"
$TmDNSName = "wus-wcm-trafficmanager-global.trafficmanager.net"
$TmEndpointName = "WUS-WCM-TrafficManager-ROW-Endpoint"

# Set-AzContext -Subscription 'WCM-WS-CDN-ROW'

#
# Resource Group
#

# get resource group
$rg = (Get-AzResourceGroup -Name $rgName)

# create resource group if not exists
if ($null -eq $rg) {
  $rg = New-AzResourceGroup -Name $rgName -Location $location
}

#
# Traffic Manager
#

# Global Profile
# Geographic profile

# Thulani TODO
# based on WUS-WCM-TrafficManager-Global

#Create Network Traffic manager profile
$TmProfile = New-AzTrafficManagerProfile `
  -Name $resourceName `
  -ResourceGroupName $rgName `
  -TrafficRoutingMethod "Geographic" `
  -RelativeDnsName $TmDNSName `
  -Ttl 30 `
  -MonitorProtocol "HTTP" `
  -MonitorPort 80 `
  -MonitorPath "/"

#Create Endpoint for Manager Profile
$TmEndPointResource = Add-AzTrafficManagerEndpointConfig `
  -EndpointName $TmEndpointName `
  -Type "NestedEndpoints" `
  -TargetResourceId $TmProfile.Id `
  -Target $TmDNSName `
  -EndpointLocation "West US" `
  -EndpointStatus Enabled `
  -MinChildEndpoints 1 `
  -TrafficManagerProfile $TmProfile`