﻿
//using Mcv.Core.SiteMessage;
//using Mcv.Core.SiteMessage.YouTubeLive;
using ryu_s.YouTubeLive.Message.Action;
using Plugin;
using System.Collections.Generic;
using ryu_s.YouTubeLive.Message;

namespace Mcv.SitePlugin.YouTubeLive
{
    class SiteMessageImplFactory
    {
        public static ISiteMessage? Parse(IAction action)
        {
            if (action is TextMessage text)
            {
                var message = text.MessageItems;
                var name = text.AuthorName ?? string.Empty;
                var authorBadge = new List<IBadge>();
                foreach (var badge in text.AuthorBadges)
                {
                    if (badge is AuthorBadgeCustomThumb custom)
                    {
                        authorBadge.Add(new RemoteIconPart(custom.Thumbnails[1].Url, 16, 16, custom.Tooltip));
                    }
                    else if (badge is AuthorBadgeIcon icon)
                    {
                        //icon.IconType
                        //OWNER
                        //MODERATOR
                        //VERIFIED
                        var data = icon.IconType switch
                        {
                            "MODERATOR" => @"<svg viewBox=""0 0 16 16"" preserveAspectRatio=""xMidYMid meet"" focusable=""false"" class=""style-scope yt-icon"" style=""pointer-events: none; display: block; width: 100%; height: 100%;"" xmlns=""http://www.w3.org/2000/svg"" version=""1.1""><g class=""style-scope yt-icon""><path d=""M9.64589146,7.05569719 C9.83346524,6.562372 9.93617022,6.02722257 9.93617022,5.46808511 C9.93617022,3.00042984 7.93574038,1 5.46808511,1 C4.90894765,1 4.37379823,1.10270499 3.88047304,1.29027875 L6.95744681,4.36725249 L4.36725255,6.95744681 L1.29027875,3.88047305 C1.10270498,4.37379824 1,4.90894766 1,5.46808511 C1,7.93574038 3.00042984,9.93617022 5.46808511,9.93617022 C6.02722256,9.93617022 6.56237198,9.83346524 7.05569716,9.64589147 L12.4098057,15 L15,12.4098057 L9.64589146,7.05569719 Z"" class=""style-scope yt-icon""></path></g></svg>",
                            "OWNER" => null,
                            "VERIFIED" => null,
                            _ => null,
                        };
                        if (data != null)
                        {
                            authorBadge.Add(new SvgIconPart(data, 16, 16, icon.Tooltip));
                        }
                    }
                }
                return new YouTubeLiveComment(message, name, authorBadge)
                {
                    AuthorChannelId=text.AuthorExternalChannelId,
                    Id = text.Id,
                    TimeStampUsec=text.TimestampUsec,
                };
            }
            else
            {
                return null;
            }
        }
    }
}
