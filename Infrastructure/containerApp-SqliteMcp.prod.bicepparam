using 'containerApp.bicep'

param imageName = 'sqlitemcp'
param appName = 'sqlitemcp'
param acrName = 'mbolmcpacr'
param environmentName = 'mbolmcpprod'
param resourceGroupName = 'rg-mbolmcpprod'
param keyVaultSecrets = [
  {
    key: 'sqlitemcpclientid' // Must be lowercase - used in secretRef
    value: 'SqliteMcpClientId' // PascalCase - actual Key Vault secret name
  }
  {
    key: 'sqlitemcpclientsecret' // Must be lowercase - used in secretRef
    value: 'SqliteMcpClientSecret' // PascalCase - actual Key Vault secret name
  }
  {
    key: 'sqlitemcptenantid' // Must be lowercase - used in secretRef
    value: 'SqliteMcpTenantId' // PascalCase - actual Key Vault secret name
  }
]
param environment = [
  {
    name: 'EntraIdAuth__TenantId'
    secretRef: 'sqlitemcptenantid'
  }
  {
    name: 'EntraIdAuth__ClientId'
    secretRef: 'sqlitemcpclientid'
  }
  {
    name: 'EntraIdAuth__ClientSecret'
    secretRef: 'sqlitemcpclientsecret'
  }
  {
    name: 'EntraIdAuth__PublicUrl'
    value: 'TODO-update-after-first-deploy'
  }
  {
    name: 'SqliteMcpApi__BaseUrl'
    value: 'not-used'
  }
  {
    name: 'IsTransportStateless'
    value: 'true'
  }
  {
    name: 'Sqlite__DbPath' 
    value: '/app/Data/sample.db'
  }
  // Application Insights connection string is automatically added by the template
]
