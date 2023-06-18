namespace 通知发部服务端.JSON
{
    public class ToastJson
    {
        public class AddHeader
        {
            /// <summary>
            /// 
            /// </summary>
            public string? Id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string? Title { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string? Arguments { get; set; }
        }

        public class ProgressBar
        {
            /// <summary>
            /// 
            /// </summary>
            public string? Name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double? Value { get; set; }
        }

        public class Message
        {
            /// <summary>
            /// 
            /// </summary>
            public AddHeader? AddHeader { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string? Text { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string? AppLogoOverride { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string? HeroImage { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string? InlineImage { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string? SetProtocolActivation { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public ProgressBar? ProgressBar { get; set; }
        }

        public class Toast_Return
        {
            /// <summary>
            /// 
            /// </summary>
            public Message? Message { get; set; }
        }

    }
}
