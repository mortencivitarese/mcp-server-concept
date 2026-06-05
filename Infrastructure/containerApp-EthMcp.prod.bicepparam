using 'containerApp.bicep'

param imageName = 'ethmcp'
param appName = 'ethmcp'
param acrName = 'mbolmcpacr'
param environmentName = 'mbolmcpprod'
param resourceGroupName = 'rg-mbolmcpprod'
param keyVaultSecrets = [
  {
    key: 'ethmcpclientid' // Must be lowercase - used in secretRef
    value: 'EthMcpClientId' // PascalCase - actual Key Vault secret name
  }
  {
    key: 'ethmcpclientsecret' // Must be lowercase - used in secretRef
    value: 'EthMcpClientSecret' // PascalCase - actual Key Vault secret name
  }
  {
    key: 'ethmcptenantid' // Must be lowercase - used in secretRef
    value: 'EthMcpTenantId' // PascalCase - actual Key Vault secret name
  }
]
param environment = [
  {
    name: 'EntraIdAuth__TenantId'
    secretRef: 'ethmcptenantid'
  }
  {
    name: 'EntraIdAuth__ClientId'
    secretRef: 'ethmcpclientid'
  }
  {
    name: 'EntraIdAuth__ClientSecret'
    secretRef: 'ethmcpclientsecret'
  }
  {
    name: 'EntraIdAuth__PublicUrl'
    value: 'TODO-update-after-first-deploy'
  }
  {
    name: 'EthMcpApi__BaseUrl'
    value: 'not-used'
  }
  {
    name: 'IsTransportStateless'
    value: 'true'
  }
  {
    name: 'Eth__RpcUrl' 
    value: 'https://cloudflare-eth.com'
  }
  // Application Insights connection string is automatically added by the template
]
