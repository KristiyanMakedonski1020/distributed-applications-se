# Escape Room Management API

Име: Кристиян Македонски
Факултетен номер: 2401321020
## Описание на проекта
Това е RESTful Web API за управление на ескейп стаи, изградено с ASP.NET Core 8 и Entity Framework Core. Проектът включва база данни с три свързани таблици, пълни CRUD операции, филтриране по множество критерии, сортиране, странициране, JWT автентикация и глобална обработка на грешки (RFC 7807).
## Инструкции за инсталация и стартиране
1. Отворете `EscapeRoomSystem.sln` чрез Visual Studio 2022.
2. Във файла `appsettings.json` променете `DefaultConnection` стринга с данните за вашия локален SQL Server (SSMS).
3. Отворете `Package Manager Console` (Tools -> NuGet Package Manager) и изпълнете командите:
   - `Add-Migration InitialCreate`
   - `Update-Database`
4. Стартирайте проекта с бутона `Run` (F5). Swagger интерфейсът с готовата документация ще се зареди автоматично в браузъра.
