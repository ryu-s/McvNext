using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcv.SitePlugin.YouTubeLive
{
    class Options
    {
        /// <summary>
        /// すべてのチャットを取得するか
        /// falseなら上位チャットのみ
        /// </summary>
        public bool IsAllChat { get; set; } = true;
        public int IconSize { get; set; } = 16;

    }
}
