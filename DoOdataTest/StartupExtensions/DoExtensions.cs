using Microsoft.Extensions.DependencyInjection;
using Xtensive.Orm;
using Xtensive.Orm.Configuration;

namespace DoOdataTest.StartupExtensions
{
    public static class DoExtensions
    {
        public static IServiceCollection AddDo(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var configuration = new DomainConfiguration("sqlite:///.\\doodatatest.db")
                {
                    UpgradeMode = DomainUpgradeMode.Recreate,
                };
                configuration.Types.Register(typeof(Program).Assembly);
                return configuration;
            });

            services.AddSingleton(sp =>
            {
                return Domain.Build(sp.GetRequiredService<DomainConfiguration>());
            });

            services.AddScoped(sp =>
            {
                var sessionConfig = new SessionConfiguration(SessionOptions.NonTransactionalReads);
                var session = sp.GetRequiredService<Domain>().OpenSession(sessionConfig);
                session.Activate(true);
                return session;
            });

            return services;
        }
    }
}
