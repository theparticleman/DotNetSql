using DotNetSql.Cli;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

var serviceCollection = new ServiceCollection();
serviceCollection.AddTransient<ISqlServerAdapter, SqlServerAdapter>();
serviceCollection.AddTransient<IFileSystem, FileSystem>();

var registrar = new ServiceCollectionTypeRegistrar(serviceCollection);

var app = new CommandApp<ExecuteSqlCommand>(registrar);
return app.Run(args);