using Microsoft.Toolkit.Uwp.Notifications;
using static 通知发部服务端.JSON.ToastJson;

namespace 通知发部服务端.MOD
{
    public static class Toast
    {
        public static void JsonPrint(Toast_Return Toast_Return)
        {
            ToastContentBuilder ToastContentBuilder = new ();
            if (Toast_Return?.Message?.AddHeader != null)
                ToastContentBuilder.AddHeader(Toast_Return?.Message?.AddHeader.Id, Toast_Return?.Message?.AddHeader.Title, Toast_Return?.Message?.AddHeader.Arguments);
            if(Toast_Return?.Message?.Text != null)
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
    }
}
