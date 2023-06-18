using System.Text.Json;
using LoongEgg.LoongLogger;
using TCP_UDP通信;
using 通知推送客户端.MOD;
using static 通知推送客户端.JSON.Events_JSON;
using static 通知推送客户端.JSON.ToastJson;

Logger.Enable(LoggerType.Console | LoggerType.Debug, LoggerLevel.Debug);//注册Log日志函数

Events_Return Events = new()
{ 
    Events = new() 
    {
        "test"
    }
};

Events_Return Events2 = new()
{
    Events = new()
    {
        "test2"
    }
};

Toast_Return Toast = new()
{
    Message = new() 
    {
        Text = "你好丫服务器!",
    }
};

Client Client = new("127.0.0.1",8787,true);
//Client Client = new("445720.xyz", 8787, true);
Client.ConnectTCP();
Message_Processing Message_Processing = new(Client);
Client.DataReceived += Data;//接收服务器发送信息

//Client.Send("Events:"+JsonSerializer.Serialize(Events));//订阅事件

//Client.Send("Message:" + JsonSerializer.Serialize(Toast));//订阅事件


while (true)
{
    Client.Send("Events:" + JsonSerializer.Serialize(Events));//订阅事件
    Thread.Sleep(10000);
    Client.Send("Events:" + JsonSerializer.Serialize(Events2));//订阅事件
    Thread.Sleep(10000);
}




void Data (string input)
{
    //Console.WriteLine(input);
}