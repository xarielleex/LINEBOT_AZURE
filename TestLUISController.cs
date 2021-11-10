using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Threading.Tasks;

using System.Web;

namespace linebotluis.Controllers
{
    public class TestLUISController : isRock.LineBot.LineWebHookControllerBase
    {

        //const string channelAccessToken = "~~~請改成自己的ChannelAccessToken~~~";
        //const string AdminUserId = "~~~改成你的AdminUserId~~~";
        // const string LuisAppId = "~~~改成你的LuisAppId~~~";
        // const string LuisAppKey = "~~~改成你的LuisAppKey~~~";
        //const string Luisdomain = "~~~改成你的Luisdomain~~~"; //ex.westus

        const string channelAccessToken = "/9McZ/eIZMVSfyFwKTY911lndu7BjPZz+UPOTruNQ/TKNzJsZhX3UgdEPqJpZ6efEhwGPHng1rdwNF36MEqlHiX3Z863Cz9MvSbLi6zMtK1iQjvplAiol42Y3aHAzo/AmeuzwDMeYrFcEW2V/2HFNQdB04t89/1O/w1cDnyilFU=";
        const string AdminUserId = "Uadd2378521004fb34ef161153cb5d0a0";
        const string LuisAppId = "f658d550-aa8f-4102-8a7f-8f2462cd59b2";
        const string LuisAppKey = "86b7db040e9e4e759937ddef96ee62a8";
        const string Luisdomain = "westus"; //ex.westus
        private const string EndpointUrl = "https://webdb.documents.azure.com:443/";
        private const string PrimaryKey = "yKg5PPPcZRyPPeSnEdja1NjVWjrJ97EGdeEoPvc1WBBl4W7Im8jj4NCIubrXe6BEfGL360TbhLWBYZQkVJkfUQ==";
        [Route("api/TestLUIS")]
        [HttpPost]
        public IHttpActionResult POST()
        {
            try
            {
                //設定ChannelAccessToken(或抓取Web.Config)
                this.ChannelAccessToken = channelAccessToken;
                //取得Line Event(範例，只取第一個)
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                //配合Line verify 
                if (LineEvent.replyToken == "00000000000000000000000000000000") return Ok();
                //回覆訊息
                if (LineEvent.type == "message")
                {
                    var repmsg = "";
                    if (LineEvent.message.type == "text") //收到文字
                    {
                        //建立LuisClient
                        Microsoft.Cognitive.LUIS.LuisClient lc =
                          new Microsoft.Cognitive.LUIS.LuisClient(LuisAppId, LuisAppKey, true, Luisdomain);

                        //Call Luis API 查詢
                        var ret = lc.Predict(LineEvent.message.text).Result;
                        if (ret.Intents.Count() <= 0)
                            repmsg = $"你說了 '{LineEvent.message.text}' ，但我看不懂喔!";
                       
                         //dialogContext.PrivateConversationData.SetValue("word", LineEvent.message.text);
                        
                        else if (ret.TopScoringIntent.Name == "None")
                            repmsg = $"你說了 '{LineEvent.message.text}' ，但不在我的服務範圍內喔!";
                        else
                        {
                            repmsg = $"OK，你想 '{ret.TopScoringIntent.Name}'，";
                            if (ret.Entities.Count > 0)
                                repmsg += $"想要的是 '{ ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value}' ";
                        }
                        //回覆
                        this.ReplyMessage(LineEvent.replyToken, repmsg);
                    }
                    if (LineEvent.message.type == "sticker") //收到貼圖
                        this.ReplyMessage(LineEvent.replyToken, 1, 2);
                }
                //response OK
                return Ok();
            }
            catch (Exception ex)
            {
                //如果發生錯誤，傳訊息給Admin
                this.PushMessage(AdminUserId, "發生錯誤:\n" + ex.Message);
                //response OK
                return Ok();
            }
        }
    }
}
