$location = "West US"
$locationFail = "East US"
$rgName = "WUS-WCM-CDN-ROW-PROD"
$storageName = "wuswcmstorageprod"

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
# Storage Account
#

# Create Storage Account
$storAcct = New-AzStorageAccount
   -ResourceGroupName $rgName
   -Name $storageName
   -SkuName "Standard_RAGRS"
   -Location $location
   -Kind "StorageV2"
   -AccessTier "Hot"
   -EnableHttpsTrafficOnly True
   
$storContext = $storAcct.Context   

# Create Container - standard   
$containerStandard = New-AzStorageContainer
   -Name "standard"
   -Permission "Blob"
   -Context $storContext

# Create Container - secure   
$containerSecure = New-AzStorageContainer
   -Name "secure"
   -Permission "None"
   -Context $storContext
   
# Create stored policy for secure container
$securePolicy = New-AzStorageContainerStoredAccessPolicy `
   -Container "secure" `
   -Policy "securepolicy2" `
   -Permission "r" `
   -StartTime (Get-Date).Date `
   -Context $storContext

# Update Metadata
$containerSecure.CloudBlobContainer.Metadata["DefaultExpirationSecs"] = "3600"
$containerSecure.CloudBlobContainer.SetMetadata()


# Testing Only
#Get-AzStorageContainerStoredAccessPolicy -Container "secure" -Policy "securepolicy" -Context $storAcct.Context 
#$storAcct = Get-AzStorageAccount -Name "wuswcmstoragedev" -ResourceGroupName "WUS-WCM-CDN-ROW"
#$storContext = $storAcct.Context  
#$containerSecure = Get-AzStorageContainer -Name "secure" -Context $storContext