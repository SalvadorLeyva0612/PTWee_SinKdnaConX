using Microsoft.EntityFrameworkCore;
using PTWee_SinKdnaConX.Models;
using PTWee_SinKdnaConX.Services;
using System.Security.Principal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//----------------Obtiene cadena de conexión--------------------

builder.Services.AddDbContext<WeeCompanyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("sql_connection"),
    sqlServerOptionsAction: sqloption =>
    {
        sqloption.EnableRetryOnFailure(
            maxRetryCount: 20,
            maxRetryDelay: TimeSpan.FromSeconds(15),
            errorNumbersToAdd: null
            );
    })
);

builder.Services.AddScoped<IRegister, RegisterService>();

builder.Services.AddCors(option => option.AddPolicy("AllowAnyOrigin", builder => builder.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod()
         )
);
//---------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Utiliza la politica CORS
app.UseCors("AllowAnyOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
