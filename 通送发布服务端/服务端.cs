using System.Text.Json;
using LoongEgg.LoongLogger;
using TCP_UDP通信;
using 通送发布服务端.MOD;
using static 通知发部服务端.JSON.ToastJson;

Logger.Enable(LoggerType.Console | LoggerType.Debug, LoggerLevel.Debug);//注册Log日志函数

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.


Server Server = new(true, 8787);
Message_Processing Message_Processing = new(Server);

Toast_Return Toast = new()
{
    Message = new()
    {
        AddHeader = new() 
        {
            Id = "欢迎",
            Title = "这是测试通道的信息",
            Arguments = "欢迎信息"
        },
        Text = "你好丫小客户端!",
        AppLogoOverride = @"C:\Users\g9964\Pictures\头像\234661739_128481282799169_3989662338006505379_n.jpg",
    }
};

while (true)
{
    Message_Processing.SendSubscribeMessage("test", "Message:" + JsonSerializer.Serialize(Toast));
    //Message_Processing.SendSubscribeMessage("test","测试订阅通道");
    //await Server.SendToAllClients("欢迎连接!");
    Thread.Sleep(10000);
}













builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
