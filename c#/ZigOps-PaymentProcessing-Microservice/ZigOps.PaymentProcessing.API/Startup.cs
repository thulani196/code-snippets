//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TheZig.Keyvault.Library;
using ZigOps.PaymentProcessing.API;
using ZigOps.PaymentProcessing.Data.Builders;
using ZigOps.PaymentProcessing.Data.Helpers;
using ZigOps.PaymentProcessing.Data.Interface;
using ZigOps.PaymentProcessing.Data.Interface.Okra;
using ZigOps.PaymentProcessing.Data.Interface.TransferWise;
using ZigOps.PaymentProcessing.Data.Repositories;
using ZigOps.PaymentProcessing.Data.Repositories.Okra;
using ZigOps.PaymentProcessing.Data.Repositories.TransferWise;
using ZigOps.PaymentProcessing.Service.Interfaces.Okra;
using ZigOps.PaymentProcessing.Service.Interfaces.TransferWise;
using ZigOps.PaymentProcessing.Service.Services.Okra;
using ZigOps.PaymentProcessing.Service.Services.TransferWise;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ZigOps.PaymentProcessing.API
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddTransient<IKeyvault, Keyvault>();
            builder.Services.AddTransient<ISendEmail, SendEmail>();
            builder.Services.AddTransient<MongoClientBuilder, MongoClientBuilder>();
            builder.Services.AddTransient<IGenerateRequest, GenerateRequest>();
            builder.Services.AddTransient<IOkraRepository, OkraRepository>();
            builder.Services.AddTransient<IOkraService, OkraService>();
            builder.Services.AddTransient<IOkraDatabaseService, OkraDatabaseService>();
            builder.Services.AddTransient<ITransferWiseRepository, TransferWiseRepository>();
            builder.Services.AddTransient<ITransferWiseService, TransferWiseService>();
            builder.Services.AddSingleton<IEmailRepository, EmailRepository>();
            builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();
            builder.Services.AddSingleton<ITransferwiseDatabaseRepository, TransferwiseDatabaseRepository>();
            builder.Services.AddSingleton<IOkraDatabaseRepository, OkraDatabaseRepository>();
        }
    }
}
