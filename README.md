# Omnium Integration SDK

[TODO]

## Getting Start

To add required services just call (assuming you are on top-level statements):

```csharp
builder.Services.AddOmniumIntegration();
```


## Configuration

By default SDK package reads data from `IConfiguration` sources.
Recommended source is `appsettings.json` file.

Following section is required in configuration file:

```json
{
  "Omnium": {
    "BaseAddress": "https://api.omnium.no",
    "ClientId": "...",
    "ClientSecret": "..."
  }
}
```

You are able to override settings in code as well:

```csharp
builder.Services.Configure<OmniumConfiguration>(o => o.ClientId = "OtherClientThanInCodeFile");
```

## How to build a new version?

Go to [Team City project](https://tc.geta.no/project/GetaPackages_IntegrationOmniumSDK?hideTestsFromDependencies=false&hideProblemsFromDependencies=false&branch=master&mode=builds#1119346) and choose branch to build from:

* `develop` - builds client against Omnium test environment ([apitest.omnium.no](apitest.omnium.no)).
* `master` - builds client against Omnium production environment ([api.omnium.no](api.omnium.no)).
