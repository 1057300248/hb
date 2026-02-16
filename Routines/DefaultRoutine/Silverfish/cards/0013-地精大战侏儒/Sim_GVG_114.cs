using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 斯尼德的伐木机卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为8点，攻击力5，生命值7
    // 卡牌效果：<b>亡语：</b>随机召唤一个<b>传说</b>随从。
    class Sim_GVG_114 : SimTemplate //* 斯尼德的伐木机 Sneed's Old Shredder
    // <b>Deathrattle:</b> Summon a random <b>Legendary</b> minion.
    // <b>亡语：</b>随机召唤一个<b>传说</b>随从。 
    {
        // 定义要召唤的传说随从卡牌（这里以国王穆克拉为例）
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_014);

        // 重写亡语效果方法，这是斯尼德的伐木机卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 在斯尼德的伐木机死亡的位置召唤一个传说随从
            // 参数说明：- 要召唤的卡牌，- 召唤位置（死亡随从的位置-1），- 是否为己方召唤
            p.callKid(kid, m.zonepos - 1, m.own);
        }
    }
}