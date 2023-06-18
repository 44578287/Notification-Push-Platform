using System.Text.Json;
using Microsoft.Toolkit.Uwp.Notifications;
using static 通知推送客户端.JSON.Events_JSON;
using static 通知推送客户端.JSON.ToastJson;

namespace 通知推送客户端.MOD
{
    public static class Toast
    {
        /// <summary>
        /// 接收Json转Toast本地通知
        /// </summary>
        /// <param name="Toast_Return">Json</param>
        public static void JsonPrint(Toast_Return Toast_Return)
        {
            ToastContentBuilder ToastContentBuilder = new();
            if (Toast_Return?.Message?.AddHeader != null)
                ToastContentBuilder.AddHeader(Toast_Return?.Message?.AddHeader.Id, Toast_Return?.Message?.AddHeader.Title, Toast_Return?.Message?.AddHeader.Arguments);
            if (Toast_Return?.Message?.Text != null)
                ToastContentBuilder.AddText(Toast_Return?.Message?.Text);
            if (Toast_Return?.Message?.HeroImage != null)
                ToastContentBuilder.AddHeroImage(new Uri(Toast_Return?.Message?.HeroImage!));
            if (Toast_Return?.Message?.InlineImage != null)
                ToastContentBuilder.AddInlineImage(new Uri(Toast_Return?.Message?.InlineImage!));
            if (Toast_Return?.Message?.AppLogoOverride != null)
                ToastContentBuilder.AddAppLogoOverride(new Uri(Toast_Return?.Message?.AppLogoOverride!));
            if (Toast_Return?.Message?.SetProtocolActivation != null)
                ToastContentBuilder.SetProtocolActivation(new Uri(Toast_Return?.Message?.SetProtocolActivation!));
            if (Toast_Return?.Message?.ProgressBar != null)
                ToastContentBuilder.AddProgressBar(Toast_Return?.Message?.ProgressBar.Name, Toast_Return?.Message?.ProgressBar.Value);

            ToastContentBuilder.Show();
        }
        /// <summary>
        /// 发送Toast信息
        /// </summary>
        /// <param name="Toast_Data">Toast内容</param>
        /// <returns>Toast信息发送字符串</returns>
        public static string Toast_PUSH(Toast_Return Toast_Data)
        {
            return $"Message:{JsonSerializer.Serialize(Toast_Data)}";
        }
        /// <summary>
        /// 发送Toast订阅信息
        /// </summary>
        /// <param name="Toast_Events">订阅内容</param>
        /// <returns>Toast订阅发送字符串</returns>
        public static string Toast_EVENTS(Events_Return Toast_Events)
        {
            return $"Events:{JsonSerializer.Serialize(Toast_Events)}";
        }
    }
}
