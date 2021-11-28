using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ToPlugin = Plugin.Message.ToPlugin;
using ToCore = Plugin.Message.ToCore;
using System.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Text;
using Plugin;
using ryu_s.YouTubeLive.Message.Action;
using ryu_s.YouTubeLive.Message;
using ryu_s.YouTubeLive.Message.Continuation;
using Mcv.YouTubeLive;
using System.Diagnostics;

namespace Mcv.SitePlugin.YouTubeLive
{
    public class YouTubeSitePlugin : IPlugin
    {
        public string Name { get; } = "YouTubeSitePlugin";
        public PluginId Id { get; } = new PluginId(Guid.NewGuid());
        public PluginTypeSite PluginType { get; } = new PluginTypeSite(new SiteId(Guid.NewGuid()), "YouTubeLive");
        public Props GetProps()
        {
            return Props.Create(() => new YouTubeSitePluginActor(Id, PluginType));
        }
    }
    class YouTubeSitePluginActor : ReceiveActor
    {
        private readonly Dictionary<ConnectionId, IActorRef> _connDict = new();
        private readonly PluginId _pluginId;
        private readonly PluginTypeSite _pluginType;
        private readonly YouTubeLiveSettings _settings = new YouTubeLiveSettings();
        private readonly IActorRef _core;
        public YouTubeSitePluginActor(PluginId pluginId, PluginTypeSite pluginType)
        {
            _pluginId = pluginId;
            _pluginType = pluginType;
            _core = Context.Parent;
            Receive<ToPlugin.Hello>(m =>
            {
                var types = new List<IPluginType>
                {
                    pluginType,
                };
                _core.Tell(new ToCore.Hello(_pluginId, types));
            });
            Receive<ToPlugin.RequestOpenConnectionMessage>(m =>
            {
                var conn = Context.ActorOf<YouTubeConnectionActor>();
                _connDict.Add(m.ConnId, conn);

            });
            Receive<ToPlugin.RequestCloseConnectionMessage>(m =>
            {
                _connDict.Remove(m.ConnId);
            });
            Receive<ToPlugin.RequestStartConnectionMessage>(m =>
            {
                var conn = _connDict[m.ConnId];
                conn.Tell(new StartMessage(m.Input), Self);
            });
            Receive<ToPlugin.RequestStopConnectionMessage>(m =>
            {
                var conn = _connDict[m.ConnId];

            });
            Receive<AskCookiesMessage>(m =>
            {
                Sender.Tell(new AnswerCookiesMessage(new List<Cookie>()));
            });
            Receive<MessageReceived>(m =>
            {
                //ConnectionActor経由で送ってきてもらわないとConnectionIdが分からない。
                var connId = GetConnectionId(Sender);
                if (connId == null) return;

                var message = SiteMessageImplFactory.Parse(m.Message);
                if (message != null)
                {
                    Context.Parent.Tell(new ToCore.NotifyMessageReceived(_pluginId, connId, message));
                }
            });
            ReceiveAsync<ToPlugin.AskPluginSettings>(async m =>
            {
                Sender.Tell(new ToCore.AnswerPluginSettings(_settings));
            });
            Receive<ToPlugin.RequestUpdateSettings>(m =>
            {
                if(m.Modified is not IYouTubeLiveSettingsDiff ytDiff)
                {
                    return;
                }
                _settings.Set(ytDiff);
                //Coreに保存要求
                _core.Tell(new ToCore.RequestSaveSettings("YouTubeSitePlugin", _settings.Serialize()));
            });
        }

        private ConnectionId? GetConnectionId(IActorRef actor)
        {
            foreach (var kv in _connDict)
            {
                if (kv.Value == actor)
                {
                    return kv.Key;
                }
            }
            return null;
        }
        private void AddConnection()
        {

        }
    }
    class StartMessage
    {
        public StartMessage(string input)
        {
            Input = input;
        }

        public string Input { get; }
    }
    class StopMessage { }
    class AskCookiesMessage
    {
        public AskCookiesMessage(string domain)
        {
            Domain = domain;
        }

        public string Domain { get; }
    }
    class AnswerCookiesMessage
    {
        public AnswerCookiesMessage(List<Cookie> cookies)
        {
            Cookies = cookies;
        }

        public List<Cookie> Cookies { get; }
    }
    class AskGetChatMessage
    {
        public AskGetChatMessage(Vid vid)
        {
            Vid = vid;
        }

        public Vid Vid { get; }
    }
    class AnswerGetChatMessage
    {
        public AnswerGetChatMessage(LiveChat liveChat)
        {
            LiveChat = liveChat;
        }

        public LiveChat LiveChat { get; }
    }
    class ActionWithInterval
    {
        public ActionWithInterval(IAction action, int interval)
        {
            Action = action;
            Interval = interval;
        }

        public IAction Action { get; }
        public int Interval { get; }
    }
    class AddActionsMessage
    {
        public AddActionsMessage(List<ActionWithInterval> actions)
        {
            Actions = actions;
        }

        public List<ActionWithInterval> Actions { get; }
    }
    class SpreadMessage { }
    class NextMessage { }
    class MessageReceived
    {
        public MessageReceived(IAction message)
        {
            Message = message;
        }

        public IAction Message { get; }
    }
    class YouTubeLiveActionSpreaderActor : ReceiveActor
    {
        private readonly IActorRef _connActor;

        private List<ActionWithInterval> Items { get; } = new List<ActionWithInterval>();
        public YouTubeLiveActionSpreaderActor(IActorRef connActor)
        {
            Receive<AddActionsMessage>(m =>
            {
                Items.AddRange(m.Actions);
                Self.Tell(new SpreadMessage());
            });
            ReceiveAsync<SpreadMessage>(async m =>
            {
                if (Items.Count == 0)
                {
                    Context.Parent.Tell(new NextMessage());
                    return;
                }
                var item = Items[0];
                Items.RemoveAt(0);
                _connActor.Tell(new MessageReceived(item.Action));
                await Task.Delay(item.Interval);
                Self.Tell(new SpreadMessage());

                Debug.WriteLine($"YouTubeLiveActionSpreaderActor.ReceiveAsync.SpreadMessage Items.Count={Items.Count}");
            });
            _connActor = connActor;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    class YouTubeConnectionActor : ReceiveActor
    {
        private readonly IActorRef _getLiveChatActor;
        private readonly IActorRef _liveChatActor;
        private readonly IActorRef _actionSpreaderActor;
        private YtInitialData? _ytInitialData;
        private YtCfg? _ytCfg;
        public YouTubeConnectionActor()
        {
            _liveChatActor = Context.ActorOf<YouTubeLiveLiveChatActor>();
            _getLiveChatActor = Context.ActorOf<YouTubeLiveGetLiveChatActor>();
            _actionSpreaderActor = Context.ActorOf(Props.Create(() => new YouTubeLiveActionSpreaderActor(Self)));
            ReceiveAsync((Func<StartMessage, Task>)(async m =>
            {
                var input = m.Input;
                var vid = ExtractVid(input);
                if (vid == null)
                {
                    throw new Exception();
                }
                var cookies = await GetCookies("youtube.com");

                var liveChat = await GetLiveChat(vid);
                _ytInitialData = liveChat.YtInitialData;
                _ytCfg = liveChat.YtCfg;
                var actions = liveChat.YtInitialData.Actions;
                var continuation = liveChat.YtInitialData.Continuation;
                if (continuation == null)
                {
                    throw new Exception("no continuation");
                }
                if (continuation is TimedContinuationData timed)
                {
                    var eachInterval = timed.TimeoutMs / actions.Count;
                    var ac = actions.Select(a => new ActionWithInterval(a, eachInterval)).ToList();
                    _actionSpreaderActor.Tell(new AddActionsMessage(ac));
                }
                else if (continuation is InvalidationContinuationData invalid)
                {
                    var eachInterval = invalid.TimeoutMs / actions.Count;
                    var ac = actions.Select(a => new ActionWithInterval(a, eachInterval)).ToList();
                    _actionSpreaderActor.Tell(new AddActionsMessage(ac));
                }
                else
                {
                    throw new NotImplementedException();
                }

            }));
            Receive<StopMessage>(m =>
            {

            });
            Receive<NextMessage>(m =>
            {
                if (_ytCfg == null) return;
                if (_ytInitialData == null) return;
                if (_ytInitialData.Continuation == null) return;

                _getLiveChatActor.Tell(new RequestNextGetLiveChatMessage(_ytCfg.InnertubeContext, _ytInitialData.Continuation, _ytCfg.InnertubeApiKey));
            });
            Receive<MessageReceived>(m =>
            {
                Context.Parent.Tell(m, Self);
            });
        }

        private async Task<LiveChat> GetLiveChat(Vid vid)
        {
            var liveChatAnswer = await _liveChatActor.Ask<AnswerGetChatMessage>(new AskGetChatMessage(vid));
            var liveChat = liveChatAnswer.LiveChat;
            return liveChat;
        }

        protected override void Unhandled(object message)
        {
            Debug.WriteLine(message);
        }


        private async Task<List<Cookie>> GetCookies(string domain)
        {
            var ans = await Context.Parent.Ask<AnswerCookiesMessage>(new AskCookiesMessage(domain));
            return ans.Cookies;
        }
        private Vid? ExtractVid(string input)
        {
            var match = Regex.Match(input, "v=([^?&%$/]+)");
            if (!match.Success) return null;
            var vid = match.Groups[1].Value;
            return new Vid(vid);
        }
    }
    class RequestGetLiveChatMessage
    {
        public RequestGetLiveChatMessage(YtCfg ytCfg, YtInitialData ytInitialData)
        {
            YtCfg = ytCfg;
            YtInitialData = ytInitialData;
        }

        public YtCfg YtCfg { get; }
        public YtInitialData YtInitialData { get; }
    }
    class RequestNextGetLiveChatMessage
    {
        public string InnertubeContext { get; }
        public IContinuation Continuation { get; }
        public string InnertubeApiKey { get; }
        public RequestNextGetLiveChatMessage(string innerTubeContext, IContinuation continuation, string innerTubeApiKey)
        {
            InnertubeContext = innerTubeContext;
            Continuation = continuation;
            InnertubeApiKey = innerTubeApiKey;
        }
    }
    class YouTubeLiveGetLiveChatActor : ReceiveActor
    {
        private readonly IActorRef _actionSpreaderActor;
        public YouTubeLiveGetLiveChatActor()
        {
            _actionSpreaderActor = Context.ActorOf(Props.Create(() => new YouTubeLiveActionSpreaderActor(Context.Parent)));
            //ReceiveAsync<RequestGetLiveChatMessage>(async m =>
            //{
            //    if (m.YtInitialData.Continuation == null)
            //    {
            //        return;
            //    }
            //    var a = CreateA(m.YtCfg.InnertubeContext, GetContinuation(m.YtInitialData.Continuation));
            //    var url = $"https://www.youtube.com/youtubei/v1/live_chat/get_live_chat?key={m.YtCfg.InnertubeApiKey}";
            //    var raw = await HttpPost(url, a);
            //    var getLiveChat = GetLiveChat.Parse(raw);

            //    var actions = getLiveChat.Actions;
            //    var nextContinuation = getLiveChat.Continuation;
            //    {
            //        if (nextContinuation is TimedContinuationData timed)
            //        {
            //            var eachInterval = timed.TimeoutMs / actions.Count;
            //            var ac = actions.Select(a => new ActionWithInterval(a, eachInterval)).ToList();
            //            _actionSpreaderActor.Tell(new AddActionsMessage(ac));
            //        }
            //        else if (nextContinuation is InvalidationContinuationData invalid)
            //        {
            //            var eachInterval = invalid.TimeoutMs / actions.Count;
            //            var ac = actions.Select(a => new ActionWithInterval(a, eachInterval)).ToList();
            //            _actionSpreaderActor.Tell(new AddActionsMessage(ac));
            //        }
            //        else
            //        {
            //            throw new NotImplementedException();
            //        }
            //    }
            //    {
            //        if (nextContinuation is TimedContinuationData timed)
            //        {
            //            await Task.Delay(timed.TimeoutMs);
            //        }
            //        else if (nextContinuation is InvalidationContinuationData invalid)
            //        {
            //            await Task.Delay(invalid.TimeoutMs);
            //        }
            //        else
            //        {
            //            throw new NotImplementedException();
            //        }
            //    }
            //    Self.Tell(new RequestNextGetLiveChatMessage(m.YtCfg.InnertubeContext, nextContinuation, m.YtCfg.InnertubeApiKey));
            //});
            ReceiveAsync<RequestNextGetLiveChatMessage>(async m =>
            {
                var a = CreateA(m.InnertubeContext, GetContinuation(m.Continuation));
                var url = $"https://www.youtube.com/youtubei/v1/live_chat/get_live_chat?key={m.InnertubeApiKey}";
                var raw = await HttpPost(url, a);
                var getLiveChat = GetLiveChat.Parse(raw);

                var actions = getLiveChat.Actions;
                var nextContinuation = getLiveChat.Continuation;
                if (nextContinuation == null)
                {
                    //配信終了を上流に通知
                    return;
                }
                {
                    if (nextContinuation is TimedContinuationData timed)
                    {
                        if (actions.Count == 0)
                        {
                            await Task.Delay(timed.TimeoutMs);
                        }
                        else
                        {
                            var eachInterval = timed.TimeoutMs / actions.Count;
                            var ac = actions.Select(a => new ActionWithInterval(a, eachInterval)).ToList();
                            _actionSpreaderActor.Tell(new AddActionsMessage(ac));
                        }
                    }
                    else if (nextContinuation is InvalidationContinuationData invalid)
                    {
                        if (actions.Count == 0)
                        {
                            await Task.Delay(invalid.TimeoutMs);
                        }
                        else
                        {
                            var eachInterval = invalid.TimeoutMs / actions.Count;
                            var ac = actions.Select(a => new ActionWithInterval(a, eachInterval)).ToList();
                            _actionSpreaderActor.Tell(new AddActionsMessage(ac));
                        }
                    }
                    else if (nextContinuation is ReloadContinuationData reload)
                    {
                        //reloadを上流に通知
                        return;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                {
                    if (nextContinuation is TimedContinuationData timed)
                    {
                        await Task.Delay(timed.TimeoutMs);
                    }
                    else if (nextContinuation is InvalidationContinuationData invalid)
                    {
                        await Task.Delay(invalid.TimeoutMs);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                Self.Tell(new RequestNextGetLiveChatMessage(m.InnertubeContext, nextContinuation, m.InnertubeApiKey));
            });
        }
        private async Task<string> HttpPost(string url, string payload)
        {
            var client = new HttpClient();
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var res = await client.PostAsync(url, content);
            var s = await res.Content.ReadAsStringAsync();
            return s;
        }
        private string CreateA(string innnerTubeContext, string continuation)
        {
            dynamic a = JsonConvert.DeserializeObject("{\"context\":{},\"continuation\":\"dummy\"}")!;
            dynamic b = JsonConvert.DeserializeObject(innnerTubeContext)!;
            a.context = b;
            a.continuation = continuation;
            var s = (string)a.ToString(Formatting.None);
            return s;
        }
        private string GetContinuation(IContinuation continuation)
        {
            if (continuation is TimedContinuationData timed)
            {
                return timed.Continaution;
            }
            else if (continuation is InvalidationContinuationData invalid)
            {
                return invalid.Continaution;
            }
            else
            {
                throw new Exception("");
            }
        }
    }
    class YouTubeLiveLiveChatActor : ReceiveActor
    {
        public YouTubeLiveLiveChatActor()
        {
            ReceiveAsync<AskGetChatMessage>(async m =>
            {
                var vid = m.Vid.Raw;
                var url = $"https://www.youtube.com/live_chat?v={vid}&is_popout=1";
                var ua = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.107 Safari/537.36";
                var html = await HttpGet(url, ua);
                var liveChat = ParseLiveChat(html);
                Sender.Tell(new AnswerGetChatMessage(liveChat));
            });
        }
        private async Task<string> HttpGet(string url)
        {
            var client = new HttpClient();
            var res = await client.GetStringAsync(url);
            return res;
        }
        private async Task<string> HttpGet(string url, string userAgent)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            var res = await client.GetStringAsync(url);
            return res;
        }
        private LiveChat ParseLiveChat(string html)
        {
            return LiveChat.Parse(html);
        }
    }
    class Vid
    {
        public Vid(string raw)
        {
            Raw = raw;
        }

        public string Raw { get; }
    }

}
