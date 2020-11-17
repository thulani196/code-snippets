# File: cdn-sm.ps1
# Date: 2020-08-04
# Author: Joe Agster, Redapt [jagster@redapt.com]
# Description: Deploys the secondary (failover) CDN profile and endpoint using the Standard Microsoft enginge

# Global variables
$location = "West US"
$rgName = "WUS-WCM-CDN-ROW"
$cdnDomain = "cdn-dev.wacom.com"

# CDN
$cdnProfileName = "WUS-WCM-CDN-SM-DEV"
$cdnEndpointName = "wcm-cdn-sm-dev"
$cdnEndpointHostName = $cdnEndpointName + ".azureedge.net"
$cdnProfileSku = "Standard_Microsoft" # Note: for China, this will be Standard_ChinaCdn

# Storage
$storageName = "wuswcmstoragedev"
# $storageEndpoint = $storageName + ".blob.core.windows.net"
$storageEndpoint = $storageName + "-secondary.blob.core.windows.net"
$originName = $storageName + "-blob-core-windows-net"


# Set-AzContext -Subscription 'WCM-WS-CDN-ROW'

# Misc
$contentTypes = @(
	"application/eot",
	"application/font",
	"application/font-sfnt",
	"application/javascript",
	"application/json",
	"application/opentype",
	"application/otf",
	"application/pkcs7-mime",
	"application/truetype",
	"application/ttf",
	"application/vnd.ms-fontobject",
	"application/xhtml+xml",
	"application/xml",
	"application/xml+rss",
	"application/x-font-opentype",
	"application/x-font-truetype",
	"application/x-font-ttf",
	"application/x-httpd-cgi",
	"application/x-javascript",
	"application/x-mpegurl",
	"application/x-opentype",
	"application/x-otf",
	"application/x-perl",
	"application/x-ttf",
	"font/eot",
	"font/ttf",
	"font/otf",
	"font/opentype",
	"image/svg+xml",
	"text/css",
	"text/csv",
	"text/html",
	"text/javascript",
	"text/js",
	"text/plain",
	"text/richtext",
	"text/tab-separated-values",
	"text/xml",
	"text/x-script",
	"text/x-component",
	"text/x-java-source"
)


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
# CDN
#

# create the CDN profile
# https://docs.microsoft.com/en-us/powershell/module/az.cdn/New-AzCdnProfile
$cdnProfile = (Get-AzCdnProfile -ProfileName $cdnProfileName -ResourceGroupName $rgName)
if ($null -eq $cdnProfile) {
  $cdnProfile = New-AzCdnProfile -ProfileName $cdnProfileName -Location $location -ResourceGroupName $rgName -Sku $cdnProfileSku
}


# CDN Rules
# URL with path /s/ rewrites to /secure/ container
# all other URLS rewrites to /standard/ container
$secureCondition = New-AzCdnDeliveryRuleCondition -MatchVariable UrlPath -Operator "BeginsWith" -MatchValue "/s/" -Transform "Lowercase"
$secureCacheAction = New-AzCdnDeliveryRuleAction -CacheBehavior "BypassCache"
$secureRewriteAction = New-AzCdnDeliveryRuleAction -SourcePattern "/" -Destination "/secure/" -PreservePath
$secureRule = New-AzCdnDeliveryRule -Name "Secure" -Order 1 -Condition @($secureCondition) -Action @($secureCacheAction, $secureRewriteAction)

$standardCondition = New-AzCdnDeliveryRuleCondition -MatchVariable UrlPath -Operator "BeginsWith" -MatchValue "/s/" -Transform "Lowercase" -NegateCondition
$standardCacheAction = New-AzCdnDeliveryRuleAction -QueryStringBehavior "ExcludeAll"
$standardRewriteAction = New-AzCdnDeliveryRuleAction -SourcePattern "/" -Destination "/standard/" -PreservePath
$standardRule = New-AzCdnDeliveryRule -Name "Standard" -Order 2 -Condition @($standardCondition) -Action @($standardCacheAction, $standardRewriteAction)

$deliveryPolicy = New-AzCdnDeliveryPolicy -Description "CDNPolicy" -Rule @($secureRule, $standardRule)
   

# create the CDN endpoint
# https://docs.microsoft.com/en-us/powershell/module/az.cdn/New-AzCdnProfile
$endPoint = (Get-AzCdnEndpoint -EndpointName $cdnEndpointName -CdnProfile $cdnProfile)
if ($null -eq $endPoint) {
  # create the CDN endpoint
  $endPoint = New-AzCdnEndpoint `
    -EndpointName $cdnEndpointName `
    -ProfileName $cdnProfileName `
    -ResourceGroupName $rgName `
    -Location $location `
    -OriginName ($originName) `
    -OriginHostName $storageEndpoint `
    -OriginHostHeader $storageEndpoint `
    -ContentTypesToCompress $contentTypes `
    -IsCompressionEnabled $true `
    -IsHttpAllowed $true `
    -IsHttpsAllowed $true `
    -QueryStringCachingBehavior BypassCaching `
    -HttpPort 80 `
    -HttpsPort 443 `
    -OptimizationType "GeneralWebDelivery"
}
   
# add the routing/caching rules to the endpoint
$endPoint.DeliveryPolicy = $deliveryPolicy
Set-AzCdnEndpoint -CdnEndpoint $endPoint

# add the custom domain to the endpoint
$customDomain = (Get-AzCdnCustomDomain -CustomDomainName $cdnDomain -CdnEndpoint $endPoint)
if ($null -eq $customDomain) {
$customDomain = New-AzCdnCustomDomain
   -HostName $cdnEndpointHostName
   -CustomDomainName $cdnDomain
   -CdnEndpoint $endPoint
#   -WhatIf
}


# -DeliveryPolicy $deliveryPolicy
#    -OriginHostHeader $storageFailEndpoint `
#$epx = Get-AzCdnEndpoint -EndpointName "WCMCDNSecureDev" -ProfileName "WUSWCMCDNDev" -ResourceGroupName "WUS-WCM-CDN-ROW"
#$ep1 = Get-AzCdnEndpoint -EndpointName $cdnEndpointName -ProfileName $cdnProfileName -ResourceGroupName $rgName
#$cdnProfile1 = (Get-AzCdnProfile -ProfileName "WUS-WCM-CDN-SM-DEV" -ResourceGroupName "WUS-WCM-CDN-ROW")
# Get-AzCdnEndpointResourceUsage -EndpointName $cdnEndpointName -ProfileName $cdnProfileName -ResourceGroupName $rgName
# New-AzCdnCustomDomain -HostName "wcm-cdn-sm-dev.azureedge.net" -CustomDomainName "cdn-dev.wacom.com" -EndpointName "wcm-cdn-sm-dev" -ProfileName "WUS-WCM-CDN-SM-DEV" -ResourceGroupName "WUS-WCM-CDN-ROW"
