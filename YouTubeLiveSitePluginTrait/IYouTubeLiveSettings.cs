using Plugin;

namespace Mcv.YouTubeLive
{
    public interface IYouTubeLiveSettings : IPluginSettings
    {
        bool IsAllChat { get; }
    }
    public interface IYouTubeLiveSettingsDiff : IPluginSettingsDiff
    {
        bool? IsAllChat { get; }
        public static IYouTubeLiveSettingsDiff GetDiff(IYouTubeLiveSettings original, IYouTubeLiveSettings modified)
        {
            return new YouTubeLiveSettingsDiff
            {
                IsAllChat = original.IsAllChat == modified.IsAllChat ? null: modified.IsAllChat,
            };
        }
    }
    class YouTubeLiveSettingsDiff : IYouTubeLiveSettingsDiff
    {
        public bool? IsAllChat { get; set; }
    }
}