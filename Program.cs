using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Hubs;

var builder = WebApplication.CreateBuilder(args);  // 初始化 builder

// 設定 CORS 策略
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:8080")  // Vue 前端的 URL
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // 允許傳遞 Cookie 或授權信息
    });
});

// 註冊 SignalR 服務
builder.Services.AddSignalR();

var app = builder.Build();  // 創建應用程序

// 使用 CORS 策略
app.UseCors("AllowFrontend");

// 設定路由與 Hub 路徑
app.UseRouting();
app.MapHub<NotificationHub>("/notificationHub");

app.Run();  // 啟動應用
