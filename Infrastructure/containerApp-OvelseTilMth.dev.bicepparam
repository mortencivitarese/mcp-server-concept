using 'containerApp.bicep'

param imageName = 'ovelsetilmth'
param appName = 'ovelsetilmth'
param acrName = 'mbolmcpacr'
param environmentName = 'mbolmcpdev'
param resourceGroupName = 'rg-mbolmcpdev'
param keyVaultSecrets = [
  {
    key: 'ovelsetilmthclientid' // Must be lowercase - used in secretRef
    value: 'OvelseTilMthClientId' // PascalCase - actual Key Vault secret name
  }
  {
    key: 'ovelsetilmthclientsecret' // Must be lowercase - used in secretRef
    value: 'OvelseTilMthClientSecret' // PascalCase - actual Key Vault secret name
  }
  {
    key: 'ovelsetilmthtenantid' // Must be lowercase - used in secretRef
    value: 'OvelseTilMthTenantId' // PascalCase - actual Key Vault secret name
  }
]
param environment = [
  {
    name: 'EntraIdAuth__TenantId'
    secretRef: 'ovelsetilmthtenantid'
  }
  {
    name: 'EntraIdAuth__ClientId'
    secretRef: 'ovelsetilmthclientid'
  }
  {
    name: 'EntraIdAuth__ClientSecret'
    secretRef: 'ovelsetilmthclientsecret'
  }
  {
    name: 'EntraIdAuth__PublicUrl'
    value: 'TODO-public-url-after-first-deploy'
  }
  {
    name: 'DownstreamApi__Scope'
    value: 'https://api.fabric.microsoft.com/.default'
  }
  {
    name: 'IsTransportStateless'
    value: 'true'
  }
  // Application Insights connection string is automatically added by the template
]
