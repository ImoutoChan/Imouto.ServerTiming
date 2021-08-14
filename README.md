# Imouto.ServerTiming

Based on Lib.AspNetCore.ServerTiming, refactored and adapted.

## Getting Started

Available on [NuGet](https://www.nuget.org/packages/Imouto.ServerTiming/).

```
PM>  Install-Package Imouto.ServerTiming
```
or
```
<PackageReference Include="Imouto.ServerTiming" Version="1.1.0" />
```

## Usage

### Basic Usage

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddServerTiming();
}

...

public void Configure(IApplicationBuilder app)
{
    app.UseServerTiming();
    ...
}
```

After you call these two methods a basic metric "total" will be added to all of your responses. You can turn it off using:

```c#

    services.AddServerTiming(o => o.IncludeTotalMetric = false);

```

### Advanced Usage

You can inject IServerTiming anywhere and use it like this:
```c#
private readonly IServerTiming _serverTiming;

public void Do()
{
    using var measurement = _serverTiming.Measure("metric-name");
    
    ...
}
```

Creating a metric without `using` and calling Dispose by yourself allows you to control when to end the metric.

```c#
private readonly IServerTiming _serverTiming;

public void Do()
{
    var measurement = _serverTiming.Measure("metric-name");
    
    ...
    
    measurement.Dispose();
}
```

### Metrics with the same name
It's also safe to create metrics with the same name. They will be numbered automatically. So it's safe to use this approach, for example, in your Mediatr behaviors or similar middlewares that can be created multiple times per request.

### Scoped Context
Make sure to use this approach only from code with the correct usage of scopes. Every unit of work in your application should create a **scope** from the `IScopeProvider`. In ASP NET Core scope is already created for you. But make sure to create it for rabbit clients or scheduled work from hosted services.
