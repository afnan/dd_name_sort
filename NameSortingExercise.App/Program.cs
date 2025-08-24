using Microsoft.Extensions.DependencyInjection;
using NameSortingExercise.App;
using NameSortingExercise.Core.Domain;
using NameSortingExercise.Core.Parsing;
using NameSortingExercise.Core.Sorting;
using NameSortingExercise.Infrastructure;


var services = new ServiceCollection()
    // application
    .AddSingleton<AppBootstrap>()
    // I/O port/adapter
    .AddSingleton<INameRepository, FileNameRepository>()
    // domain services from Core
    .AddSingleton<INameParser, NameParser>()
    .AddSingleton<System.Collections.Generic.IComparer<Person>, PersonNameComparer>()
    .BuildServiceProvider();

var app = services.GetRequiredService<AppBootstrap>();
return await app.RunAsync(args);