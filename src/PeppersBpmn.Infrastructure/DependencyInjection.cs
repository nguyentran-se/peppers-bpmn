using System.Net.Http.Headers;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeppersBpmn.Application.Services;
using PeppersBpmn.Domain.Repositories;
using PeppersBpmn.Infrastructure.Camunda;
using PeppersBpmn.Infrastructure.Persistence;
using PeppersBpmn.Infrastructure.Repositories;

namespace PeppersBpmn.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        var camundaSection = configuration.GetSection("Camunda");
        var baseUrl = camundaSection["BaseUrl"]!.TrimEnd('/') + "/engine-rest/";
        var username = camundaSection["Username"] ?? "demo";
        var password = camundaSection["Password"] ?? "demo";
        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

        services.AddHttpClient<CamundaClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddScoped<ICamundaService, CamundaService>();
        services.AddScoped<IProcessDefinitionRepository, ProcessDefinitionRepository>();
        services.AddScoped<IProcessInstanceRepository, ProcessInstanceRepository>();

        return services;
    }
}
