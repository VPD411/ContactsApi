using ContactsApi.Infrastructures.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); // Создаем builder, который дальше будет "строить" приложение

// Подключем SQLite
builder.Services.AddDbContext<ContactsDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Контроллеры
builder.Services.AddControllers(); // Добавляет контроллеры
builder.Services.AddEndpointsApiExplorer(); // Добавляем "обозреватель" API эндпоинтов
builder.Services.AddSwaggerGen(); // Добавляем Swagger

var app = builder.Build();

// Если находимся в разработке (development)...
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Используем сваггер
    app.UseSwaggerUI(); // Используем пользовательский интерфейс сваггера
}

app.UseHttpsRedirection(); // Перенаправляет все HTTP запросы на HTTPS
app.MapControllers(); // Маппим контроллеры

// Автоматические создание БД и применение миграций
// Создаем область
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ContactsDbContext>(); // Получаем экземпляр DbContext
    db.Database.EnsureCreated(); // Если БД создана - норм, если нет - создаем
    db.Database.Migrate(); // Применяем все миграции автоматически
}


// Точка запуска приложения
app.Run();