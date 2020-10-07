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