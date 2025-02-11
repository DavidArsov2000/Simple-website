using Backend.DatabaseContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Dodajanje dbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// Dodajanje dovolenja za dostop
var _allowSpecificOrigin = "allowSpecificOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: _allowSpecificOrigin, policy =>
    {
        policy.WithOrigins("http://localhost:5272");
        policy.WithMethods("GET","POST", "OPTIONS");
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors(_allowSpecificOrigin);
// Spletna stran je dostopna na http://localhost:5272/index.html ter https://localhost:7108/index.html
app.UseStaticFiles();

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
