using 'containerApp.bicep'

param imageName = 'cryptomcp'
param appName = 'cryptomcp'
param acrName = 'mbolmcpacr'
param environmentName = 'mbolmcpprod'
param resourceGroupName = 'rg-mbolmcpprod'
param keyVaultSecrets = [
  {
    key: 'cryptomcpclientid' // Must be lowercase - used in secretRef
    value: 'CryptoMcpClientId' // PascalCase - actual Key Vault secret name
  }
  {
    key: 'cryptomcpclientsecret' // Must be lowercase - used in secretRef
    value: 'CryptoMcpClientSecret' // PascalCase - actual Key Vault secret name
  }
  {
    key: 'cryptomcptenantid' // Must be lowercase - used in secretRef
    value: 'CryptoMcpTenantId' // PascalCase - actual Key Vault secret name
  }
]
param environment = [
  {
    name: 'EntraIdAuth__TenantId'
    secretRef: 'cryptomcptenantid'
  }
  {
    name: 'EntraIdAuth__ClientId'
    secretRef: 'cryptomcpclientid'
  }
  {
    name: 'EntraIdAuth__ClientSecret'
    secretRef: 'cryptomcpclientsecret'
  }
  {
    name: 'EntraIdAuth__PublicUrl'
    value: 'TODO-update-after-first-deploy'
  }
  {
    name: 'CryptoMcpApi__BaseUrl'
    value: 'not-used'
  }
  {
    name: 'IsTransportStateless'
    value: 'true'
  }
  {
    name: 'Crypto__BinanceUrl' 
    value: 'https://api.binance.com'
  }
  {
    name: 'Crypto__CoingeckoUrl' 
    value: 'https://api.coingecko.com'
  }
  // Application Insights connection string is automatically added by the template
]
