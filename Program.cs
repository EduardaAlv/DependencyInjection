using DependencyInjection.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Objeto muda a cada vez que é instanciado
builder.Services.AddTransient<IApplicationBuilder, ApplicationBuilder>();


//Objeto só muda por request
builder.Services.AddScoped<IApplicationBuilder, ApplicationBuilder>();
builder.Services.AddScoped<IService, Service>();

//Objeto não muda 
builder.Services.AddSingleton<IApplicationBuilder, ApplicationBuilder>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
