$rgName = "WCN-WCM-CDN-CN-DEV"
$cdnProfileName = "WCM-WS-CDN-CHINA-DEPLOYTEST"
$location = "China East"
$sku = "Standard_ChinaCdn"

# CDN Endpoint variables
$cdnEndPointName = "cdn-dev-cn-endpoint-com"
$originName = "wcm-cdn-cn-dev"
$originHostName = "wcm-cdn-cn-dev.azureedge.net"

# Custom domain variables
$customDomainHostName = "cdn-dev.wacom.com"
$customDomainName = "contoso"

$contentTypesToCompress = @("application/eot","application/font","application/font-sfnt",
"application/javascript","application/json","application/opentype","application/otf",
"application/pkcs7-mime","application/truetype","application/ttf","application/vnd.ms-fontobject",
"application/xhtml+xml","application/xml","application/xml+rss","application/x-font-opentype",
"application/x-font-truetype","application/x-font-ttf","application/x-httpd-cgi",
"application/x-javascript","application/x-mpegurl","application/x-opentype","application/x-otf",
"application/x-perl","application/x-ttf","font/eot","font/ttf","font/otf","font/opentype",
"image/svg+xml","text/css","text/csv","text/html","text/javascript","text/js","text/plain",
"text/richtext","text/tab-separated-values","text/xml","text/x-script","text/x-component",
"text/x-java-source")

# Resource Group Name
$rg = (Get-AzResourceGroup -Name $rgName)

# Create Resource Group if not exist
if($null -eq $rg) {
    $rg = New-AzResourceGroup -Name $rgName -Location $location
}

# Check for CDN profile or create if Not Exist
$cdnProfile = (Get-AzCdnProfile -ProfileName $cdnProfileName -ResourceGroupName $rgName)
if($null -eq $cdnProfile) {
    # Create CDN-Profile
    $cdnProfile = New-AzCdnProfile `
        -ProfileName $cdnProfileName `
        -Location $location `
        -Sku $sku `
        -ResourceGroupName $rg.ResourceGroupName `
}

# Create CDN Endpoint
$cdnEndpoint = New-AzCdnEndpoint `
    -EndpointName $cdnEndPointName `
    -ProfileName $cdnProfile.Name `
    -ResourceGroupName $rg.ResourceGroupName `
    -Location $location `
    -OriginName $originName `
    -OriginHostName $originHostName `
    -ContentTypesToCompress $contentTypesToCompress `
    -OriginHostHeader $originHostName
    
# ================================================
# Add a Custom Custom domain Steps
# ================================================
# Get an existing endpoint
$endpoint = Get-AzCdnEndpoint `
    -ProfileName $cdnProfile.Name `
    -ResourceGroupName $rg.ResourceGroupName `
    -EndpointName $cdnEndpoint.Name

# Check the custom domain mapping
$result = Test-AzCdnCustomDomain `
    -CdnEndpoint $endpoint `
    -CustomDomainHostName $customDomainHostName

# Create the custom domain on the endpoint
If($result.CustomDomainValidated) 
{ 
    New-AzCdnCustomDomain `
        -CustomDomainName $customDomainName `
        -HostName $customDomainHostName `
        -CdnEndpoint $endpoint 
}
# ==================================================
# ==================================================