using System.Text.Json;
using LoongEgg.LoongLogger;
using TCP_UDP通信;
using static 通知发部服务端.JSON.ToastJson;

namespace 通送发布服务端.MOD
{
    public class Message_Processing
    {
        /// <summary>
        /// 服务端对象
        /// </summary>
        private Server _Server;
        /// <summary>
        /// 客户端信息列表
        /// </summary>
        public Dictionary<int, Client_Data> ClientList = new();
        /// <summary>
        /// 订阅信息列表
        /// </summary>
        public Dictionary<string, List<Client_Data>> SubscribeList = new();

        /// <summary>
        /// 构造函数
        /// </summary>
        public Message_Processing(Server Server)
        {
            _Server = Server;
            _Server.TcpReceiveMessage += Receiving;//注册接收信息
            _Server.TcpConnect += Registration;//注册客户端
            _Server.TcpDisconnect += Cancellation;//注销客户端信息
        }
        /// <summary>
        /// 注册客户端
        /// </summary>
        /// <param name="ID">客户端ID</param>
        private void Registration(int ID)
        {
            ClientList!.Add(ID, new()
            {
                ID = ID,
                IP = _Server.GetClientIP(ID)
            });
            Logger.WriteInfor($"注册客户端 ID:{ID} IP:{ClientList[ID].IP}");
        }

        /// <summary>
        /// 接收信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Data"></param>
        private void Receiving(int ID, string Data)
        {
            string messageType;
            try
            {
                messageType = Data.Substring(0, Data.IndexOf(":"));// 获取消息类型
            }
            catch
            {
                Logger.WriteError($"IP:{ClientList[ID].IP} ID:{ID} 发送未知信息类型!");
                _Server.SendFileToClient(ID, "发送未知信息类型!");
                return;
            }
            switch (messageType)// 根据消息类型处理不同的信息
            {
                case nameof(Message_Type.Events):
                    string TEMP_Data = Data.Substring(Data.IndexOf(":") + 1);
                    var Temp_Json = JsonSerializer.Deserialize<Client_Data>(TEMP_Data);
                    ClientList[ID].Events = Temp_Json?.Events;
                    Logger.WriteInfor($"IP:{ClientList[ID].IP} ID:{ID} 设置订阅: {TEMP_Data}");

                    List<string> Temp_Projects = new();
                    foreach (var Temp_Data in ClientList[ID].Events!)
                    {
                        if (!SubscribeList.ContainsKey(Temp_Data))//判断订阅是否存在
                        {
                            SubscribeList.Add(Temp_Data, new() { ClientList[ID] });//不存在创建并存添加订阅
                            Temp_Projects.Add(Temp_Data);
                        }
                        else
                        {
                            if (!SubscribeList[Temp_Data].Contains(ClientList[ID]))//没有在列表里才添加
                            {
                                SubscribeList[Temp_Data].Add(ClientList[ID]);//存在添加订阅
                                Temp_Projects.Add(Temp_Data);
                            }
                        }
                    }
                    Logger.WriteInfor($"更新订阅成功! 受影响订阅数:{Temp_Projects.Count}");

                    break;
                case nameof(Message_Type.Message):
                    /*string TEMP_Data_Message = Data.Substring(Data.IndexOf(":") + 1);
                    var Temp_Json_Message = JsonSerializer.Deserialize<Toast_Return>(TEMP_Data_Message);
                    通知发部服务端.MOD.Toast.JsonPrint(Temp_Json_Message!);*/

                    break;
                case nameof(Message_Type.File):

                    break;
                case nameof(Message_Type.Command):

                    break;
            }
        }

        /// <summary>
        /// 注销客户端信息
        /// </summary>
        /// <param name="ID"></param>
        private void Cancellation(int ID)
        {
            if (ClientList[ID].Events != null)
            {
                foreach (var Temp_Data in ClientList[ID].Events!)//便利客户端订阅
                {
                    if (SubscribeList.ContainsKey(Temp_Data))//检查订阅池里是否存在此订阅
                    {
                        if (SubscribeList[Temp_Data].Count > 1)//有订阅且不止一个客户端订阅则移除此客户端
                        {
                            SubscribeList[Temp_Data].Remove(ClientList[ID]);
                        }
                        else
                        {
                            SubscribeList.Remove(Temp_Data);//没别人了毁灭八
                        }
                    }
                }
            }
            ClientList.Remove(ID);
            Logger.WriteInfor($"注销客户端ID:{ID}成功!");
        }

        /// <summary>
        /// 发送订阅信息
        /// </summary>
        /// <param name="SubscribeName">订阅名</param>
        /// <param name="Message">发送内容</param>
        public void SendSubscribeMessage(string SubscribeName, string Message)
        {
            try
            {
                foreach (var Temp_Data in SubscribeList[SubscribeName])//遍利订阅目标
                {
                    _Server.SendToClient(Temp_Data.ID, Message).ConfigureAwait(false);//发送订阅信息
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError($"向订阅目标: \"{SubscribeName}\" 发送信息失败因为:{ex.Message}");
                return;
            }
            Logger.WriteInfor($"向订阅目标: \"{SubscribeName}\" 的{SubscribeList[SubscribeName].Count}个客户端发送了 \"{Message}\"");
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
