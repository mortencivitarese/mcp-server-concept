using 'main.bicep'

// Shared registry — keep acrName and acrResourceGroupName identical to dev.bicepparam
param acrName             = 'mbolmcpacr'
param acrResourceGroupName = 'rg-mbolmcpacr'

// Prod-environment resources
param containerAppsEnvName = 'mbolmcpprod'
param keyVaultName        = 'mbolmcpprod'
param logAnalyticsName    = 'mbolmcpprod'
param location            = 'westeurope'
param resourceGroupName   = 'rg-mbolmcpprod'
param storageAccountName  = 'stmbolmcpprod'
