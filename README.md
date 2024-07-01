# Setup

To run this funtion, you will need a `.\src\Defra.Trade.Events.SUS.RemosSignUpSubscriber\local.settings.json` file. The file will need the following settings:

```jsonc 
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "ServiceBus:ConnectionString": "<secret>", // Value can be obtained here: https://portal.azure.com/#@DefraCloudDev.onmicrosoft.com/resource/subscriptions/d6f720f6-0e75-44c9-a406-2840c33ec61e/resourceGroups/DEVTREINFRG1001/providers/Microsoft.ServiceBus/namespaces/DEVTREINFSB1001/saskey
    "ConfigurationServer:ConnectionString": "<secret>", // Value can be obtained here: https://portal.azure.com/#@DefraCloudDev.onmicrosoft.com/resource/subscriptions/d6f720f6-0e75-44c9-a406-2840c33ec61e/resourceGroups/DEVTRDINFRG1001/providers/Microsoft.AppConfiguration/configurationStores/DEVTRDINFAC1001/keys
    "ConfigurationServer:TenantId": "<secret>" // Value can be obtained here: https://portal.azure.com/#settings/directory - The DefraCloudDev Directory ID
  }
}
```

Putting messages onto the [defra.trade.sus.remos.signup](https://portal.azure.com/#@DefraCloudDev.onmicrosoft.com/resource/subscriptions/d6f720f6-0e75-44c9-a406-2840c33ec61e/resourceGroups/DEVTREINFRG1001/providers/Microsoft.ServiceBus/namespaces/DEVTREINFSB1001/queues/defra.trade.sus.remos.signup/overview) queue will then be picked up and processed by this function app.

You may want to pause the 