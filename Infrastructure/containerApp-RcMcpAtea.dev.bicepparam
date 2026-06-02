using 'containerApp.bicep'

param imageName = 'rcmcpatea'
param appName = 'rcmcpatea'
param acrName = 'mbolmcpacr'
param environmentName = 'mbolmcpdev'
param resourceGroupName = 'rg-mbolmcpdev'
param keyVaultSecrets = [
  {
    key: 'rcmcpateaclientid' // Must be lowercase - used in secretRef
    value: 'RcMcpAteaClientId' // PascalCase - actual Key Vault secret name
  }
  {
    key: 'rcmcpateaclientsecret' // Must be lowercase - used in secretRef
    value: 'RcMcpAteaClientSecret' // PascalCase - actual Key Vault secret name
  }
  {
    key: 'rcmcpateatenantid' // Must be lowercase - used in secretRef
    value: 'RcMcpAteaTenantId' // PascalCase - actual Key Vault secret name
  }
]
param environment = [
  {
    name: 'EntraIdAuth__TenantId'
    secretRef: 'rcmcpateatenantid'
  }
  {
    name: 'EntraIdAuth__ClientId'
    secretRef: 'rcmcpateaclientid'
  }
  {
    name: 'EntraIdAuth__ClientSecret'
    secretRef: 'rcmcpateaclientsecret'
  }
  {
    name: 'EntraIdAuth__PublicUrl'
    value: 'https://rcmcpatea.wonderfulsmoke-7219c7b7.westeurope.azurecontainerapps.io'
  }
  {
    name: 'RcMcpAteaApi__BaseUrl'
    value: 'https://restcountries.com/v3.1'
  }
  {
    name: 'IsTransportStateless'
    value: 'true'
  }
  // Application Insights connection string is automatically added by the template
]
