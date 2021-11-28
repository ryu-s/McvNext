using Plugin;
using System.Collections.Generic;
namespace Mcv.Plugin.MainViewPlugin
{
    class SiteMessageParser
    {
        private readonly List<ISiteStrategy> _strategies;

        public SiteMessageParser(List<ISiteStrategy> strategies)
        {
            _strategies = strategies;
        }
        public IMcvCommentViewModel? Parse(ISiteMessage siteMessage, ConnectionNameViewModel nameVm,IAdapter adapter)
        {
            foreach(var site in _strategies)
            {
                if(site.TryParseMessage(siteMessage, nameVm,adapter, out var cvm))
                {
                    return cvm;
                }
            }
            return null;
        }
    }
}
