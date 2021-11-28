using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcv.Plugin.A17Live.InternalMessage;

interface IInternalMessage { }
class UnknownMessage : IInternalMessage
{
    public UnknownMessage(string raw)
    {
        Raw = raw;
    }

    public string Raw { get; }
    public static UnknownMessage Create(string raw)
    {
        return new UnknownMessage(raw);
    }
}
internal class Heartbeat : IInternalMessage
{
    internal static Heartbeat Parse(dynamic _)
    {
        return new Heartbeat();
    }
}
internal class Connected : IInternalMessage
{
    internal static Connected Parse(dynamic _)
    {
        return new Connected();
    }
}
internal class Attached : IInternalMessage
{
    internal static Attached Parse(dynamic _)
    {
        return new Attached();
    }
}
internal class Attach : IInternalMessage
{
    private readonly string _channel;

    public Attach(string channel)
    {
        _channel = channel;
    }
    public string Raw => $"{{\"action\":10,\"channel\":\"{_channel}\"}}";
}
internal class Comment : IInternalMessage
{
    public string Id { get; private set; }
    public long SendTime { get; private set; }
    public string Text { get; private set; }

    internal static Comment Parse(dynamic d, string id)
    {
        var sendTime = (long)d.sendTime;
        var level = (int)d.level;
        var name = (string)d.name.text;
        var text = (string)d.comment.text;
        var content = (string)d.content;
        if (text != content)
        {
            Debugger.Break();
        }
        return new Comment
        {
            Id = id,
            SendTime = sendTime,
            Text = text,
        };
    }
}
internal class LiveStreamEnd : IInternalMessage
{
    internal static LiveStreamEnd Parse(dynamic _)
    {
        return new LiveStreamEnd();
    }
}
internal class LiveStreamInfoChange : IInternalMessage
{
    internal static LiveStreamInfoChange Parse(dynamic _)
    {
        return new LiveStreamInfoChange
        {
        };
    }
}
internal class NewGift : IInternalMessage
{
    public string UserDisplayName { get; private set; }
    public string UserId { get; private set; }
    public string UserPicture { get; private set; }
    public string GiftId { get; private set; }
    public string GiftToken { get; private set; }

    internal static NewGift Parse(dynamic d)
    {
        //{"displayUser":{"userID":"fdc5f09e-6719-4906-8c02-4d31c625362b","displayName":"yuuki.hikaru","picture":"39452615-F71F-4556-835C-F600B4E00DE9.jpg","level":39,"isVIP":false,"isGuardian":false,"checkinLevel":1,"producer":0,"program":0,"badgeURL":"","pfxBadgeURL":"","topRightIconURL":"","bgColor":"","fgColor":"","circleBadgeURL":"","armyRank":0,"hasProgram":false,"isProducer":false,"isStreamer":false,"isDirty":false,"isDirtyUser":false,"mLevel":0,"checkinBdgURL":"","checkinCmtURL":"","gloryroadMode":0,"gloryroadInfo":{"point":172,"level":6,"iconURL":"https://assets-17app.akamaized.net/61104704-7a10-44ab-aa40-8653dc2bbcee.png","badgeIconURL":"https://assets-17app.akamaized.net/705f9dc1-f596-4d2d-80f5-77ef7c561af5.png"}},"giftID":"1811_jp_dailymission2_1","avatar":null,"isSendAll":false,"point":0,"giftToken":1267987562,"extID":"","giftIDs":["1811_jp_dailymission2_1"],"giftMetas":[{"targetGiftID":"1811_jp_dailymission2_1","luckyBag":null,"slot":null,"combo":{"count":1,"validDurationMs":3000,"animationResourceID":"","animationLevel":0},"poke":null,"poll":null,"barrage":{"showBarrage":false},"levelUp":null,"handDrawSticker":null,"luckyDraw":null,"texture":null,"layout":null,"game":null}]}
        var userId = (string)d.displayUser.userID;
        var userDisplayName = (string)d.displayUser.displayName;
        var userPicture = (string)d.displayUser.picture;
        var giftId = (string)d.giftID;
        var giftToken = (string)d.giftToken;

        if ((int)d.giftIDs.Count > 1)
        {
            Debugger.Break();
        }

        return new NewGift
        {
            UserDisplayName = userDisplayName,
            UserId = userId,
            UserPicture = userPicture,
            GiftId = giftId,
            GiftToken = giftToken,
        };
    }
}
internal class SubscriberEnterLive : IInternalMessage
{
    internal static SubscriberEnterLive Parse(dynamic d)
    {
        //{"displayName":"はせやす","picture":"9329e535-89ad-4652-969f-313c9d91b46d.jpg","animation":7,"mLevel":0,"eventNotifMsg":null,"userID":"b9ba1a7d-e744-4f65-a131-37ef718524b1","level":23}
        return new SubscriberEnterLive
        {
        };
    }
}
internal class NewLuckyBag : IInternalMessage
{
    internal static NewLuckyBag Parse(dynamic d)
    {
        //{"displayUser":{"userID":"b4c72157-7f4a-4c58-a3ea-f6e0934bebda","displayName":"ボン1644","picture":"59683738-2612-4088-88E7-FE6B47204903.jpg","level":10,"isVIP":false,"isGuardian":false,"checkinLevel":4,"producer":0,"program":0,"badgeURL":"","pfxBadgeURL":"","topRightIconURL":"","bgColor":"","fgColor":"","circleBadgeURL":"","armyRank":0,"hasProgram":false,"isProducer":false,"isStreamer":false,"isDirty":false,"isDirtyUser":false,"mLevel":0,"checkinBdgURL":"https://assets-17app.akamaized.net/dcdd5cc2-b4e7-4ff0-be76-1fc4be5c051b.png","checkinCmtURL":"http://cdn.17app.co/b08c44a4-0c7d-4999-ae7f-362907ca6888.png","gloryroadMode":0,"gloryroadInfo":null},"giftID":"2108_jp_menuaug_1","avatar":null,"isSendAll":false,"point":1,"giftToken":1266481246,"extID":"2108_jp_menuaug_bag","giftIDs":null,"giftMetas":[{"targetGiftID":"2108_jp_menuaug_1","luckyBag":{"triggerGiftID":"2108_jp_menuaug_bag","drewGiftID":"2108_jp_menuaug_1","forceDisplay":false},"slot":null,"combo":null,"poke":null,"poll":null,"barrage":{"showBarrage":false},"levelUp":null,"handDrawSticker":null,"luckyDraw":null,"texture":null,"layout":null,"game":null}]}
        return new NewLuckyBag
        {
        };
    }
}
internal class ReactMsg : IInternalMessage
{
    internal static ReactMsg Parse(dynamic d)
    {
        //{"type":1,"streamerType":0,"level":75,"openID":"おでーぶ","gloryroadMode":0,"gloryroadInfo":{"point":153,"level":5,"iconURL":"https://assets-17app.akamaized.net/56eaeb9d-e16a-4389-b3f7-ef39a51313a9.png","badgeIconURL":"https://assets-17app.akamaized.net/bbfdb79a-c242-41c9-b80c-bb420dc903e4.png"},"displayUser":{"userID":"10925d6e-dbd2-42bb-7a69-84499cf4c7ca","displayName":"おでーぶ","picture":"188ba3bf-aba7-4df5-ac94-837832bfbf02.jpg","level":75,"isVIP":false,"isGuardian":false,"checkinLevel":1,"producer":0,"program":0,"badgeURL":"","pfxBadgeURL":"https://cdn.17app.co/bca7b5d0-58e8-407a-8e1c-6bed556c51b9.png","topRightIconURL":"","bgColor":"","fgColor":"","circleBadgeURL":"","armyRank":0,"hasProgram":false,"isProducer":false,"isStreamer":false,"isDirty":false,"isDirtyUser":false,"mLevel":0,"checkinBdgURL":"","checkinCmtURL":"","gloryroadMode":0,"gloryroadInfo":{"point":153,"level":5,"iconURL":"https://assets-17app.akamaized.net/56eaeb9d-e16a-4389-b3f7-ef39a51313a9.png","badgeIconURL":"https://assets-17app.akamaized.net/bbfdb79a-c242-41c9-b80c-bb420dc903e4.png"}}}
        //{"type":2,"streamerType":0,"level":43,"openID":"taloon0123","gloryroadMode":0,"gloryroadInfo":{"point":361,"level":8,"iconURL":"https://assets-17app.akamaized.net/036773b3-d58f-4dad-a65c-ad1c64b1c658.png","badgeIconURL":"https://assets-17app.akamaized.net/309c20c1-5365-435f-a8b0-c93694fd5ae7.png"},"displayUser":{"userID":"000a8a2a-c394-4d6c-b729-18566377ca28","displayName":"taloon0123","picture":"f5c42f0e-172b-495d-b4f4-7fc595459634.jpg","level":43,"isVIP":false,"isGuardian":false,"checkinLevel":3,"producer":0,"program":0,"badgeURL":"","pfxBadgeURL":"","topRightIconURL":"","bgColor":"","fgColor":"","circleBadgeURL":"","armyRank":0,"hasProgram":false,"isProducer":false,"isStreamer":false,"isDirty":false,"isDirtyUser":false,"mLevel":0,"checkinBdgURL":"https://assets-17app.akamaized.net/bccb5dcc-0b56-491b-b732-d50d8e2d78c6.png","checkinCmtURL":"http://cdn.17app.co/ee4041c4-2700-44ea-b931-13255d0f6ff6.png","gloryroadMode":0,"gloryroadInfo":{"point":361,"level":8,"iconURL":"https://assets-17app.akamaized.net/036773b3-d58f-4dad-a65c-ad1c64b1c658.png","badgeIconURL":"https://assets-17app.akamaized.net/309c20c1-5365-435f-a8b0-c93694fd5ae7.png"}}}
        //{"type":3,"streamerType":0,"level":5,"openID":"よしん","gloryroadMode":0,"gloryroadInfo":null,"displayUser":{"userID":"821259a3-6073-4210-8a0a-b11d4b69b867","displayName":"よしん","picture":"","level":5,"isVIP":false,"isGuardian":false,"checkinLevel":1,"producer":0,"program":0,"badgeURL":"","pfxBadgeURL":"","topRightIconURL":"","bgColor":"","fgColor":"","circleBadgeURL":"","armyRank":0,"hasProgram":false,"isProducer":false,"isStreamer":false,"isDirty":false,"isDirtyUser":false,"mLevel":0,"checkinBdgURL":"","checkinCmtURL":"","gloryroadMode":0,"gloryroadInfo":null}}

        var type = (int)d.type;
        if (type != 1 && type != 2 && type != 3)
        {
            Debugger.Break();
        }
        if (type == 2)
        {
            //ライバーにいいねしました
            var userId = (string)d.displayUser.userID;
            var displayName = (string)d.displayUser.displayName;

        }
        return new ReactMsg
        {
        };
    }
}
internal class Live : IInternalMessage
{
    internal static Live Parse(dynamic d)
    {
        //{"type":0,"mute":true,"giftRankOne":null,"liveViewerCount":0,"levelToGo":null,"commodity":null,"achievementValue":0}
        //{"type":1,"mute":false,"giftRankOne":null,"liveViewerCount":740,"levelToGo":null,"commodity":null,"achievementValue":745982}
        var type = (int)d.type;
        if ((int)type != 1)
        {
            Debugger.Break();
        }
        var liveViewerCount = (int)d.liveViewerCount;
        var achievementValue = (int)d.achievementValue;
        return new Live
        {
        };
    }
}
internal class Poke : IInternalMessage
{
    internal static Poke Parse(dynamic d)
    {

        return new Poke
        {
        };
    }
}
internal class MyArmyOverview : IInternalMessage
{
    internal static MyArmyOverview Parse(dynamic d)
    {
        //{"subscriberTeams":[{"rank":1,"members":[{"user":{"userID":"3aae6a98-56d8-416d-b64e-2cdafb4e7d4a","displayName":"いちごや♥️🌈🥂ゆうか","picture":"2c92f444-0ffb-4172-b423-12a2ef1fb7f4.jpg","name":"","level":56,"openID":"いちごや♥️🌈🥂ゆうか","region":"JP","gloryroadInfo":{"point":0,"level":1,"iconURL":"https://assets-17app.akamaized.net/97132e4b-eecd-4d42-88d2-d9824097c4f0.png","badgeIconURL":"https://assets-17app.akamaized.net/965c755e-e348-485a-bca4-60b5dd858c91.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":3212572,"seniority":60,"startTime":1632657929,"endTime":1640433929,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"f5b58f69-6c25-443b-8134-82df2b546bb9","displayName":"たぬ♥️🌈🥂ゆうゆう","picture":"421d9f26-1373-4e62-936c-bc202d9b76c2.jpg","name":"たぬ🏴‍☠️🦝🌚","level":52,"openID":"たぬ♥️🌈🥂ゆうゆう","region":"JP","gloryroadInfo":{"point":1,"level":1,"iconURL":"https://assets-17app.akamaized.net/97132e4b-eecd-4d42-88d2-d9824097c4f0.png","badgeIconURL":"https://assets-17app.akamaized.net/965c755e-e348-485a-bca4-60b5dd858c91.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":2468615,"seniority":94,"startTime":1629709916,"endTime":1640099599,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"a2ab88b4-a6d0-4fa4-a447-b4acd55e8c79","displayName":"かわパパ","picture":"E5D46CEF-D36F-4915-939C-8AAA5EA7BAF4.jpg","name":"城咲ひめか㊗️8月星イベ🌟総合4位❣️","level":81,"openID":"かわパパ","region":"JP","gloryroadInfo":{"point":17,"level":2,"iconURL":"https://assets-17app.akamaized.net/65303647-0004-46ef-b246-4a1672270cf4.png","badgeIconURL":"https://assets-17app.akamaized.net/f93ec482-d3e2-49a1-975b-03d1fcc4df6a.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":1300966,"seniority":61,"startTime":1632624936,"endTime":1650781523,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"30eab059-8fc4-4c91-8d1a-f124012754a4","displayName":"zaki✨ザキ🌻✨non","picture":"FDA97C1B-9B80-44EB-99CB-3812807AD9D1.jpg","name":"女神❤️ non ✨のんたん🌻✨noﾉ","level":58,"openID":"zaki✨ザキ🌻✨non","region":"JP","gloryroadInfo":{"point":9,"level":1,"iconURL":"https://assets-17app.akamaized.net/97132e4b-eecd-4d42-88d2-d9824097c4f0.png","badgeIconURL":"https://assets-17app.akamaized.net/965c755e-e348-485a-bca4-60b5dd858c91.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":1056646,"seniority":104,"startTime":1628869158,"endTime":1644421158,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"ad07ecf2-3f20-49f4-b4c6-1b210fbe0218","displayName":"圧あつ❤️🌈🥂ゆうか","picture":"dae2c4b2-437c-476d-bc6c-8ef7032465c8.jpg","name":"楽しむ❤🌈🥂","level":42,"openID":"圧あつ❤️🌈🥂ゆうか","region":"JP","gloryroadInfo":{"point":20,"level":2,"iconURL":"https://assets-17app.akamaized.net/65303647-0004-46ef-b246-4a1672270cf4.png","badgeIconURL":"https://assets-17app.akamaized.net/f93ec482-d3e2-49a1-975b-03d1fcc4df6a.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":954339,"seniority":37,"startTime":1634681364,"endTime":1639882805,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"90137220-f7d2-4e8f-9a99-52b3bc3d57bc","displayName":"ふみくん✈️🌈🌴🌺","picture":"31562BB9-45FD-4D2B-8CC5-1C59BEA7B0D3.jpg","name":"大切にしてくれる人を応援します！","level":50,"openID":"ふみくん✈️🌈🌴🌺","region":"JP","gloryroadInfo":{"point":100,"level":5,"iconURL":"https://assets-17app.akamaized.net/56eaeb9d-e16a-4389-b3f7-ef39a51313a9.png","badgeIconURL":"https://assets-17app.akamaized.net/bbfdb79a-c242-41c9-b80c-bb420dc903e4.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":785052,"seniority":94,"startTime":1629741948,"endTime":1637864707,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"a9a8e4fa-a7d1-467b-a995-550af43c5586","displayName":"ツイスタ","picture":"6e2b25bf-a11e-4822-9a9c-b36169060a56.jpg","name":"🌪️時間を越えていけ🌪️","level":68,"openID":"ツイスタ","region":"JP","gloryroadInfo":{"point":36,"level":3,"iconURL":"https://assets-17app.akamaized.net/9e922b90-4c79-4aa6-94cb-860b059e27c5.png","badgeIconURL":"https://assets-17app.akamaized.net/5920a479-9fa7-4ef8-af6e-390577132b02.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":660381,"seniority":148,"startTime":1625065219,"endTime":1640617219,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"32ff023d-7a30-4aac-9bd4-7c722512e77b","displayName":"クロタカ❤️🌈🥂","picture":"deef94fa-5bf8-4eab-85f0-95541378509f.jpg","name":"おはよ～ごす💭🍈💕","level":48,"openID":"クロタカ❤️🌈🥂","region":"JP","gloryroadInfo":{"point":0,"level":1,"iconURL":"https://assets-17app.akamaized.net/97132e4b-eecd-4d42-88d2-d9824097c4f0.png","badgeIconURL":"https://assets-17app.akamaized.net/965c755e-e348-485a-bca4-60b5dd858c91.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":529612,"seniority":91,"startTime":1629986891,"endTime":1640362049,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"6a237974-a6de-44d1-923f-45f5df7de82b","displayName":"kenshi6623","picture":"14920740-c04c-450f-be83-3486cab6ee3e.jpg","name":"けんけんぼう","level":65,"openID":"kenshi6623","region":"JP","gloryroadInfo":{"point":6,"level":1,"iconURL":"https://assets-17app.akamaized.net/97132e4b-eecd-4d42-88d2-d9824097c4f0.png","badgeIconURL":"https://assets-17app.akamaized.net/965c755e-e348-485a-bca4-60b5dd858c91.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":266629,"seniority":47,"startTime":1633786553,"endTime":1644193368,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"971d0aed-5a32-4ddd-6e4c-012f7637fd57","displayName":"俊_優格君君","picture":"c4a89c40-d225-4bd9-aa13-df1dd8fbd949.jpg","name":"♥️🌈🥂","level":80,"openID":"俊_優格君君","region":"JP","gloryroadInfo":{"point":14,"level":2,"iconURL":"https://assets-17app.akamaized.net/65303647-0004-46ef-b246-4a1672270cf4.png","badgeIconURL":"https://assets-17app.akamaized.net/f93ec482-d3e2-49a1-975b-03d1fcc4df6a.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":203450,"seniority":146,"startTime":1625208068,"endTime":1640874424,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"b9ba1a7d-e744-4f65-a131-37ef718524b1","displayName":"はせやす","picture":"9329e535-89ad-4652-969f-313c9d91b46d.jpg","name":"","level":26,"openID":"はせやす","region":"JP","gloryroadInfo":{"point":0,"level":1,"iconURL":"https://assets-17app.akamaized.net/97132e4b-eecd-4d42-88d2-d9824097c4f0.png","badgeIconURL":"https://assets-17app.akamaized.net/965c755e-e348-485a-bca4-60b5dd858c91.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":162583,"seniority":35,"startTime":1634831396,"endTime":1640015396,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"e76ffeca-ff19-4367-beac-710bb8935107","displayName":"🌈🏝🎨もと🎮🎯🏖","picture":"D6DA8FDE-1BBA-4B73-8379-6E1DAF86C8F1.jpg","name":"","level":31,"openID":"🌈🏝🎨もと🎮🎯🏖","region":"JP","gloryroadInfo":{"point":0,"level":1,"iconURL":"https://assets-17app.akamaized.net/97132e4b-eecd-4d42-88d2-d9824097c4f0.png","badgeIconURL":"https://assets-17app.akamaized.net/965c755e-e348-485a-bca4-60b5dd858c91.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":97269,"seniority":23,"startTime":1635853021,"endTime":1638445021,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"df3aa4e8-477e-47fc-93a0-9703bfc80033","displayName":"まさまさ1122","picture":"fd5d1f8a-0353-4303-90bf-b4c3c38ca8bd.jpg","name":"","level":21,"openID":"まさまさ1122","region":"JP","gloryroadInfo":{"point":0,"level":1,"iconURL":"https://assets-17app.akamaized.net/97132e4b-eecd-4d42-88d2-d9824097c4f0.png","badgeIconURL":"https://assets-17app.akamaized.net/965c755e-e348-485a-bca4-60b5dd858c91.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":65861,"seniority":85,"startTime":1630518164,"endTime":1638713668,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"5ff1c811-fadd-4179-552d-9a73c5e04583","displayName":"ちゃと🚶‍♂️","picture":"6465CB09-21C2-4835-AE6B-ACF3DA2851A7.jpg","name":"","level":53,"openID":"ちゃと🚶‍♂️","region":"JP","gloryroadInfo":{"point":18,"level":2,"iconURL":"https://assets-17app.akamaized.net/65303647-0004-46ef-b246-4a1672270cf4.png","badgeIconURL":"https://assets-17app.akamaized.net/f93ec482-d3e2-49a1-975b-03d1fcc4df6a.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":63527,"seniority":53,"startTime":1633280317,"endTime":1638464317,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"84078245-cea8-42b3-85ef-752f3d982ff2","displayName":"KuruKuruまわる","picture":"F2288253-CCC7-4D17-8F7F-2D5F251940F6.jpg","name":"","level":23,"openID":"KuruKuruまわる","region":"JP","gloryroadInfo":{"point":0,"level":1,"iconURL":"https://assets-17app.akamaized.net/97132e4b-eecd-4d42-88d2-d9824097c4f0.png","badgeIconURL":"https://assets-17app.akamaized.net/965c755e-e348-485a-bca4-60b5dd858c91.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":45832,"seniority":37,"startTime":1634654715,"endTime":1642430715,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"88cff38a-5d41-418c-86b8-1ae3232af691","displayName":"俊_さとし","picture":"81aa78ed-b34b-410b-92b7-6f8c2ea9175d.jpg","name":"17辞めます","level":31,"openID":"俊_さとし","region":"JP","gloryroadInfo":{"point":122,"level":5,"iconURL":"https://assets-17app.akamaized.net/56eaeb9d-e16a-4389-b3f7-ef39a51313a9.png","badgeIconURL":"https://assets-17app.akamaized.net/bbfdb79a-c242-41c9-b80c-bb420dc903e4.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":44066,"seniority":85,"startTime":1630505079,"endTime":1638281079,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"701815ae-fd65-4ffc-aac5-8a7b42a9e07c","displayName":"おぱんつ😈🍽はるな🏩","picture":"5F7E1992-3714-491D-8765-378A30C7660D.jpg","name":"マイイベ開催中♛おぱん亭喫茶12月開催予定","level":59,"openID":"おぱんつ😈🍽はるな🏩","region":"JP","gloryroadInfo":{"point":9439,"level":19,"iconURL":"https://assets-17app.akamaized.net/c790e3fe-642a-45c1-b2b5-6578af78da9d.png","badgeIconURL":"https://assets-17app.akamaized.net/8c1f1a2e-9478-45ce-afef-a5ddc68fe00f.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":32173,"seniority":26,"startTime":1635566183,"endTime":1640750183,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}},{"user":{"userID":"012b53f6-a0b5-440f-b8e1-86e73cab4ae9","displayName":"🐱きょー🐆kyou🍓🍒","picture":"75eae69f-33c5-4327-b555-e86a07f97875.jpg","name":"","level":54,"openID":"🐱きょー🐆kyou🍓🍒","region":"JP","gloryroadInfo":{"point":49,"level":3,"iconURL":"https://assets-17app.akamaized.net/9e922b90-4c79-4aa6-94cb-860b059e27c5.png","badgeIconURL":"https://assets-17app.akamaized.net/5920a479-9fa7-4ef8-af6e-390577132b02.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":18659,"seniority":27,"startTime":1635522639,"endTime":1638114639,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}}]},{"rank":2,"members":[{"user":{"userID":"82409309-10ff-4323-ac89-4555fbd006b2","displayName":"なななな029♥️🌈🥂","picture":"6f0526ca-da55-41b2-a0ab-afebcffacf1b.jpg","name":"","level":41,"openID":"なななな029♥️🌈🥂","region":"JP","gloryroadInfo":{"point":0,"level":1,"iconURL":"https://assets-17app.akamaized.net/97132e4b-eecd-4d42-88d2-d9824097c4f0.png","badgeIconURL":"https://assets-17app.akamaized.net/965c755e-e348-485a-bca4-60b5dd858c91.png"},"gloryroadMode":0,"onliveInfo":null},"rank":2,"pointContribution":935141,"seniority":60,"startTime":1632664441,"endTime":1637863883,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}}]},{"rank":3,"members":[{"user":{"userID":"12a0de46-8375-4da5-ab6f-f5c085931cb7","displayName":"ゆうき♥️🌈🥂ゆうか","picture":"33011ead-81d7-4c5c-911f-0f88ea4d74f5.jpg","name":"大切にしてくれる人を大切にしたい","level":59,"openID":"ゆうき♥️🌈🥂ゆうか","region":"JP","gloryroadInfo":{"point":0,"level":1,"iconURL":"https://assets-17app.akamaized.net/97132e4b-eecd-4d42-88d2-d9824097c4f0.png","badgeIconURL":"https://assets-17app.akamaized.net/965c755e-e348-485a-bca4-60b5dd858c91.png"},"gloryroadMode":0,"onliveInfo":null},"rank":3,"pointContribution":4108538,"seniority":90,"startTime":1630048456,"endTime":1639812048,"isOnLive":false,"anonymousInfo":{"isInvisible":false,"pureText":""}}]}],"subscribedUsers":[{"user":{"userID":"701815ae-fd65-4ffc-aac5-8a7b42a9e07c","displayName":"おぱんつ😈🍽はるな🏩","picture":"5F7E1992-3714-491D-8765-378A30C7660D.jpg","name":"マイイベ開催中♛おぱん亭喫茶12月開催予定","level":59,"openID":"おぱんつ😈🍽はるな🏩","region":"JP","gloryroadInfo":{"point":9439,"level":19,"iconURL":"https://assets-17app.akamaized.net/c790e3fe-642a-45c1-b2b5-6578af78da9d.png","badgeIconURL":"https://assets-17app.akamaized.net/8c1f1a2e-9478-45ce-afef-a5ddc68fe00f.png"},"gloryroadMode":0,"onliveInfo":null},"rank":1,"pointContribution":190059,"seniority":80,"startTime":1630936647,"endTime":1638712647,"isOnLive":false,"anonymousInfo":null}],"level":6,"levelRules":[{"level":0,"bonus":0,"minMembers":0,"minCaptains":0,"minColonels":0,"minGenerals":0},{"level":1,"bonus":20000,"minMembers":1,"minCaptains":0,"minColonels":0,"minGenerals":0},{"level":2,"bonus":30000,"minMembers":2,"minCaptains":0,"minColonels":0,"minGenerals":0},{"level":3,"bonus":45000,"minMembers":3,"minCaptains":0,"minColonels":0,"minGenerals":0},{"level":4,"bonus":70000,"minMembers":5,"minCaptains":0,"minColonels":0,"minGenerals":0},{"level":5,"bonus":100000,"minMembers":10,"minCaptains":0,"minColonels":0,"minGenerals":0},{"level":6,"bonus":200000,"minMembers":20,"minCaptains":1,"minColonels":1,"minGenerals":0},{"level":7,"bonus":250000,"minMembers":30,"minCaptains":3,"minColonels":2,"minGenerals":1},{"level":8,"bonus":300000,"minMembers":50,"minCaptains":5,"minColonels":3,"minGenerals":2}]}
        return new MyArmyOverview
        {
        };
    }
}
internal class FreshUserEnter : IInternalMessage
{
    internal static FreshUserEnter Parse(dynamic d)
    {
        //{"user":{"userID":"bc83e4ce-e418-4602-93e9-1cb8c77feed3","displayName":"リュウ17374","picture":"","name":"","level":1,"openID":"リュウ17374","region":"JP","gloryroadInfo":{"point":0,"level":1,"iconURL":"https://assets-17app.akamaized.net/97132e4b-eecd-4d42-88d2-d9824097c4f0.png","badgeIconURL":"https://assets-17app.akamaized.net/965c755e-e348-485a-bca4-60b5dd858c91.png"},"gloryroadMode":0,"onliveInfo":null},"fgColor":"#FFFFFF","bgColor":"#b3f6696c","token":{"key":"streamer_newbie_poke","params":[{"value":"リュウ17374","needMap":false,"mapOption":0,"fontAttribute":null},{"value":"💜🍋秘書室のアン🍋💜","needMap":false,"mapOption":0,"fontAttribute":null}],"fontAttribute":null},"isStreamerOnly":false}
        return new FreshUserEnter
        {
        };
    }
}
internal class RockViewerUpdate : IInternalMessage
{
    internal static RockViewerUpdate Parse(dynamic _)
    {
        return new RockViewerUpdate();
    }
}
internal class LaborRewardStreamerReceive : IInternalMessage
{
    public int RewardType { get; init; }
    public int Value { get; init; }
    public string UserId { get; init; }
    public string UserDisplayName { get; init; }
    internal static LaborRewardStreamerReceive Parse(dynamic d)
    {
        //{"value":1,"userInfo":{"userID":"fdc5f09e-6719-4906-8c02-4d31c625362b","displayName":"yuuki.hikaru","picture":"39452615-F71F-4556-835C-F600B4E00DE9.jpg","name":"","level":39,"openID":"yuuki.hikaru","region":"JP","gloryroadInfo":{"point":172,"level":6,"iconURL":"https://assets-17app.akamaized.net/61104704-7a10-44ab-aa40-8653dc2bbcee.png","badgeIconURL":"https://assets-17app.akamaized.net/705f9dc1-f596-4d2d-80f5-77ef7c561af5.png"},"gloryroadMode":0,"onliveInfo":null},"rewardType":1}
        var value = (int)d.value;
        var rewardType = (int)d.rewardType;
        var userId = (string)d.userInfo.userID;
        var userDisplayName = (string)d.userInfo.displayName;

        return new LaborRewardStreamerReceive
        {
            Value = value,
            RewardType = rewardType,
            UserDisplayName = userDisplayName,
            UserId = userId,
        };
    }
}
internal class MissionRemind : IInternalMessage
{
    internal static MissionRemind Parse(dynamic _)
    {
        //{"targetUserID":"bc83e4ce-e418-4602-93e9-1cb8c77feed3","remindSecs":60,"toastMsg":{"key":"baggage_complete_toast","params":[],"fontAttribute":null},"toastSecs":10}
        return new MissionRemind();
    }
}
internal class MessageParser
{
    public static IInternalMessage Parse(string raw)
    {
        dynamic? d;
        try
        {
            d = JsonConvert.DeserializeObject(raw);
        }
        catch (JsonReaderException)
        {
            return UnknownMessage.Create(raw);
        }
        if (d == null)
        {
            return UnknownMessage.Create(raw);
        }
        if (!d.ContainsKey("action"))
        {
            return UnknownMessage.Create(raw);
        }
        var action = (int)d.action;

        IInternalMessage ret;
        try
        {
            ret = action switch
            {
                0 => Heartbeat.Parse(d),
                4 => Connected.Parse(d),
                11 => Attached.Parse(d),
                15 => ParseMessage(d),
                _ => UnknownMessage.Create(raw),
            };
        }
        catch (Exception)
        {
            ret = UnknownMessage.Create(raw);
        }
        return ret;
    }
    internal static IInternalMessage ParseMessage(dynamic json)
    {
        var id = (string)json.id;
        var connectionSerial = (int)json.connectionSerial;
        var channel = (string)json.channel;
        var timestamp = (long)json.timestamp;
        var data = (string)json.messages[0].data;
        var decodedData = DecodeData(data);

        dynamic d = JsonConvert.DeserializeObject(decodedData)!;
        var type = (int)d.type;
        return type switch
        {
            3 => Comment.Parse(d.commentMsg, id),
            5 => LiveStreamEnd.Parse(d),
            6 => LiveStreamInfoChange.Parse(d),
            13 => NewGift.Parse(d.giftMsg),
            27 => SubscriberEnterLive.Parse(d.subscriberEnterMsg),
            28 => ReactMsg.Parse(d.reactMsg),
            32 => NewLuckyBag.Parse(d.giftMsg),
            38 => Live.Parse(d.liveinfo),
            47 => Poke.Parse(d.pokeInfo),
            56 => MyArmyOverview.Parse(d.myArmyOverviewMsg),
            64 => FreshUserEnter.Parse(d.freshUserEnterMsg),
            74 => RockViewerUpdate.Parse(d),
            79 => LaborRewardStreamerReceive.Parse(d.laborReceiveRewardMsg),
            80 => MissionRemind.Parse(d.missionRemind),
            _ => throw new NotSupportedException(),
        };
    }
    static string DecodeData(string data)
    {
        var cc = Convert.FromBase64String(data);
        using var s = new GZipStream(new MemoryStream(cc), CompressionMode.Decompress);
        using var reader = new StreamReader(s);
        var k = reader.ReadToEnd();
        return k;
    }
}

