using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using server.Features.GetChatById;
using server.Features.SaveConversation;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR();
builder.Services.AddAwsDynamoDb(configuration);
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

internal static class ServiceCollectionExtension
{
    public static void AddMediatR(this IServiceCollection services) =>
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Program>());

    public static void AddAwsDynamoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetAWSOptions();
        services.AddDefaultAWSOptions(options);
        services.AddAWSService<IAmazonDynamoDB>();
        services.AddScoped<IDynamoDBContext, DynamoDBContext>();
    }
}


internal static class WebApplicationExtension
{
    public static void MapControllers(this WebApplication app)
    {
        SaveConversation.AddRoute(app);
        GetChatById.AddRoute(app);
    }
}

public partial class Program;