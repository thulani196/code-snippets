using FlynnDW;
using FlynnDW.Logic.Helpers;
using FlynnDW.Logic.Interfaces;
using FlynnDW.Logic.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(FlynnDW.Startup))]
namespace FlynnDW
{

    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IFileParserService,FileParserService>();
            builder.Services.AddTransient<ITbeParser,TbeParser>();
        }
    }
}
