using System.Text.Json;
using LoongEgg.LoongLogger;
using TCP_UDP通信;
using static 通知推送客户端.JSON.ToastJson;

namespace 通知推送客户端.MOD
{
    public class Message_Processing
    {
        public Client _Client;

        public Message_Processing(Client Client)
        {
            _Client = Client;
            _Client.DataReceived += Receiving;
        }
        /// <summary>
        /// 处里接收到的数据
        /// </summary>
        /// <param name="Data">数据</param>
        private void Receiving(string Data)
        {
            string messageType;
            try
            {
                messageType = Data.Substring(0, Data.IndexOf(":"));// 获取消息类型
            }
            catch
            {
                Logger.WriteError($"接收到未知信息类型!");
                return;
            }
            switch (messageType)// 根据消息类型处理不同的信息
            {
                case nameof(Message_Type.Message):
                    string TEMP_Data_Message = Data.Substring(Data.IndexOf(":") + 1);
                    var Temp_Json_Message = JsonSerializer.Deserialize<Toast_Return>(TEMP_Data_Message);
                    Toast.JsonPrint(Temp_Json_Message!);

                    break;
                case nameof(Message_Type.File):

                    break;
                case nameof(Message_Type.Command):

                    break;
            }
        }
    }
    /// <summary>
    /// 客户端信息
    /// </summary>
    public class Client_Data
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID;
        /// <summary>
        /// IP
        /// </summary>
        public string? IP { get; set; } = ":::";
        /// <summary>
        /// 订阅事件
        /// </summary>
        public List<string>? Events { get; set; } = new();
    }
    /// <summary>
    /// 信息类型
    /// </summary>
    public enum Message_Type
    {
        Events,
        Message,
        File,
        Command
    }
}