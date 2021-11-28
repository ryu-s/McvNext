using Mcv.YouTubeLive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Mcv.Plugin.MainViewPlugin.Site.YouTubeLive;

internal class McvYouTubeLiveSettings : IYouTubeLiveSettings
{
    public Color PaidCommentBackColor { get; set; } = Colors.Red;
    public Color PaidCommentForeColor { get; set; } = Colors.Blue;
    public Color MembershipBackColor { get; set; } = Colors.Yellow;
    public Color MembershipForeColor { get; set; } = Colors.Brown;
    public bool IsAutoSetNickname { get; set; } = true;
    public bool IsAllChat { get; set; } = true;
}
