using Alibaba.Heracles.Application.Common.Interfaces;
using Alibaba.Heracles.Application.Throttlings;
using Alibaba.Heracles.Application.Throttlings.Services;
using Alibaba.Heracles.Infrastructure.Persistence;
using Alibaba.Heracles.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Alibaba.Heracles.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IDateTime, DateTimeService>();
            // using in memory ef core database
            // this must be replaced by a proper database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("Alibaba.Heracles"));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            // this service can be replaces with a query
            // some design change may be needed and move this matching to database query
            services.AddSingleton<IRequestMatcher, RegexRequestMatcher>();
            // for a more convenient implementation redis is a good one
            // we can use redis list for storing time of access and use its 
            // expiration to expire any entry from more than 1 hour ago(max bucket)
            // or maybe use a number of different bucket (for each pattern-ip)
            // todo: worth to check it out
            services.AddSingleton<IRequestStore,InMemoryRequestStore>();

            return services;
        }
    }
}