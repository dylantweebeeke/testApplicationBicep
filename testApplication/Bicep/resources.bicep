param appName string = 'tp-arm-demo'
param environment string = 'dev'
param location string = resourceGroup().location

resource databaseServer 'Microsoft.Sql/servers@2022-02-01-preview' = {
  name: '${appName}-sqlserver-${environment}'
  location: location
  properties: {
    administratorLogin: 'db_username'
    administratorLoginPassword: '3I4Plu8lW60a'
  }
}

resource database 'Microsoft.Sql/servers/databases@2022-02-01-preview' = {
  name: '${databaseServer.name}/${appName}-sqldb-${environment}'
  location: location
  sku: {
    name: 'Basic'
    size: 'Basic'
    tier: 'Basic'
  }
}

resource storageAccount 'Microsoft.Storage/storageAccounts@2022-05-01' = {
  kind: 'StorageV2'
  location: location
  name: 'dtctdh199908' // Unique name
  sku: {
    name: 'Standard_LRS'
  }
}

resource blobContainer 'Microsoft.Storage/storageAccounts/blobServices/containers@2022-05-01' = {
  name: '${storageAccount.name}/default/blobcontainer'
}
