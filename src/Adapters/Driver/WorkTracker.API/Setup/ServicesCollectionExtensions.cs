
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using FluentValidation;
using WorkTracker.Clock.Domain.Models;
using WorkTracker.Clock.Domain.Models.Validators;
using WorkTracker.Clock.Domain.Ports;
using WorkTracker.Clock.Domain.Repositories;
using WorkTracker.Clock.Domain.Services;
using WorkTracker.Clock.UseCase.Ports;
using WorkTracker.Clock.UseCase.UseCases;
using WorkTracker.Gateways.Http;
using WorkTracker.Gateways.MySQL.Repositories;
using WorkTracker.Gateways.Notification;
using Microsoft.Extensions.Configuration;
using Amazon.Runtime;
using Amazon.CognitoIdentityProvider;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesColletionExtensions
    {
        public static IServiceCollection AddClockServices(
            this IServiceCollection services)
        {
            services.AddScoped<IPunchesRepository, PunchesRepository>();

            services.AddScoped<IPunchUseCases, PunchUseCases>();

            services.AddScoped<IPunchService, PunchService>();
            services.AddScoped<IUtilsService, UtilsService>();

            services.AddScoped<IValidator<Punch>, PunchValidator>();

            return services;
        }

        public static IServiceCollection AddComunicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IHttpHandler, HttpHandler>();
            services.AddScoped<IEmailNotificationService, EmailNotificationService>();

            services.AddDefaultAWSOptions(new AWSOptions
            {
                Region = RegionEndpoint.GetBySystemName(configuration["AWS_REGION"])
                // Credentials = new BasicAWSCredentials(configuration["AWS:AccessKey"], configuration["AWS:SecretKey"])
            });

                services.AddAWSService<IAmazonCognitoIdentityProvider>();

            return services;
        }
    }
}