using isRock.LineBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace linebotluis
{
    public partial class _default : System.Web.UI.Page
    {
        //const string channelAccessToken = "!!!!! 改成自己的ChannelAccessToken !!!!!";
        // const string AdminUserId= "!!!改成你的AdminUserId!!!";
        
        const string channelAccessToken = "pE0p9oAbP3F=+8eFEZrR7bNZM3z5SNrnExSPytHtDo84C1MoMqkObkFDEes06ZtqhEhwGPHng1rdwNF36MEqlHiX3Z863Cz9MvSbLi6zMtK0heTr+Bhq2Lc4Z9EV7CRk542Kuc6nv/Mr1kpz/w6tw5AdB04t89/1O/w1cDnyilFU=";
        const string AdminUserId = "Uadd2378521004fb34ef161153cb5d0a0";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var bot = new Bot(channelAccessToken);
            bot.PushMessage(AdminUserId, $"測試 {DateTime.Now.ToString()} ! ");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var bot = new Bot(channelAccessToken);
            bot.PushMessage(AdminUserId, 1,2);
        }
    }
}