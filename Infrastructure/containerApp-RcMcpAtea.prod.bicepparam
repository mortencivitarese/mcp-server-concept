using 'containerApp.bicep'

param imageName = 'rcmcpatea'
param appName = 'rcmcpatea'
param acrName = 'mbolmcpacr'
param environmentName = 'mbolmcpprod'
param resourceGroupName = 'rg-mbolmcpprod'
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
    value: 'https://rcmcpatea.kindhill-77d03965.westeurope.azurecontainerapps.io'
  }
  {
    name: 'RcMcpAteaApi__BaseUrl'
    value: 'https://restcountries.com/v3.1'
  }
  {
    name: 'WeatherApi__BaseUrl'
    value: 'https://api.open-meteo.com/v1'
  }
  {
    name: 'WeatherApi__GeocodingUrl'
    value: 'https://geocoding-api.open-meteo.com/v1'
  }
  {
    name: 'ExchangeRateApi__BaseUrl'
    value: 'https://open.er-api.com/v6'
  }
  {
    name: 'PokemonApi__BaseUrl'
    value: 'https://pokeapi.co/api/v2'
  }
  {
    name: 'BookApi__BaseUrl'
    value: 'https://openlibrary.org'
  }
  {
    name: 'IpGeoApi__BaseUrl'
    value: 'https://ipwho.is'
  }
  {
    name: 'IsTransportStateless'
    value: 'true'
  }
  // Application Insights connection string is automatically added by the template
]
