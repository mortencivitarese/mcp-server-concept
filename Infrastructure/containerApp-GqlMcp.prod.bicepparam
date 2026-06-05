using 'containerApp.bicep'

param imageName = 'gqlmcp'
param appName = 'gqlmcp'
param acrName = 'mbolmcpacr'
param environmentName = 'mbolmcpprod'
param resourceGroupName = 'rg-mbolmcpprod'
param keyVaultSecrets = [
  {
    key: 'gqlmcpclientid' // Must be lowercase - used in secretRef
    value: 'GqlMcpClientId' // PascalCase - actual Key Vault secret name
  }
  {
    key: 'gqlmcpclientsecret' // Must be lowercase - used in secretRef
    value: 'GqlMcpClientSecret' // PascalCase - actual Key Vault secret name
  }
  {
    key: 'gqlmcptenantid' // Must be lowercase - used in secretRef
    value: 'GqlMcpTenantId' // PascalCase - actual Key Vault secret name
  }
]
param environment = [
  {
    name: 'EntraIdAuth__TenantId'
    secretRef: 'gqlmcptenantid'
  }
  {
    name: 'EntraIdAuth__ClientId'
    secretRef: 'gqlmcpclientid'
  }
  {
    name: 'EntraIdAuth__ClientSecret'
    secretRef: 'gqlmcpclientsecret'
  }
  {
    name: 'EntraIdAuth__PublicUrl'
    value: 'TODO-update-after-first-deploy'
  }
  {
    name: 'GqlMcpApi__BaseUrl'
    value: 'not-used'
  }
  {
    name: 'IsTransportStateless'
    value: 'true'
  }
  {
    name: 'GraphQL__CountriesUrl' 
    value: 'https://countries.trevorblades.com/'
  }
  {
    name: 'GraphQL__RickMortyUrl' 
    value: 'https://rickandmortyapi.com/graphql'
  }
  {
    name: 'GraphQL__StarWarsUrl' 
    value: 'https://swapi-graphql.netlify.app/.netlify/functions/index'
  }
  // Application Insights connection string is automatically added by the template
]
