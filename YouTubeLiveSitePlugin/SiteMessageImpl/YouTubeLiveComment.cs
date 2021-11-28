using Mcv.YouTubeLive;
using ryu_s.YouTubeLive.Message;
using System.Collections.Generic;

namespace Mcv.SitePlugin.YouTubeLive
{
    class YouTubeLiveComment : IYouTubeLiveComment
    {
        public YouTubeLiveComment(IReadOnlyList<IMessagePart> messageItems, string name,IReadOnlyList<IBadge> badges)
        {
            MessageItems = messageItems;
            AuthorName = name;
            AuthorBadges = badges;
        }

        public IReadOnlyList<IMessagePart> MessageItems { get; }
        public string AuthorName { get; }
        public IReadOnlyList<IBadge> AuthorBadges { get; }
        public string AuthorChannelId { get; init; }
        public string Id { get; init; }
        public long TimeStampUsec { get; init; }
    }
}
