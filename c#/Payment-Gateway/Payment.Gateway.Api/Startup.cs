namespace Payment.Gateway.Api
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Payment.Gateway.Data.Constants;
    using Payment.Gateway.Data.Repositories;
    using Payment.Gateway.Logic.Interfaces;
    using Payment.Gateway.Logic.Services;
    using Payment.Gateway.Data.Validators;
    using Payment.Gateway.Logic.Helpers;
    using Microsoft.AspNetCore.Authentication.AzureAD.UI;
    using Microsoft.AspNetCore.Authentication;
    using System.Net.Http.Headers;
    using Payment.Gateway.Data.Models;
    using Payment.Gateway.Data.Models.MtnModels;

    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
               .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));

            services.AddSingleton<IMongoClientBuilder, MongoClientBuilder>();
            services.AddTransient<ICacheManager, CacheManagerService>();
            services.AddTransient<IValidators, Validators>();
            services.AddTransient<PaymentValidators>();
            services.AddTransient<QueryDRValidators>();
            services.AddTransient<RefundValidators>();
            services.AddTransient<IHelpers,Helpers>();
            services.AddTransient<VoidPaymentValidators>();
            services.AddTransient<MTNReferenceValidators>();
            services.AddTransient<MTNTransactionValidators>();
            services.AddSingleton(s => s.GetRequiredService<IMongoClientBuilder>().BuilderMongoClient());
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IMtnService, MtnPaymentService>();

            services.AddHttpClient("ChargeBackClient", c => {
                c.BaseAddress = new Uri(Configuration["GatewayProperties:vcp-ChargeBackUrl"]);
            });

            services.AddHttpClient("PaymentClient", c => {
                c.BaseAddress = new Uri(Configuration["GatewayProperties:vpc-PaymentServerURL"]);
            });

            services.AddHttpClient(Constants.MtnRequestFields.MtnClientName, c => {
                c.BaseAddress = new Uri(Configuration["MtnConfiguration:BaseUrl"]);
                c.DefaultRequestHeaders.Add(Constants.MtnRequestFields.subscriptionKey, Configuration["MtnConfiguration:SubscriptionAuthKey"]);
            });

            services.AddHttpClient("VPC", c =>
            {
                c.BaseAddress = new Uri(Environment.GetEnvironmentVariable(Constants.AppSettings.Paymentserviceurlname));
                c.DefaultRequestHeaders.Add("Accept", "application/x-www-form-urlencoded");
                c.DefaultRequestHeaders.Add("User-Agent", "Zig-PaymentGateway-Client");
            });

            services.AddEasyCaching(options => {
                options.UseInMemory(Constants.EasyCachingFields.InMemoryCache);
            });

            services.AddControllers();

            services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
