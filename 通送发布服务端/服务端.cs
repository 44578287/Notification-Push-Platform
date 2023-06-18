using System.Text.Json;
using LoongEgg.LoongLogger;
using TCP_UDPͨ��;
using ͨ�ͷ��������.MOD;
using static ֪ͨ���������.JSON.ToastJson;

Logger.Enable(LoggerType.Console | LoggerType.Debug, LoggerLevel.Debug);//ע��Log��־����

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
            Id = "��ӭ",
            Title = "���ǲ���ͨ������Ϣ",
            Arguments = "��ӭ��Ϣ"
        },
        Text = "���ѾС�ͻ���!",
        AppLogoOverride = @"C:\Users\g9964\Pictures\ͷ��\234661739_128481282799169_3989662338006505379_n.jpg",
    }
};

while (true)
{
    Message_Processing.SendSubscribeMessage("test", "Message:" + JsonSerializer.Serialize(Toast));
    //Message_Processing.SendSubscribeMessage("test","���Զ���ͨ��");
    //await Server.SendToAllClients("��ӭ����!");
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
