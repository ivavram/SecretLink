var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IspitCS"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("http://127.0.0.1:5555",
                           "https://127.0.0.1:5555",
                           "http://localhost:5268",
                           "https://localhost:5268");
    });
});

builder.Services.AddControllers();
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
builder.Services.AddScoped<HubService>();
builder.Services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
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
