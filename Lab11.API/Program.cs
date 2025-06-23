// --- Program.cs (ajuste mínimo) --------------------------------
using Lab11.Application.Handlers;
using Lab11.Application.Common;
using Lab11.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Hangfire.SqlServer;
using Lab11.Application.Services;           // 👈 ya lo tenías
using Lab11.Application.Services;           // (duplicado eliminado si existía)

var builder = WebApplication.CreateBuilder(args);

// ▶ Servicios base
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DataCleanupService>();

// ▶ DbContext  ❗️——— CAMBIO ÚNICO ———❗️
// ⛔️ COMENTA la línea InMemory:
//// builder.Services.AddDbContext<AppDbContext>(opt =>
////     opt.UseInMemoryDatabase("EmpresaDb"));

// ✅ ACTIVA SQL Server (usa la misma cadena DefaultConnection de appsettings.json):
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ▶ MediatR
builder.Services.AddMediatR(typeof(AddEmpresaCommandHandler).Assembly);

// ▶ Hangfire (sin cambios)
builder.Services.AddHangfire(c =>
    c.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();

// ▶ Jobs (sin cambios)
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<UserCleanupService>();

var app = builder.Build();

// ▶ Swagger sólo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ▶ Middleware
app.UseAuthorization();
app.MapControllers();
app.UseHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate<UserCleanupService>(
    "limpiar-usuarios-antiguos",
    x => x.CleanOldUsers(),
    Cron.Daily);

// ▶ Jobs recurrentes
RecurringJob.AddOrUpdate<NotificationService>(
    "job-notificacion-diaria",
    s => s.SendNotification("usuario_diario"),
    Cron.Daily);

RecurringJob.AddOrUpdate<DataCleanupService>(
    "job-limpieza-empresas-antiguas",
    s => s.LimpiarEmpresasAntiguas(),
    Cron.Daily);

app.Run();
// ---------------------------------------------------------------
