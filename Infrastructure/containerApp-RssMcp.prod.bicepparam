using 'containerApp.bicep'

param imageName = 'rssmcp'
param appName = 'rssmcp'
param acrName = 'mbolmcpacr'
param environmentName = 'mbolmcpprod'
param resourceGroupName = 'rg-mbolmcpprod'
param keyVaultSecrets = [
  {
    key: 'rssmcpclientid' // Must be lowercase - used in secretRef
    value: 'RssMcpClientId' // PascalCase - actual Key Vault secret name
  }
  {
    key: 'rssmcpclientsecret' // Must be lowercase - used in secretRef
    value: 'RssMcpClientSecret' // PascalCase - actual Key Vault secret name
  }
  {
    key: 'rssmcptenantid' // Must be lowercase - used in secretRef
    value: 'RssMcpTenantId' // PascalCase - actual Key Vault secret name
  }
]
param environment = [
  {
    name: 'EntraIdAuth__TenantId'
    secretRef: 'rssmcptenantid'
  }
  {
    name: 'EntraIdAuth__ClientId'
    secretRef: 'rssmcpclientid'
  }
  {
    name: 'EntraIdAuth__ClientSecret'
    secretRef: 'rssmcpclientsecret'
  }
  {
    name: 'EntraIdAuth__PublicUrl'
    value: 'TODO-update-after-first-deploy'
  }
  {
    name: 'RssMcpApi__BaseUrl'
    value: 'not-used'
  }
  {
    name: 'IsTransportStateless'
    value: 'true'
  }
  {
    name: 'Rss__HackerNewsUrl' 
    value: 'https://news.ycombinator.com/rss'
  }
  {
    name: 'Rss__BbcUrl' 
    value: 'https://feeds.bbci.co.uk/news/world/rss.xml'
  }
  {
    name: 'Rss__GithubReleasesUrl' 
    value: 'https://github.com/mortencivitarese/mcp-server-concept/releases.atom'
  }
  // Application Insights connection string is automatically added by the template
]
