# File: cdn-vz-dev.ps1
# Date: 2020-08-18
# Author: Thulani, Redapt [jagster@redapt.com]
# Description: Deploys the primary CDN profile and endpoint using the Verizon Premium engine

# Global variables
$location = "Global"
$rgName = "WUS-WCM-CDN-ROW-PROD"
$cdnDomain = "cdn-dev.wacom.com"

# CDN
$cdnProfileName = "WUS-WCM-CDN-VZ-PROD"
$cdnEndpointName = "wcm-cdn-vz-prod"
$cdnEndpointHostName = $cdnEndpointName + ".azureedge.net"
$cdnProfileSku = "Premium_Verizon" # Note: for China, this will be Standard_ChinaCdn

# Storage
$storageName = "wuswcmstorageprod"
$storageEndpoint = $storageName + ".blob.core.windows.net"
$originName = $storageName + "-blob-core-windows-net"


# TODO:
# Go to WUS-WCM-CDN-VZ-DEV resource in Azure
# Go to Export Template
# script here

# Get resource group
$rg = (Get-AzResourceGroup -Name $rgName)

# Create Resource Group if Not exist
if ($null -eq $rg) {
    $rg = New-AzResourceGroup -Name $rgName -Location $location -WhatIf
}

# Create the CDN Profile
$cdnProfile = (Get-AzCdnProfile -ProfileName $cdnProfileName -ResourceGroupName $rgName)
if ($null -eq $cdnProfile) {
    $cdnProfile = New-AzCdnProfile -ProfileName $cdnProfileName -Location $location -ResourceGroupName -$rgName -Sku $cdnProfileSku -WhatIf
}

# Create Profile Endpoint
$endPoint = (Get-AzCdnEndpoint -EndpointName $cdnEndpointName -CdnProfile $cdnProfile)
if($null -eq $endPoint) {
    # Create endpoint if doesn't exist
    $endPoint = New-AzCdnEndpoint `
        -EndpointName $cdnEndpointName `
        -ProfileName $cdnProfileName `
        -ResourceGroupName $rgName `
        -Location $location `
        -OriginName ($originName) `
        -OriginHostName $storageEndpoint `
        -OriginHostHeader $storageEndpoint `
        -IsCompressionEnabled $false `
        -IsHttpAllowed $true `
        -IsHttpsAllowed $true `
        -QueryStringCachingBehavior NotSet `
        -HttpPort 80 `
        -HttpsPort 443 `
        -OptimizationType "GeneralWebDelivery" `
        -WhatIf
}

# add the routing/caching rules to the endpoint
$endPoint.DeliveryPolicy = $deliveryPolicy
Set-AzCdnEndpoint -CdnEndpoint $endPoint -WhatIf

# Add Custom Domain to the Endpoint
$customDomain = (Get-AzCdnCustomDomain -CustomDomainName $cdnDomain -CdnEndpoint $endPoint)
if ($null -eq $customDomain) {
    $customDomain = New-AzCdnCustomDomain `
        -HostName $cdnEndpointHostName `
        -CustomDomainName $cdnDomain `
        -CdnEndpoint $endPoint `
        -WhatIf
}