using 'containerApp.bicep'

param imageName = 'ateastarter'
param appName = 'ateastarter'
param acrName = 'mbolmcpacr'
param environmentName = 'mbolmcpdev'
param resourceGroupName = 'rg-mbolmcpdev'
param keyVaultSecrets = [
  {
    key: 'ateastarterclientid' // Must be lowercase - used in secretRef
    value: 'AteaStarterClientId' // PascalCase - actual Key Vault secret name
  }
  {
    key: 'ateastarterclientsecret' // Must be lowercase - used in secretRef
    value: 'AteaStarterClientSecret' // PascalCase - actual Key Vault secret name
  }
  {
    key: 'ateastartertenantid' // Must be lowercase - used in secretRef
    value: 'AteaStarterTenantId' // PascalCase - actual Key Vault secret name
  }
]
param environment = [
  {
    name: 'EntraIdAuth__TenantId'
    secretRef: 'ateastartertenantid'
  }
  {
    name: 'EntraIdAuth__ClientId'
    secretRef: 'ateastarterclientid'
  }
  {
    name: 'EntraIdAuth__ClientSecret'
    secretRef: 'ateastarterclientsecret'
  }
  {
    name: 'EntraIdAuth__PublicUrl'
    value: 'https://ateastarter.wonderfulsmoke-7219c7b7.westeurope.azurecontainerapps.io'
  }
  {
    name: 'AteaStarterApi__BaseUrl'
    value: 'https://restcountries.com/v3.1'
  }
  {
    name: 'IsTransportStateless'
    value: 'true'
  }
  // Application Insights connection string is automatically added by the template
]
