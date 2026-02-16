using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 砰砰博士卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为7点，攻击力7，生命值7
    // 卡牌效果：<b>战吼：</b>召唤两个1/1的砰砰机器人。<i>警告：该机器人随时可能爆炸。</i>
    class Sim_GVG_110 : SimTemplate //* 砰砰博士 Dr. Boom
    // <b>Battlecry:</b> Summon two 1/1 Boom EmbeddedRoutine. <i>WARNING: EmbeddedRoutine may explode.</i>
    // <b>战吼：</b>召唤两个1/1的砰砰机器人。<i>警告：该机器人随时可能爆炸。</i> 
    {
        // 定义要召唤的砰砰机器人卡牌
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GVG_110t);

        // 重写战吼效果方法，这是砰砰博士卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 在砰砰博士的位置召唤第一个砰砰机器人
            // 参数说明：- 要召唤的卡牌，- 召唤位置（砰砰博士的位置），- 是否为己方召唤
            p.callKid(kid, own.zonepos, own.own);

            // 在砰砰博士的位置前方召唤第二个砰砰机器人
            // 参数说明：- 要召唤的卡牌，- 召唤位置（砰砰博士的位置-1），- 是否为己方召唤
            p.callKid(kid, own.zonepos - 1, own.own);
        }
    }
}