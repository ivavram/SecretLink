var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IspitCS"));
});

builder.Services.AddCors(options =>
{

    options.AddPolicy("CORS", builder =>
    {
        builder.SetIsOriginAllowed(host => true)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });

    /*options.AddPolicy("CORS", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithOrigins("http://localhost:3000",
                           "http://127.0.0.1:3000",
                           "https://localhost:3000",
                           "https://127.0.0.1:3000"); 
    });*/
});


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<Interface.IUnitOfWOrk, Repository.UnitOfWork>(); 
builder.Services.AddScoped<PlayerService>(); 
builder.Services.AddScoped<WordService>(); 
builder.Services.AddScoped<Connect4Service>(); 
builder.Services.AddScoped<GuessTheWordService>(); 
builder.Services.AddScoped<GameService>(); 
builder.Services.AddScoped<PlayerInGameService>(); 
//builder.Services.AddScoped<HubService>();

builder.Services.AddSingleton<MessageHub>();

builder.Services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.MaximumReceiveMessageSize = int.MaxValue;
                //options.KeepAliveInterval = TimeSpan.FromSeconds(15);
                //options.ClientTimeoutInterval = TimeSpan.FromMinutes(5);
            });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CORS");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<MessageHub>("messageHub");

app.Run();
