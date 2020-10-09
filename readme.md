# ApiKey Filter

The package adds an ApiKey filter to your Asp.Net Core Application.

The API Keys are organized in an SQLite database. The Roles can be defined as string.

## Installation

Make sure that your __ConfigureServices__ contains the following lines. You have to specify a master key
```c#
public void ConfigureServices(IServiceCollection services) {
    services.AddControllers(
        e => e.Filters.Add<ApiFilter>()
    );
    services.AddApiKeyController("00000000-0000-0000-0000-000000000000");
}
```

## Usage

To apply a filter to a controller just add the filter attribute to your controller
```c#
    [ApiKeyFilter.LevelFilter("weather")]
    public class WeatherForecastController : ControllerBase {
```

## Access control
To add a new API key call the url
```
POST http://{baseUrlOfHostProject}/api/accessControl/ApiKey

{
	"description": "New Api Key"
}
```
as a response you get the new API Key
```json
{
  "description": "New Api Key",
  "roles": null,
  "id": "a86b941b-0000-0000-0000-d1c7f68b5022",
  "created": "2020-10-09T05:24:54.1935783+00:00",
  "deleted": null
}
```

To add an API key to a specific role call
```
POST http://{baseUrlOfHostProject}/api/accessControl/ApiKey/a86b941b-0000-0000-0000-d1c7f68b5022/role/<NameOfRole>
```