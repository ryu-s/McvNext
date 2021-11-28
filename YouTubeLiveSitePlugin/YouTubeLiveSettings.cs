using System;
using System.Diagnostics;
using Mcv.YouTubeLive;

namespace Mcv.SitePlugin.YouTubeLive
{
    class YouTubeLiveSettings : McvPluginUtils.DynamicOptionsBase, IYouTubeLiveSettings
    {
        public bool IsAllChat { get => GetValue(); set => SetValue(value); }

        protected override void Init()
        {
            Dict.Add(nameof(IsAllChat), new Item { DefaultValue = true, Predicate = b => true, Serializer = b => b.ToString(), Deserializer = s => bool.Parse(s) });
        }
        internal void Set(YouTubeLiveSettings changedOptions)
        {
            foreach (var src in changedOptions.Dict)
            {
                var v = src.Value;
                SetValue(v.Value, src.Key);
            }
        }
        internal void Set(IYouTubeLiveSettingsDiff ytDiff)
        {
            if (ytDiff.IsAllChat.HasValue)
            {
                IsAllChat = ytDiff.IsAllChat.Value;
            }
        }
    }
}
