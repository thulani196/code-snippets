$resourceGroup = "WCN-WCM-CDN-CN-DEV"
$profileName = "WCN-WS-TM-CHINA-DEV"
$endpointName = "WCN-WS-TM-CHINA-Endpoint"
$cdnUrl = "cdn-dev.wacom.com.mcchcdn.com"

# Set-AzContext -Subscription 'WCM-WS-CDN-CHINA'

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
# Priority profile
#

$dnsName = $profileName.ToLower()

# Create Profile
$tmProfile = New-AzTrafficManagerProfile -Name $profileName -ResourceGroupName $resourceGroup -TrafficRoutingMethod Priority -RelativeDnsName $dnsName -Ttl 30 -MonitorProtocol HTTP -MonitorPort 80 -MonitorPath "/"

# Create Endpoint
Add-AzTrafficManagerEndpointConfig -EndpointName $endpointName -TrafficManagerProfile $tmProfile -Type ExternalEndpoints -Target $cdnUrl -Priority 1 -EndpointStatus Enabled
Set-AzTrafficManagerProfile -TrafficManagerProfile $tmProfile

# Check Profile
Get-AzTrafficManagerProfile -Name $profileName

