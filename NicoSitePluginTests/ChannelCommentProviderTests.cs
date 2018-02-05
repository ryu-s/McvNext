﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NicoSitePlugin.Test;
using Moq;
using System.Reflection;
using NUnit.Framework;
namespace NicoSitePluginTests
{
    [TestFixture]
    class ChannelCommentProviderTests
    {
        const string psHasMsList = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getplayerstatus status=\"ok\" time=\"1489873319\"><stream><id>lv293505973</id><title>テスト</title><description>ああ</description><provider_type>community</provider_type><default_community>co1034396</default_community><international>13</international><is_owner>1</is_owner><owner_id>2297426</owner_id><owner_name>Ryu</owner_name><is_reserved>0</is_reserved><is_niconico_enquete_enabled>1</is_niconico_enquete_enabled><watch_count>0</watch_count><comment_count>0</comment_count><base_time>1489873305</base_time><open_time>1489873305</open_time><start_time>1489873605</start_time><end_time>1489875405</end_time><is_rerun_stream>0</is_rerun_stream><bourbon_url>http://live.nicovideo.jp/gate/lv293505973?sec=nicolive_crowded&amp;sub=watch_crowded_0_community_lv293505973_onair</bourbon_url><full_video>http://live.nicovideo.jp/gate/lv293505973?sec=nicolive_crowded&amp;sub=watch_crowded_0_community_lv293505973_onair</full_video><after_video></after_video><before_video></before_video><kickout_video>http://live.nicovideo.jp/gate/lv293505973?sec=nicolive_oidashi&amp;sub=watchplayer_oidashialert_0_community_lv293505973_onair</kickout_video><twitter_tag>#co1034396</twitter_tag><danjo_comment_mode>0</danjo_comment_mode><infinity_mode>0</infinity_mode><archive>0</archive><press><display_lines>-1</display_lines><display_time>-1</display_time><style_conf></style_conf></press><plugin_delay></plugin_delay><plugin_url></plugin_url><plugin_urls/><allow_netduetto>0</allow_netduetto><broadcast_token>1b540c6a8f3a8df99fa08f0eb382d5d07d4f9b59</broadcast_token><ng_scoring>0</ng_scoring><is_nonarchive_timeshift_enabled>0</is_nonarchive_timeshift_enabled><is_timeshift_reserved>0</is_timeshift_reserved><header_comment>0</header_comment><footer_comment>0</footer_comment><split_bottom>0</split_bottom><split_top>0</split_top><background_comment>0</background_comment><font_scale></font_scale><comment_lock>0</comment_lock><telop><enable>0</enable></telop><contents_list><contents id=\"main\" disableAudio=\"0\" disableVideo=\"0\" start_time=\"1489873306\">rtmp:rtmp://nlpoca126.live.nicovideo.jp:1935/publicorigin/170319_06_0/,lv293505973?1489873318:30:334a723a2bc17c7f</contents></contents_list><picture_url>http://icon.nimg.jp/community/103/co1034396.jpg?1489263513</picture_url><thumb_url>http://icon.nimg.jp/community/s/103/co1034396.jpg?1489263513</thumb_url><is_priority_prefecture></is_priority_prefecture></stream><user><user_id>2297426</user_id><nickname>Ryu</nickname><is_premium>1</is_premium><userAge>28</userAge><userSex>1</userSex><userDomain>jp</userDomain><userPrefecture>14</userPrefecture><userLanguage>ja-jp</userLanguage><room_label>co1034396</room_label><room_seetno>2525786</room_seetno><is_join>1</is_join><twitter_info><status>enabled</status><screen_name>namazu2525</screen_name><followers_count>8990</followers_count><is_vip>0</is_vip><profile_image_url>http://pbs.twimg.com/profile_images/2150344550/20120420070739576_normal.jpg</profile_image_url><after_auth>0</after_auth><tweet_token>4502cf5ad39ff089ecca1acba0c6c2e2cee09114</tweet_token></twitter_info></user><rtmp is_fms=\"1\" rtmpt_port=\"80\"><url>rtmp://nleu14.live.nicovideo.jp:1935/liveedge/live_170319_06_10</url><ticket>2297426:lv293505973:4:1489873605:c88a84c54c4e32ee</ticket></rtmp><ms><addr>msg104.live.nicovideo.jp</addr><port>2839</port><thread>1585333588</thread></ms><tid_list><tid>1585333588</tid><tid>1585333589</tid></tid_list><ms_list><ms><addr>msg104.live.nicovideo.jp</addr><port>2839</port><thread>1585333588</thread></ms><ms><addr>msg105.live.nicovideo.jp</addr><port>2849</port><thread>1585333589</thread></ms></ms_list><twitter><live_enabled>1</live_enabled><vip_mode_count>10000</vip_mode_count><live_api_url>http://watch.live.nicovideo.jp/api/</live_api_url></twitter><player><qos_analytics>0</qos_analytics><dialog_image><oidashi>http://nicolive.cdn.nimg.jp/live/simg/img/201311/281696.a29344.png</oidashi></dialog_image><is_notice_viewer_balloon_enabled>1</is_notice_viewer_balloon_enabled><error_report>1</error_report></player><marquee><category>一般(その他)</category><game_key>2d9ea29f</game_key><game_time>1489873318</game_time><force_nicowari_off>1</force_nicowari_off></marquee></getplayerstatus>";
        const string playerStatusXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getplayerstatus status=\"ok\" time=\"1516809417\"><stream><id>lv310492598</id><title>【初見歓迎】生着替えからの腰痛改善筋トレ:;(∩︎´﹏`∩︎);:</title><description> 【Twitter】  https://twitter.com/siroiomotimain?lang=ja   【YouTube】  https://t.co/U4VDJ3ISR4    </description><provider_type>community</provider_type><default_community>co2810321</default_community><international>13</international><is_owner>0</is_owner><owner_id>34732619</owner_id><owner_name>おもちまいん（ぽんこつ）</owner_name><is_reserved>0</is_reserved><is_niconico_enquete_enabled>1</is_niconico_enquete_enabled><watch_count>5975</watch_count><comment_count>4818</comment_count><base_time>1516718536</base_time><open_time>1516718536</open_time><start_time>1516718650</start_time><end_time>1516725556</end_time><is_rerun_stream>0</is_rerun_stream><bourbon_url>http://live.nicovideo.jp/gate/lv310492598?sec=nicolive_crowded&amp;sub=watch_crowded_0_community_lv310492598_closed</bourbon_url><full_video>http://live.nicovideo.jp/gate/lv310492598?sec=nicolive_crowded&amp;sub=watch_crowded_0_community_lv310492598_closed</full_video><after_video></after_video><before_video></before_video><kickout_video>http://live.nicovideo.jp/gate/lv310492598?sec=nicolive_oidashi&amp;sub=watchplayer_oidashialert_0_community_lv310492598_closed</kickout_video><twitter_tag>#co2810321</twitter_tag><danjo_comment_mode>0</danjo_comment_mode><infinity_mode>0</infinity_mode><archive>1</archive><press><display_lines>-1</display_lines><display_time>-1</display_time><style_conf></style_conf></press><plugin_delay></plugin_delay><plugin_url></plugin_url><plugin_urls/><allow_netduetto>0</allow_netduetto><ng_scoring>0</ng_scoring><is_nonarchive_timeshift_enabled>1</is_nonarchive_timeshift_enabled><is_timeshift_reserved>0</is_timeshift_reserved><timeshift_time>1516718650</timeshift_time><quesheet><que vpos=\"0\" mail=\"\" name=\"\">/play rtmp:lv310492598 main</que><que vpos=\"11400\" mail=\"\" name=\"\">/publish lv310492598 rtmp://nlpoca109.live.nicovideo.jp:1935/fileorigin/ts_01,/content/20180123/lv310492598_234410555000_1_bda1ae.f4v?1516809417:30:48d35e8e4b63a79c</que><que vpos=\"12100\" mail=\"184\" name=\"\">/info 3 \"90分延長しました\"</que><que vpos=\"23900\" mail=\"184\" name=\"\">/info 8 \"第12位にランクインしました\"</que><que vpos=\"27200\" mail=\"black small\" name=\"\">/telop on &lt;a target='_blank' href='http://live.nicovideo.jp/watch/lv310453526'&gt;&lt;u&gt;ニコ生クルーズ&lt;/u&gt;&lt;/a&gt;</que><que vpos=\"28800\" mail=\"184\" name=\"\">/info 1 \"市場に下ネタとか恥ずかしくないの？が登録されました。\"</que><que vpos=\"30000\" mail=\"184\" name=\"\">/info 8 \"第8位にランクインしました\"</que><que vpos=\"31700\" mail=\"orange\" name=\"\">/telop perm ニコ生クルーズが去って行きました</que><que vpos=\"33200\" mail=\"\" name=\"\">/telop off</que><que vpos=\"35900\" mail=\"184\" name=\"\">/info 8 \"第6位にランクインしました\"</que><que vpos=\"40700\" mail=\"184\" name=\"\">/info 2 \"1人がコミュニティをフォローしました。\"</que><que vpos=\"41900\" mail=\"184\" name=\"\">/info 8 \"第3位にランクインしました\"</que><que vpos=\"46800\" mail=\"184\" name=\"\">/info 2 \"2人（プレミアム2人）がコミュニティをフォローしました。\"</que><que vpos=\"46800\" mail=\"184\" name=\"\">/info 1 \"市場に下ネタとか恥ずかしくないの？が登録されました。\"</que><que vpos=\"52800\" mail=\"184\" name=\"\">/info 1 \"市場に下ネタとか恥ずかしくないの？が登録されました。\"</que><que vpos=\"70800\" mail=\"184\" name=\"\">/info 2 \"2人（プレミアム1人）がコミュニティをフォローしました。\"</que><que vpos=\"98769\" mail=\"\" name=\"\">/vote start 筋トレにふさわしい服 お花 ハリネズミ うめずT(囚人) マッチョ ぷろれすT</que><que vpos=\"100800\" mail=\"184\" name=\"\">/info 2 \"2人（プレミアム1人）がコミュニティをフォローしました。\"</que><que vpos=\"106700\" mail=\"184\" name=\"\">/info 2 \"1人（プレミアム1人）がコミュニティをフォローしました。\"</que><que vpos=\"107706\" mail=\"\" name=\"\">/vote showresult per 212 207 140 279 162</que><que vpos=\"108888\" mail=\"\" name=\"\">/vote stop</que><que vpos=\"115500\" mail=\"184\" name=\"\">/uadpoint 310492598 1000</que><que vpos=\"118700\" mail=\"184\" name=\"\">/info 2 \"1人がコミュニティをフォローしました。\"</que><que vpos=\"142700\" mail=\"184\" name=\"\">/info 2 \"1人がコミュニティをフォローしました。\"</que><que vpos=\"166800\" mail=\"184\" name=\"\">/info 2 \"2人（プレミアム1人）がコミュニティをフォローしました。\"</que><que vpos=\"172800\" mail=\"184\" name=\"\">/info 2 \"2人（プレミアム1人）がコミュニティをフォローしました。\"</que><que vpos=\"180300\" mail=\"\" name=\"\">/publish lv310492598 rtmp://nlpoca109.live.nicovideo.jp:1935/fileorigin/ts_01,/content/20180123/lv310492598_234410555000_2_ad0002.f4v?1516809417:30:48d35e8e4b63a79c</que><que vpos=\"185500\" mail=\"184\" name=\"\">/uadpoint 310492598 1500</que><que vpos=\"190800\" mail=\"184\" name=\"\">/info 2 \"1人がコミュニティをフォローしました。\"</que><que vpos=\"198000\" mail=\"184\" name=\"\">/uadpoint 310492598 2300</que><que vpos=\"202800\" mail=\"184\" name=\"\">/info 2 \"1人（プレミアム1人）がコミュニティをフォローしました。\"</que><que vpos=\"203900\" mail=\"184\" name=\"\">/uadpoint 310492598 3300</que><que vpos=\"208800\" mail=\"184\" name=\"\">/info 2 \"1人がコミュニティをフォローしました。\"</que><que vpos=\"209900\" mail=\"184\" name=\"\">/info 8 \"第2位にランクインしました\"</que><que vpos=\"220700\" mail=\"184\" name=\"\">/info 2 \"1人がコミュニティをフォローしました。\"</que><que vpos=\"221700\" mail=\"184\" name=\"\">/uadpoint 310492598 3600</que><que vpos=\"226700\" mail=\"184\" name=\"\">/info 2 \"1人がコミュニティをフォローしました。\"</que><que vpos=\"227900\" mail=\"184\" name=\"\">/uadpoint 310492598 4100</que><que vpos=\"244800\" mail=\"184\" name=\"\">/info 2 \"1人がコミュニティをフォローしました。\"</que><que vpos=\"250700\" mail=\"184\" name=\"\">/info 2 \"1人がコミュニティをフォローしました。\"</que><que vpos=\"258100\" mail=\"184\" name=\"\">/uadpoint 310492598 6100</que><que vpos=\"262700\" mail=\"184\" name=\"\">/info 2 \"1人（プレミアム1人）がコミュニティをフォローしました。\"</que><que vpos=\"263600\" mail=\"184\" name=\"\">/info 8 \"第1位にランクインしました\"</que><que vpos=\"263600\" mail=\"184\" name=\"\">/uadpoint 310492598 6100</que><que vpos=\"269800\" mail=\"184\" name=\"\">/uadpoint 310492598 7100</que><que vpos=\"274700\" mail=\"184\" name=\"\">/info 2 \"1人（プレミアム1人）がコミュニティをフォローしました。\"</que><que vpos=\"276100\" mail=\"184\" name=\"\">/uadpoint 310492598 7600</que><que vpos=\"287600\" mail=\"184\" name=\"\">/uadpoint 310492598 8100</que><que vpos=\"292800\" mail=\"184\" name=\"\">/info 2 \"1人（プレミアム1人）がコミュニティをフォローしました。\"</que><que vpos=\"294700\" mail=\"184\" name=\"\">/uadpoint 310492598 8400</que><que vpos=\"305600\" mail=\"184\" name=\"\">/uadpoint 310492598 8900</que><que vpos=\"340800\" mail=\"184\" name=\"\">/info 2 \"1人がコミュニティをフォローしました。\"</que><que vpos=\"352800\" mail=\"184\" name=\"\">/info 1 \"市場に下ネタとか恥ずかしくないの？が登録されました。\"</que><que vpos=\"382800\" mail=\"184\" name=\"\">/info 2 \"1人（プレミアム1人）がコミュニティをフォローしました。\"</que><que vpos=\"394700\" mail=\"184\" name=\"\">/info 2 \"1人（プレミアム1人）がコミュニティをフォローしました。\"</que><que vpos=\"406700\" mail=\"184\" name=\"\">/info 2 \"1人がコミュニティをフォローしました。\"</que><que vpos=\"436700\" mail=\"184\" name=\"\">/info 2 \"1人（プレミアム1人）がコミュニティをフォローしました。\"</que><que vpos=\"442700\" mail=\"184\" name=\"\">/info 2 \"1人（プレミアム1人）がコミュニティをフォローしました。\"</que><que vpos=\"448800\" mail=\"184\" name=\"\">/info 2 \"1人（プレミアム1人）がコミュニティをフォローしました。\"</que><que vpos=\"502800\" mail=\"184\" name=\"\">/info 2 \"2人（プレミアム2人）がコミュニティをフォローしました。\"</que><que vpos=\"514700\" mail=\"184\" name=\"\">/info 2 \"2人（プレミアム2人）がコミュニティをフォローしました。\"</que><que vpos=\"532800\" mail=\"184\" name=\"\">/info 2 \"1人がコミュニティをフォローしました。\"</que><que vpos=\"544700\" mail=\"184\" name=\"\">/info 2 \"1人がコミュニティをフォローしました。\"</que><que vpos=\"562800\" mail=\"184\" name=\"\">/info 2 \"1人（プレミアム1人）がコミュニティをフォローしました。\"</que><que vpos=\"570000\" mail=\"184\" name=\"\">/uadpoint 310492598 9100</que><que vpos=\"574800\" mail=\"184\" name=\"\">/info 2 \"1人（プレミアム1人）がコミュニティをフォローしました。\"</que><que vpos=\"592800\" mail=\"184\" name=\"\">/info 2 \"1人がコミュニティをフォローしました。\"</que><que vpos=\"646700\" mail=\"184\" name=\"\">/info 2 \"1人がコミュニティをフォローしました。\"</que><que vpos=\"688700\" mail=\"184\" name=\"\">/info 2 \"3人（プレミアム2人）がコミュニティをフォローしました。\"</que><que vpos=\"694700\" mail=\"184\" name=\"\">/info 2 \"1人がコミュニティをフォローしました。\"</que></quesheet><picture_url>http://icon.nimg.jp/community/281/co2810321.jpg?1499788679</picture_url><thumb_url>http://icon.nimg.jp/community/s/281/co2810321.jpg?1499788679</thumb_url><is_priority_prefecture></is_priority_prefecture></stream><user><user_id>2297426</user_id><nickname>Ryu</nickname><is_premium>1</is_premium><userAge>29</userAge><userSex>1</userSex><userDomain>jp</userDomain><userPrefecture>14</userPrefecture><userLanguage>ja-jp</userLanguage><room_label>co2810321</room_label><room_seetno>2525</room_seetno><is_join>1</is_join><twitter_info><status>enabled</status><screen_name>namazu2525</screen_name><followers_count>8990</followers_count><is_vip>0</is_vip><profile_image_url>http://pbs.twimg.com/profile_images/2150344550/20120420070739576_normal.jpg</profile_image_url><after_auth>0</after_auth><tweet_token>88d083606920f438c688e70f123bdbc9f106900b</tweet_token></twitter_info></user><rtmp is_fms=\"1\"><url>rtmp://nleu23.live.nicovideo.jp:1935/liveedge/ts_180125_00_1</url><ticket>2297426:lv310492598:0:1516809417:f5a09379ce5483b1</ticket></rtmp><ms><addr>msg104.live.nicovideo.jp</addr><port>2844</port><thread>1620366276</thread></ms><tid_list/><twitter><live_enabled>1</live_enabled><vip_mode_count>10000</vip_mode_count><live_api_url>http://watch.live.nicovideo.jp/api/</live_api_url></twitter><player><qos_analytics>0</qos_analytics><dialog_image><oidashi>http://nicolive.cdn.nimg.jp/live/simg/img/201311/281696.a29344.png</oidashi></dialog_image><is_notice_viewer_balloon_enabled>1</is_notice_viewer_balloon_enabled><error_report>1</error_report></player><marquee><category>一般(その他)</category><game_key>e8965379</game_key><game_time>1516809417</game_time><force_nicowari_off>0</force_nicowari_off></marquee></getplayerstatus>";
        [Test]
        public async Task リスナーmsからのms()
        {
            var ccMock = new Mock<System.Net.CookieContainer>();
            var live_id = "lv123";
            var dataServerMock = new Mock<IDataSource>();
            dataServerMock.Setup(s => s.Get("http://int-main.net/playerstatus/" + live_id, ccMock.Object)).Returns(() => Task.FromResult<string>(psHasMsList));
            dataServerMock.Setup(s => s.Get("http://live.nicovideo.jp/api/getplayerstatus?v=" + live_id, ccMock.Object)).Returns(() => Task.FromResult<string>(playerStatusXml));
            var uploadServerMock = new Mock<IUploadServer>();
            
            
            var siteOptionsMock = new Mock<NicoSiteOptions>();
            var c = new ChannelCommentProvider(dataServerMock.Object, uploadServerMock.Object, live_id, ccMock.Object, siteOptionsMock.Object);
            var method = typeof(ChannelCommentProvider).GetMethod("GetInitialRooms", BindingFlags.NonPublic | BindingFlags.Instance);
            var t = (Task<RoomInfo[]>)method.Invoke(c, null);
            var rooms = await t;

            Assert.IsNotNull(rooms);
            Assert.AreEqual("msg104.live.nicovideo.jp", rooms[0].Addr);
            Assert.AreEqual("1585333588", rooms[0].Thread);
            Assert.AreEqual(2839, rooms[0].Port);
            Assert.AreEqual("co1034396", rooms[0].RoomLabel);

            Assert.AreEqual("msg105.live.nicovideo.jp", rooms[1].Addr);
            Assert.AreEqual("1585333589", rooms[1].Thread);
            Assert.AreEqual(2849, rooms[1].Port);
            Assert.AreEqual("立ち見A列", rooms[1].RoomLabel);
        }
        [Test]
        public async Task リスナー404からのms()
        {
            var ccMock = new Mock<System.Net.CookieContainer>();
            var live_id = "lv123";
            var dataServerMock = new Mock<IDataSource>();
            dataServerMock.Setup(s => s.Get("http://int-main.net/playerstatus/" + live_id, ccMock.Object)).Returns(() => throw new System.Net.WebException());
            dataServerMock.Setup(s => s.Get("http://live.nicovideo.jp/api/getplayerstatus?v=" + live_id, ccMock.Object)).Returns(() => Task.FromResult<string>(playerStatusXml));
            var uploadServerMock = new Mock<IUploadServer>();


            var siteOptionsMock = new Mock<NicoSiteOptions>();
            var c = new ChannelCommentProvider(dataServerMock.Object, uploadServerMock.Object, live_id, ccMock.Object, siteOptionsMock.Object);
            var method = typeof(ChannelCommentProvider).GetMethod("GetInitialRooms", BindingFlags.NonPublic | BindingFlags.Instance);
            var t = (Task<RoomInfo[]>)method.Invoke(c, null);
            var rooms = await t;
            Assert.IsNotNull(rooms);

        }
    }
}
