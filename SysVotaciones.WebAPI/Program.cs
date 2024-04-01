using SysVotaciones.BLL;
using SysVotaciones.EN;

var builder = WebApplication.CreateBuilder(args);
var myRules = "Permissions";
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(myRules, builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("connectionString") ?? "";

builder.Services.AddScoped(serviceProvider => new YearBLL(connectionString));
builder.Services.AddScoped(serviceProvider => new CareerBLL(connectionString));
builder.Services.AddScoped(serviceProvider => new StudentBLL(connectionString));
builder.Services.AddScoped(serviceProvider => new ContestBLL(connectionString));
builder.Services.AddScoped(serviceProvider => new CategoryBLL(connectionString));
builder.Services.AddScoped(serviceProvider => new ParticipantBLL(connectionString));
builder.Services.AddScoped(serviceProvider => new AdminBLL(connectionString));
builder.Services.AddScoped(serviceProvider => new TypeContestBLL(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(myRules);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
