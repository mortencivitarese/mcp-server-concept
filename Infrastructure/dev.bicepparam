using 'main.bicep'

// Shared registry — keep acrName and acrResourceGroupName identical in prod.bicepparam
param acrName             = 'mbolmcpacr'
param acrResourceGroupName = 'rg-mbolmcpacr'

// Dev-environment resources
param containerAppsEnvName = 'mbolmcpdev'
param keyVaultName        = 'mbolmcpdev'
param logAnalyticsName    = 'mbolmcpdev'
param location            = 'westeurope'
param resourceGroupName   = 'rg-mbolmcpdev'
param storageAccountName  = 'stmbolmcpdev'
