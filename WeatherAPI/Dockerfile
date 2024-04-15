# Используем образ SDK .NET Core для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем файлы проекта и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем исходный код и собираем проект
COPY . ./
RUN dotnet publish -c Release -o out

# Второй этап сборки: создаем образ для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Устанавливаем переменную среды для соединения с PostgreSQL
ENV ConnectionStrings__DefaultConnection "Host=localhost;Port=5432;Database=weatherDB;Username=postgres;Password=123"

# Указываем порт, который будет прослушивать ваше приложение
EXPOSE 5432
ENTRYPOINT ["dotnet", "YourProjectName.dll"]
