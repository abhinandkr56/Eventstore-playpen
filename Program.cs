
using EventStore.ClientAPI;
using EventstoreBasics.Infrastructure;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var esConnection = EventStoreConnection.Create(builder.Configuration["eventStore:connectionString"],
        ConnectionSettings.Create().KeepReconnecting(),"webinar");

// var entityStore = new AggregateStore(esConnection);

// builder.Services.AddSingleton<IAggregateStore>(entityStore);

builder.Services.AddSingleton(esConnection);
builder.Services.AddSingleton<IAggregateStore, AggregateStore>();
builder.Services.AddHostedService<EventStoreHostedService>();


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
