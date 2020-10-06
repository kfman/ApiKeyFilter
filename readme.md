# ApiKey Filter

The package adds an ApiKey filter to your Asp.Net Core Application.

The API Keys are organized in an SQLite database. The Roles can be defined as string.

## Installation

Make sure that your __ConfigureServices__ contains the following lines.
```c#
public void ConfigureServices(IServiceCollection services) {
    services.AddControllers(
        e => e.Filters.Add<ApiFilter>()
    );
    services.AddApiKeyController();
}
```

## Usage

To apply a filter to a controller just add the filter attribute to your controller
```c#
    [ApiKeyFilter.LevelFilter("weather")]
    public class WeatherForecastController : ControllerBase {
```