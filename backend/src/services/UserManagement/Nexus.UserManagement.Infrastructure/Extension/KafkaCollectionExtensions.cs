using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nexus.UserManagement.Application.Abstractions.Events;
using Nexus.UserManagement.Application.Events;
using Nexus.UserManagement.Infrastructure.MessageBroker;
using Shared.Abstractions.Messaging;
using Shared.Contracts.UserManagement.Events;
using Shared.Messaging;

namespace Nexus.UserManagement.Infrastructure.Extension
{
    public static class KafkaCollectionExtensions
    {
        public static IServiceCollection RegisterMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ProducerConfig>(configuration.GetSection("Kafka:Producer"));
            services.AddSingleton(sp => new ProducerBuilder<string, string>(sp.GetRequiredService<IOptions<ProducerConfig>>().Value).Build());
            services.AddSingleton<IEventPublisher, KafkaProducer>();
            services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
            services.AddSingleton<ITopicResolver>(provider =>
            {
                var resolver = new TopicResolver()
                    .Map<UserCreatedIntegrationEvent>("user-management.user.account-created")
                    .Map<UserPasswordResetIntegrationEvent>("user-management.user.password-reset")
                    .Map<UserAccountDeletedIntegrationEvent>("user-management.user.account-delete")
                    .Map<PasswordResetRequestedIntegrationEvent>("crossdyne-notifications")
                    .Map<ChangeEmailRequestedIntegrationEvent>("crossdyne-notifications");
                
               return resolver;
            });

            return services;
        }
    }
}