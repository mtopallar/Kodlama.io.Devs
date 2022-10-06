using Application.Features.Auths.Rules;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.SubTechnologies.Rules;
using Application.Features.Users.Rules;
using Application.Features.UserWebAddresses.Rules;
using Application.Services.AuthService;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<ProgrammingLanguageBusinessRules>();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddScoped<SubTechnologyBusinessRules>();
            services.AddScoped<UserWebAddressBusinessRules>();
            services.AddScoped<UserBusinessRules>();
            services.AddScoped<AuthBusinessRules>();
            services.AddScoped<IAuthService, AuthManager>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>)); //metod bazlı aspect
            

            // validation, authorization, caching, log, busines rules...

            return services;
        }
    }
}
