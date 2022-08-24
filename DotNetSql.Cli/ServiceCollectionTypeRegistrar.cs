using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace DotNetSql.Cli;

public class ServiceCollectionTypeRegistrar : ITypeRegistrar
{
    private readonly IServiceCollection builder;

    public ServiceCollectionTypeRegistrar(IServiceCollection builder)
    {
        this.builder = builder;
    }

    public ITypeResolver Build()
    {
        return new TypeResolver(builder.BuildServiceProvider());
    }

    public void Register(Type service, Type implementation)
    {
        builder.AddSingleton(service, implementation);
    }

    public void RegisterInstance(Type service, object implementation)
    {
        builder.AddSingleton(service, implementation);
    }

    public void RegisterLazy(Type service, Func<object> func)
    {
        if (func is null)
        {
            throw new ArgumentNullException(nameof(func));
        }

        builder.AddSingleton(service, (provider) => func());
    }
}