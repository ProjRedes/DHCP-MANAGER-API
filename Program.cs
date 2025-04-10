using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using DHCPManagerAPI.Services; // Adicione esta linha

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configuração do CORS (se necessário)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// 🔹 Configuração da autenticação JWT
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException("Chave JWT não configurada!");
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();

// 🔹 Configuração do Google Sheets
var googleSheetsCredential = builder.Configuration["GoogleSheets:CredentialsPath"]
    ?? throw new ArgumentNullException("Credencial do Google Sheets não configurada!");

var spreadsheetId = builder.Configuration["GoogleSheets:SpreadsheetId"]
    ?? throw new ArgumentNullException("ID da planilha do Google Sheets não configurado!");

// Adiciona o serviço do Google Sheets passando os parâmetros corretamente
builder.Services.AddScoped<GoogleSheetsService>(provider =>
    new GoogleSheetsService(googleSheetsCredential, spreadsheetId));

// 🔹 Adiciona serviços necessários (ex: Google Sheets, Autenticação)
builder.Services.AddScoped<AuthService>();

// 🔹 Adiciona controladores
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 Configuração do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
