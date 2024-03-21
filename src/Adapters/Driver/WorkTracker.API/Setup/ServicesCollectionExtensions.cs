
using FluentValidation;
using WorkTracker.Clock.Domain.Models;
using WorkTracker.Clock.Domain.Models.Validators;
using WorkTracker.Clock.Domain.Ports;
using WorkTracker.Clock.Domain.Repositories;
using WorkTracker.Clock.Domain.Services;
using WorkTracker.Clock.UseCase.Ports;
using WorkTracker.Clock.UseCase.UseCases;
using WorkTracker.Gateways.MySQL.Repositories;

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

            services.AddScoped<IValidator<Punch>, PunchValidator>();

            return services;
        }

        // public static IServiceCollection AddCommunicationServices(this IServiceCollection services)
        // {
        //     services.AddScoped<IHttpHandler, HttpHandler>();
        //     services.AddScoped<IMessenger, Messenger>();

        //     services.AddHostedService<DemandMessagesConsumer>();

        //     return services;
        // }
    }
}